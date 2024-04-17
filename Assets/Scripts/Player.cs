using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //플레이어 이동 경계지점
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public int life;            
    public int score;           
    public float speed;         
    public int maxPower;        
    public int power;           
    public int maxBomb;         
    public int bomb;            
    public float maxShotDelay;  
    public float curShotDelay;  

    public GameObject bulletObjA;   
    public GameObject bulletObjB;
    public GameObject bombEffect;

    public GameManager gameManager;
    public ObjectManger objectManager;

    public bool isHit;
    public bool isBombTime;
    public bool isDubbleTouch;
    public bool isRespawntime;

    Animator anim;
    SpriteRenderer spriteRenderer;
    public AudioSource audioSource;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        Unbeatable();
        Invoke("Unbeatable", 2);
    }

    void Unbeatable()
    {
        isRespawntime = !isRespawntime;
        
        if (isRespawntime)  //무적타임 이펙트(투명도)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
        else    //무적타임 종료
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }

    void Update()
    {
        Bomb();
        Move();
        Fire();
        Reload();
    }

    void Move()
    {
        //마우스클릭(터치)로 입력을 받고 이동시킴 (0은 왼쪽, 1은 오른쪽, 2는 가운데 클릭)
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);       //마우스위치값
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);                        //스크린좌표(모니터화면)를 월드좌표(게임)로 변환
            Vector2 nextPos;
            float distance = Vector2.Distance(transform.position, worldPos);                    //두지점 사이의 거리계산

            float h = worldPos.y;
            float v = worldPos.x;
            if (distance > 0.1f)                                                                //일정거리(0.1만큼 거리) 될때까지 이동하기위한 조건
            {
                //위로 이동중 경계에 닿거나 아래 이동중 경계에 닿으면 위아래로 이동하지않음.
                if ((isTouchTop && worldPos.y > transform.position.y) || (isTouchBottom && worldPos.y < transform.position.y))
                {
                    h = transform.position.y;
                }
                //좌로 이동중 경계에 닿거나 우로 이동중 경계에 닿으면 좌우로 이동하지않음.
                if ((isTouchLeft && worldPos.x < transform.position.x) || (isTouchRight && worldPos.x > transform.position.x))
                {
                    v = transform.position.x;
                }
                //최종 목표 위치가 현재 위치가 되어서 이동하지않음.
                nextPos = new Vector2(v, h + 0.5f);

                //폭탄사용시 가운데로 이동 방지
                if (isDubbleTouch == false)
                {
                    //현재위치에서 목표위치로 속도(speed)에 맞춰(플레이어의 속도) 이동시킴.
                    transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
                }
            }
            if (worldPos.x < transform.position.x)
            {
                //왼쪽 애니메이션
                anim.SetInteger("Input", -1);
            }
            if (worldPos.x > transform.position.x)
            {
                //오른쪽 애니메이션
                anim.SetInteger("Input", 1);
            }
        }
        //Idle 상태 애니메이션
        else anim.SetInteger("Input", 0);
    }

    void Fire()
    {
        //Fire1버튼 누르지않으면 실행하지않음(눌러야지만 실행)
        if (!Input.GetButton("Fire1"))
            return;

        if (curShotDelay < maxShotDelay)
            return;

        switch (power)
        {
            case 1:
                //Power One
                GameObject bullet = objectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                //총알속도 10
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                //효과음
                audioSource.Play();

                break;

            case 2:
                GameObject bulletR = objectManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                GameObject bulletL = objectManager.MakeObj("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;

                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                //효과음
                audioSource.Play();

                break;

            case 3:
                GameObject bulletRR = objectManager.MakeObj("BulletPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.25f;
                GameObject bulletCC = objectManager.MakeObj("BulletPlayerB");
                bulletCC.transform.position = transform.position;
                GameObject bulletLL = objectManager.MakeObj("BulletPlayerA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.25f;

                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                //효과음
                audioSource.Play();

                break;

            case 4:
                GameObject bulletRRR = objectManager.MakeObj("BulletPlayerA");
                bulletRRR.transform.position = transform.position + Vector3.right * 0.25f;
                GameObject bulletRL = objectManager.MakeObj("BulletPlayerB");
                bulletRL.transform.position = transform.position + Vector3.right * 0.1f;
                GameObject bulletLR = objectManager.MakeObj("BulletPlayerB");
                bulletLR.transform.position = transform.position + Vector3.left * 0.1f;
                GameObject bulletLLL = objectManager.MakeObj("BulletPlayerA");
                bulletLLL.transform.position = transform.position + Vector3.left * 0.25f;


                Rigidbody2D rigidRRR = bulletRRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRL = bulletRL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLR = bulletLR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLLL = bulletLLL.GetComponent<Rigidbody2D>();

                rigidRRR.AddForce(new Vector2(0.07f, 1f) * 10, ForceMode2D.Impulse);
                rigidRL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLLL.AddForce(new Vector2(-0.07f, 1f) * 10, ForceMode2D.Impulse);

                //효과음
                audioSource.Play();

                break;
        }
        //재장전(딜레이 변수 0으로 초기화
        curShotDelay = 0;
    }

    void Reload()
    {
        //딜레이에 실제시간을 더하여 시간계산
        curShotDelay += Time.deltaTime;
    }

    void Bomb()
    {
        if (Input.touchCount == 2 || Input.GetMouseButton(2))
        {
            isDubbleTouch = true;
            if (isBombTime)
                return;

            if (bomb == 0)
                return;

            bomb--;
            
            //부울변수 초기화
            isBombTime = true;
            //이펙트 활성화
            gameManager.UpdateBombIcon(bomb);

            bombEffect.SetActive(true);
            Invoke("OffBombEffect", 3f);
            //적 에게 데미지
            GameObject[] enemiesSt_G = objectManager.GetPool("EnemyST_G");
            GameObject[] enemiesSt_R = objectManager.GetPool("EnemyST_R");
            GameObject[] enemiesS_G = objectManager.GetPool("EnemyS_G");
            GameObject[] enemiesS_R = objectManager.GetPool("EnemyS_R");
            GameObject[] enemiesM_G = objectManager.GetPool("EnemyM_G");
            GameObject[] enemiesM_R = objectManager.GetPool("EnemyM_R");
            GameObject[] enemiesL_G = objectManager.GetPool("EnemyL_G");
            GameObject[] enemiesL_R = objectManager.GetPool("EnemyL_R");
            GameObject[] enemiesB = objectManager.GetPool("EnemyB");

            for (int index = 0; index < enemiesSt_G.Length; index++)            {
                if (enemiesSt_G[index].activeSelf)                {
                    Enemy enemyLogic = enemiesSt_G[index].GetComponent<Enemy>();
                    enemyLogic.OnHit(100);
                }
            }
            for (int index = 0; index < enemiesSt_R.Length; index++)            {
                if (enemiesSt_R[index].activeSelf)                {
                    Enemy enemyLogic = enemiesSt_R[index].GetComponent<Enemy>();
                    enemyLogic.OnHit(100);
                }
            }
            for (int index = 0; index < enemiesS_G.Length; index++)            {
                if (enemiesS_G[index].activeSelf)                {
                    Enemy enemyLogic = enemiesS_G[index].GetComponent<Enemy>();
                    enemyLogic.OnHit(100);
                }
            }
            for (int index = 0; index < enemiesS_R.Length; index++)            {
                if (enemiesS_R[index].activeSelf)                {
                    Enemy enemyLogic = enemiesS_R[index].GetComponent<Enemy>();
                    enemyLogic.OnHit(100);
                }
            }
            for (int index = 0; index < enemiesM_G.Length; index++)            {
                if (enemiesM_G[index].activeSelf)                {
                    Enemy enemyLogic = enemiesM_G[index].GetComponent<Enemy>();
                    enemyLogic.OnHit(100);
                }
            }
            for (int index = 0; index < enemiesM_R.Length; index++)            {
                if (enemiesM_R[index].activeSelf)                {
                    Enemy enemyLogic = enemiesM_R[index].GetComponent<Enemy>();
                    enemyLogic.OnHit(100);
                }
            }
            for (int index = 0; index < enemiesL_G.Length; index++)            {
                if (enemiesL_G[index].activeSelf)                {
                    Enemy enemyLogic = enemiesL_G[index].GetComponent<Enemy>();
                    enemyLogic.OnHit(100);
                }
            }
            for (int index = 0; index < enemiesL_R.Length; index++)            {
                if (enemiesL_R[index].activeSelf)                {
                    Enemy enemyLogic = enemiesL_R[index].GetComponent<Enemy>();
                    enemyLogic.OnHit(100);
                }
            }
            for (int index = 0; index < enemiesB.Length; index++)            {
                if (enemiesB[index].activeSelf)                {
                    Enemy enemyLogic = enemiesB[index].GetComponent<Enemy>();
                    enemyLogic.OnHit(100);
                }
            }

            //적 탄환 제거
            GameObject[] bulletsA = objectManager.GetPool("BulletEnemyA");
            GameObject[] bulletsB = objectManager.GetPool("BulletEnemyB");
            GameObject[] bulletsC = objectManager.GetPool("BulletBossA");
            GameObject[] bulletsD = objectManager.GetPool("BulletBossB");

            for (int index = 0; index < bulletsA.Length; index++)            {
                if (bulletsA[index].activeSelf)                {
                    bulletsA[index].SetActive(false);
                }
            }
            for (int index = 0; index < bulletsB.Length; index++)            {
                if (bulletsB[index].activeSelf)                {
                    bulletsB[index].SetActive(false);
                }
            }
            for (int index = 0; index < bulletsC.Length; index++)            {
                if (bulletsC[index].activeSelf)                {
                    bulletsC[index].SetActive(false);
                }
            }
            for (int index = 0; index < bulletsD.Length; index++)            {
                if (bulletsD[index].activeSelf)
                {
                    bulletsD[index].SetActive(false);
                }
            }
        }
        else if (Input.touchCount < 2)
        {
            //가운데이동방지 변수초기화
            isDubbleTouch = false;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        //이동경계에 닿음
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
        //피격이벤트
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            //리스폰타임에는 빠져나감(무적)
            if (isRespawntime)
                return;

            //피격시 함수실행종료
            if (isHit)
                return;

            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);
            gameManager.CallExplosion(transform.position, "P");
            //폭파효과음
            SoundManager.instance.PlayExplodeSound();
            if (life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.RespawnPlayer();
            }
            gameObject.SetActive(false);
            if (collision.gameObject.tag == "Enemy")
            {
                GameObject bossGo = collision.gameObject;
                Enemy enemyBoss = bossGo.GetComponent<Enemy>();
                if (enemyBoss.enemyName == "B")
                {
                    if (power == 1)
                        return;
                    power--;
                }
                else
                {
                    collision.gameObject.SetActive(false);
                }
            }
            else
                collision.gameObject.SetActive(false);
            if (power == 1)
                return;
            power--;
        }
        //아이템 이벤트
        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.Type)
            {
                //코인1습득
                case "Coin 1":
                    score += 1000;
                    break;

                //코인2습득
                case "Coin 2":
                    score += 2500;
                    break;

                //파워습득
                case "Power":
                    if (power == maxPower)  //파워가 맥스일때
                        score += 500;
                    else
                        power++;
                    break;

                //폭탄습득
                case "Bomb":
                    if (bomb == maxBomb)    //폭탄이 맥스일때
                        score += 500;
                    else
                    {
                        bomb++;
                        gameManager.UpdateBombIcon(bomb);
                    }
                    break;
            }
            //습득한 아이템 오브젝트 비활성화
            collision.gameObject.SetActive(false);
        }
    }

    //폭탄이펙트 비활성화 함수
    void OffBombEffect()
    {
        bombEffect.SetActive(false);
        isBombTime = false;
    }

    //4방향 경계에서 떨어짐
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
