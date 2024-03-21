using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public int stage;
    public Animator stageAnim;
    public Animator clearAnim;
    public Animator fadeAnim;
    public Transform playerPos;

    public string[] enemyObjs;          
    public Transform[] spawnPoints;    
    public float nextSpawnDelay;       
    public float curSpawnDelay;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] bombImage;
    public GameObject gameOverSet;

    public ObjectManger objectManger;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "EnemyST_G", "EnemyST_R","EnemyS_G", "EnemyS_R" , "EnemyM_G", "EnemyM_R", "EnemyL_G", "EnemyL_R","EnemyB" };
        StageStart();
    }

    public void StageStart()    //스테이지 시작
    {
        //#스테이지 UI 로드
        stageAnim.SetTrigger("On");
        stageAnim.GetComponent<Text>().text = "Stage " + stage + "\nStart";
        clearAnim.GetComponent<Text>().text = "Stage " + stage + "\nClear!";
        //#적 스폰 파일 읽기
        ReadSpawnFile();
        //#페이드 인
        fadeAnim.SetTrigger("In");
        //# 플레이어 위치 리셋
        player.transform.position = playerPos.position;
    }

    public void StageEnd()  //스테이지 끝
    {
        //#스테이지 클리어 UI 로드
        clearAnim.SetTrigger("On");
        //#페이드 아웃
        fadeAnim.SetTrigger("Out");
        //#스테이지 증가
        stage++;
        if (stage > 2)
            Invoke("GameOver", 5);
        else
            Invoke("StageStart", 5);
    }

    void ReadSpawnFile()    //스테이지 텍스트 파일 읽기
    {
        //변수 초기화
        //함수 실행전 List내용을 지움
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        //스폰 파일 읽기
        //Resources 폴더 내 파일 불러오기
        TextAsset textFile = Resources.Load("Stage "+ stage) as TextAsset;
        //파일 내의 문자열 데이터 읽기 클래스
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            if (line == null)
                break;

            //리스폰 데이터 생성
            Spawn spawnData = new Spawn();
            //지정한 구분문자로 문자열을 나눔
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }
        //텍스트 파일 닫기
        stringReader.Close();
        //첫번째 스폰 딜레이 적용
        nextSpawnDelay = spawnList[0].delay;
    }

    void Update()
    {
        // 델타타임을 더함
        curSpawnDelay += Time.deltaTime;
        //스폰이 안끝났거나 딜레이 조건이 될경우
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            //적생성후 딜레이 변수 0으로 초기화(초기화해야 스폰리스트 짜기 수월)
            curSpawnDelay = 0;
        }
        // UI Score 업데이트
        Player playerLogic = player.GetComponent<Player>();
        // string.Format() 지정된 양식으로 문자열을 변환해줌({0:n0} : 세자리마다 쉼표로 나눠주는 숫자양식)
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }
    
    void SpawnEnemy()   //적 소환 함수
    {
        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "ST_G":
                enemyIndex = 0;
                break;
            case "ST_R":
                enemyIndex = 1;
                break;
            case "S_G":
                enemyIndex = 2;
                break;
            case "S_R":
                enemyIndex = 3;
                break;
            case "M_G":
                enemyIndex = 4;
                break;
            case "M_R":
                enemyIndex = 5;
                break;
            case "L_G":
                enemyIndex = 6;
                break;
            case "L_R":
                enemyIndex = 7;
                break;
            case "B":
                enemyIndex = 8;
                break;
        }
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManger.MakeObj(enemyObjs[enemyIndex]);
        //#위치와 각도는 인스턴스 변수에서 사용
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        enemyLogic.player = player;
        enemyLogic.gameManager = this;
        enemyLogic.objectManager = objectManger;

        if (enemyPoint == 5 || enemyPoint == 6)
        { //오른쪽 스폰
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        { //왼쪽 스폰
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        { //정면 스폰
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

        //리스폰 인덱스 증가
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        //다음 리스폰 딜레이 갱신
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }
   
    public void UpdateLifeIcon(int life)
    {
        // UI Life 비활성화
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
        // UI Life 활성화
        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }
    public void UpdateBombIcon(int bomb)
    {
        // UI Bomb 비활성화
        for (int index = 0; index < 3; index++)
        {
            bombImage[index].color = new Color(1, 1, 1, 0);
        }
        // UI Bomb 활성화
        for (int index = 0; index < bomb; index++)
        {
            bombImage[index].color = new Color(1, 1, 1, 1);
        }
    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    void RespawnPlayerExe()
    {

        player.transform.position = Vector3.down * 4.5f;
        player.SetActive(true);

        //bool 변수를 초기화해서 피격함수를 다시실행 가능하게함.
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }

    //폭파효과 적용함수
    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManger.MakeObj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type);
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }
}
