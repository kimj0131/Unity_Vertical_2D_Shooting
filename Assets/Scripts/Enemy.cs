using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //적 비행기의 구성요소를 변수로 구체화
    public string enemyName;
    public int enemyScore;
    public float speed;
    public int health;
    public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject bulletObjC;
    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemBomb;

    public GameObject player;
    public GameManager gameManager;
    public ObjectManger objectManager;

    public bool isEnemyDead;
    SpriteRenderer spriteRenderer;
    Animator anim;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {
        //같은 타입으로 초기화
        spriteRenderer = GetComponent<SpriteRenderer>();

        //나열된 타입이 아닐때 애니메이션을 초기화
        if (enemyName != "ST_G" || enemyName != "ST_R")
            anim = GetComponent<Animator>();
    }
    void OnEnable()
    {
        isEnemyDead = false;

        switch (enemyName)
        {
            case "B":
                health = 1000;
                Invoke("Stop", 2);
                break;
            case "L_G":
                health = 60;
                break;
            case "L_R":
                health = 60;
                break;
            case "M_G":
                health = 40;
                break;
            case "M_R":
                health = 40;
                break;
            case "S_G":
                health = 15;
                break;
            case "S_R":
                health = 15;
                break;
            case "ST_G":
                health = 3;
                break;
            case "ST_R":
                health = 3;
                break;
        }
    }
    void Stop()
    {
        //함수 두번호출 방지
        if (!gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        //패턴을 읽음
        Invoke("BossThink", 2);
    }

    //보스패턴흐름
    void BossThink()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        //패턴변경시 패턴실행횟수 변수 초기화
        curPatternCount = 0;
        //패턴테스트용
        //patternIndex = 3;
        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    void FireForward()
    {
        if (health <= 0)
            return;
        Debug.Log("앞으로 8발 발사");

        //# 정면 8줄 발사
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.2f;
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        GameObject bulletRRR = objectManager.MakeObj("BulletBossA");
        bulletRRR.transform.position = transform.position + Vector3.right * 0.9f;
        GameObject bulletRRRR = objectManager.MakeObj("BulletBossA");
        bulletRRRR.transform.position = transform.position + Vector3.right * 1.15f;

        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.2f;
        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;
        GameObject bulletLLL = objectManager.MakeObj("BulletBossA");
        bulletLLL.transform.position = transform.position + Vector3.left * 0.9f;
        GameObject bulletLLLL = objectManager.MakeObj("BulletBossA");
        bulletLLLL.transform.position = transform.position + Vector3.left * 1.15f;

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRRR = bulletRRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRRRR = bulletRRRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLLL = bulletLLL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLLLL = bulletLLLL.GetComponent<Rigidbody2D>();


        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 7.6f, ForceMode2D.Impulse);
        rigidRRR.AddForce(Vector2.down * 7.3f, ForceMode2D.Impulse);
        rigidRRRR.AddForce(Vector2.down * 7, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 7.6f, ForceMode2D.Impulse);
        rigidLLL.AddForce(Vector2.down * 7.3f, ForceMode2D.Impulse);
        rigidLLLL.AddForce(Vector2.down * 7, ForceMode2D.Impulse);

        //각 패턴별 횟수를 실행후 다음패턴으로 넘어가도록함
        curPatternCount++;

        if(curPatternCount< maxPatternCount[patternIndex])
            Invoke("FireForward", 2);
        else
            Invoke("BossThink", 3);
    }

    void FireShot()
    {
        if (health <= 0)
            return;
        Debug.Log("플레이어 방향으로 삿건");


        //# 플레이어에게 5발의 탄환을 산탄형식으로 발사
        for (int index = 0; index < 8; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 6, ForceMode2D.Impulse);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 2f);
        else
            Invoke("BossThink", 3);
    }

    void FireArc()
    {
        if (health <= 0)
            return;
        Debug.Log("부채모양으로 발사");

        //# 채찍모양으로 발사
        int roundNumA = 30;
        int roundNumB = 20;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum), -1);
            rigid.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.7f);
        else
            Invoke("BossThink", 3);
    }

    void FireAround()
    {
        if (health <= 0)
            return;
        Debug.Log("원 형태로 전체공격");
        //# 원형 모양으로 전체발사
        //탄환수
        int roundNumA = 30;
        int roundNumB = 25;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum),
                Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 2.5f, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.8f);
        else
            Invoke("BossThink", 4);
    }


    void Update()
    {
        //보스타입은 아래함수 실행x
        if (enemyName == "B")
            return;

        Fire();
        Reload();
    }
    void Fire()
    {
        if (curShotDelay < maxShotDelay)
            return;
     
        if (enemyName == "S_G" || enemyName == "S_R")        {

            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 playDirVec = player.transform.position - transform.position;
            rigid.AddForce(playDirVec.normalized * 3, ForceMode2D.Impulse);
        }
        
        else if (enemyName == "M_G" || enemyName == "M_R")        {

            GameObject bullet = objectManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 playDirVec = player.transform.position - transform.position;
            rigid.AddForce(playDirVec.normalized * 3, ForceMode2D.Impulse);
        }
        
        else if (enemyName == "L_G" || enemyName == "L_R")        {
            GameObject bulletR = objectManager.MakeObj("BulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;

            GameObject bulletL = objectManager.MakeObj("BulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            //목표물로 방향 = 목표물 위치(플레이어) - 자신의 위치(적)
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rigidR.AddForce(dirVecR.normalized * 2, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 2, ForceMode2D.Impulse);
        }
      
        curShotDelay = 0;
    }
    void Reload()
    {
        //딜레이에 실제시간을 더하여 시간계산
        curShotDelay += Time.deltaTime;
    }

    public void OnHit(int dmg)
    {
        health -= dmg;
        //피격효과
        if(enemyName == "ST_G"|| enemyName == "ST_R")
        {
            //적비행기가 피격시 스프라이트 변경 (St타입만)
            spriteRenderer.sprite = sprites[1];
            //시간차를 두고 함수를 호출(RetrunSprite())
            Invoke("ReturnSprite", 0.1f);
        }
        else
        {
            //트리거 파라미터에 의해실행
            anim.SetTrigger("OnHit");
        }
        
        //health가 0이하일때
        if (health <= 0)
        {
            //중복실행 방지
            if (isEnemyDead)
                return;
            isEnemyDead = true;
            //폭파효과음
            SoundManager.instance.PlayExplodeSound();
            //점수증가
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            //랜덤으로 아이템을 드랍(보스는 확률 0)
            int ran = Random.Range(0, 10);

            //타입별 드랍확률
            //#초록
            if(enemyName == "ST_G" || enemyName == "S_G")            {
                if (ran < 3)//아이템없음
                {
                    Debug.Log("Not Item");
                }

                else if (ran < 10)//코인
                {
                    GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                    itemCoin.transform.position = transform.position;

                    Debug.Log("Coin1");
                }
            }

            if (enemyName == "M_G" || enemyName == "L_G")            {
                //코인2
                GameObject itemCoin2 = objectManager.MakeObj("ItemCoin2");
                itemCoin2.transform.position = transform.position;

                Debug.Log("Coin2");
            }

            //#빨강
            if (enemyName == "ST_R")            {
                //파워
                GameObject itemPower = objectManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;

                Debug.Log("Power");
            }

            if (enemyName == "S_R")            {
                if (ran < 1)//파워
                {
                    GameObject itemPower = objectManager.MakeObj("ItemPower");
                    itemPower.transform.position = transform.position;
                    Debug.Log("Power");
                }

                else if (ran < 10)//폭탄
                {
                    GameObject itemBomb = objectManager.MakeObj("ItemBomb");
                    itemBomb.transform.position = transform.position;
                    Debug.Log("Bomb");
                }
            }

            if (enemyName == "M_R" || enemyName == "L_R")            {
                if (ran < 2)//파워
                {
                    GameObject itemPower = objectManager.MakeObj("ItemPower");
                    itemPower.transform.position = transform.position;
                    Debug.Log("Power");
                }

                else if (ran < 10)//폭탄
                {
                    GameObject itemBomb = objectManager.MakeObj("ItemBomb");
                    itemBomb.transform.position = transform.position;
                    Debug.Log("Bomb");
                }
            }
            objectManager.DeleteAllObj("Boss");
            CancelInvoke("BossThink");
            gameObject.SetActive(false);
            //Quaternion.identity : 기본 회전값 = 0
            transform.rotation = Quaternion.identity;
            //폭파효과
            gameManager.CallExplosion(transform.position, enemyName);

            //# Boss Kill
            if (enemyName == "B")
            {
                //폭파효과음
                SoundManager.instance.PlayExplodeSound();
                gameManager.StageEnd();
                //적 탄환 제거
                GameObject[] bulletsA = objectManager.GetPool("BulletEnemyA");
                GameObject[] bulletsB = objectManager.GetPool("BulletEnemyB");
                GameObject[] bulletsC = objectManager.GetPool("BulletBossA");
                GameObject[] bulletsD = objectManager.GetPool("BulletBossB");

                for (int index = 0; index < bulletsA.Length; index++)                {
                    if (bulletsA[index].activeSelf)
                    {
                        bulletsA[index].SetActive(false);
                    }
                }
                for (int index = 0; index < bulletsB.Length; index++)                {
                    if (bulletsB[index].activeSelf)
                    {
                        bulletsB[index].SetActive(false);
                    }
                }
                for (int index = 0; index < bulletsC.Length; index++)                {
                    if (bulletsC[index].activeSelf)
                    {
                        bulletsC[index].SetActive(false);
                    }
                }
                for (int index = 0; index < bulletsD.Length; index++)                {
                    if (bulletsD[index].activeSelf)
                    {
                        bulletsD[index].SetActive(false);
                    }
                }
            }
        }
    }
    
    void ReturnSprite()
    {
        //피격후 원래 스프라이트로 되돌아옴
        spriteRenderer.sprite = sprites[0];
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        //트리거를 이용 첫번째는 경계에서 적 오브젝트를 삭제 
        //두번째는 OnHit함수를 불러들여서 실행, 플레이어 탄환을 적이 피격시 총알 오브젝트를 삭제
        if (collision.gameObject.tag == "BorderBullet" && enemyName !="B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            //적이 피격시 플레이어 총알을 삭제
            collision.gameObject.SetActive(false);
        }
    }
}
