using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManger : MonoBehaviour
{
    //프리팹 변수 생성
    public GameObject enemySt_GPrefeb;
    public GameObject enemySt_RPrefeb;
    public GameObject enemyS_GPrefeb;
    public GameObject enemyS_RPrefeb;
    public GameObject enemyM_GPrefeb;
    public GameObject enemyM_RPrefeb;
    public GameObject enemyL_GPrefeb;
    public GameObject enemyL_RPrefeb;
    public GameObject enemyBPrefeb;

    public GameObject itemCoinPrefeb;
    public GameObject itemCoin2Prefeb;
    public GameObject itemPowerPrefeb;
    public GameObject itemBombPrefeb;

    public GameObject bulletPlayerAPrefeb;
    public GameObject bulletPlayerBPrefeb;
    public GameObject bulletEnemyAPrefeb;
    public GameObject bulletEnemyBPrefeb;
    public GameObject bulletBossAPrefeb;
    public GameObject bulletBossBPrefeb;

    public GameObject explosionPrefeb;


    //미리 생성할 오브젝트를 배열로 선언
    GameObject[] enemyST_G;
    GameObject[] enemyST_R;
    GameObject[] enemyS_G;
    GameObject[] enemyS_R;
    GameObject[] enemyM_G;
    GameObject[] enemyM_R;
    GameObject[] enemyL_G;
    GameObject[] enemyL_R;
    GameObject[] enemyB;

    GameObject[] itemCoin;
    GameObject[] itemCoin2;
    GameObject[] itemPower;
    GameObject[] itemBomb;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;

    GameObject[] explosion;

    GameObject[] targetPool;


    void Awake()
    {
        //한번에 만들 개수를 고려, 배열길이를 정함
        enemyST_G = new GameObject[20];
        enemyST_R = new GameObject[10];
        enemyS_G = new GameObject[20];
        enemyS_R = new GameObject[10];
        enemyM_G = new GameObject[10];
        enemyM_R = new GameObject[10];
        enemyL_G = new GameObject[5];
        enemyL_R = new GameObject[5];
        enemyB = new GameObject[1];

        itemCoin = new GameObject[20];
        itemCoin2 = new GameObject[10];
        itemPower = new GameObject[10];
        itemBomb = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[1000];
        bulletEnemyB = new GameObject[500];
        bulletBossA = new GameObject[30];
        bulletBossB = new GameObject[500];

        explosion = new GameObject[30];
        Generate();

    }
    void Generate()
    {
        //instantiate로 생성한 인스턴스를 배열에 저장
        //1.Enemy
        for (int index = 0; index < enemyST_G.Length; index++)        {
            enemyST_G[index] = Instantiate(enemySt_GPrefeb);
            //생성직후 비활성화
            enemyST_G[index].SetActive(false);
        }
        for (int index = 0; index < enemyST_R.Length; index++)        {
            enemyST_R[index] = Instantiate(enemySt_RPrefeb);
            enemyST_R[index].SetActive(false);
        }
        for (int index = 0; index < enemyS_G.Length; index++)        {
            enemyS_G[index] = Instantiate(enemyS_GPrefeb);
            enemyS_G[index].SetActive(false);
        }
        for (int index = 0; index < enemyS_R.Length; index++)        {
            enemyS_R[index] = Instantiate(enemyS_RPrefeb);
            enemyS_R[index].SetActive(false);
        }
        for (int index = 0; index < enemyM_G.Length; index++)        {
            enemyM_G[index] = Instantiate(enemyM_GPrefeb);
            enemyM_G[index].SetActive(false);
        }
        for (int index = 0; index < enemyM_R.Length; index++)        {
            enemyM_R[index] = Instantiate(enemyM_RPrefeb);
            enemyM_R[index].SetActive(false);
        }
        for (int index = 0; index < enemyL_G.Length; index++)        {
            enemyL_G[index] = Instantiate(enemyL_GPrefeb);
            enemyL_G[index].SetActive(false);
        }
        for (int index = 0; index < enemyL_R.Length; index++)        {
            enemyL_R[index] = Instantiate(enemyL_RPrefeb);
            enemyL_R[index].SetActive(false);
        }
        for (int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefeb);
            enemyB[index].SetActive(false);
        }
        //2.Item
        for (int index = 0; index < itemCoin.Length; index++)        {
            itemCoin[index] = Instantiate(itemCoinPrefeb);
            itemCoin[index].SetActive(false);
        }
        for (int index = 0; index < itemCoin2.Length; index++)        {
            itemCoin2[index] = Instantiate(itemCoin2Prefeb);
            itemCoin2[index].SetActive(false);
        }
        for (int index = 0; index < itemPower.Length; index++)        {
            itemPower[index] = Instantiate(itemPowerPrefeb);
            itemPower[index].SetActive(false);
        }
        for (int index = 0; index < itemBomb.Length; index++)        {
            itemBomb[index] = Instantiate(itemBombPrefeb);
            itemBomb[index].SetActive(false);
        }
        //3.Bullet
        for (int index = 0; index < bulletPlayerA.Length; index++)        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefeb);
            bulletPlayerA[index].SetActive(false);
        }
        for (int index = 0; index < bulletPlayerB.Length; index++)        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefeb);
            bulletPlayerB[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyA.Length; index++)        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefeb);
            bulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyB.Length; index++)        {
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefeb);
            bulletEnemyB[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossA.Length; index++)        {
            bulletBossA[index] = Instantiate(bulletBossAPrefeb);
            bulletBossA[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossB.Length; index++)        {
            bulletBossB[index] = Instantiate(bulletBossBPrefeb);
            bulletBossB[index].SetActive(false);
        }

        for (int index = 0; index < explosion.Length; index++)        {
            explosion[index] = Instantiate(explosionPrefeb);
            explosion[index].SetActive(false);
        }
    }
    //오브젝트 풀에 접근할 수 있는 함수 생성
    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "EnemyST_G":
                targetPool = enemyST_G;
                break;
            case "EnemyST_R":
                targetPool = enemyST_R;
                break;
            case "EnemyS_G":
                targetPool = enemyS_G;
                break;
            case "EnemyS_R":
                targetPool = enemyS_R;
                break;
            case "EnemyM_G":
                targetPool = enemyM_G;
                break;
            case "EnemyM_R":
                targetPool = enemyM_R;
                break;
            case "EnemyL_G":
                targetPool = enemyL_G;
                break;
            case "EnemyL_R":
                targetPool = enemyL_R;
                break;
            case "EnemyB":
                targetPool = enemyB;
                break;

            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemCoin2":
                targetPool = itemCoin2;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBomb":
                targetPool = itemBomb;
                break;

            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;

            case "Explosion":
                targetPool = explosion;
                break;
        }
        //index 배열 길이만큼 반복
        //targetPool은 위쪽 스위치문에서 가져옴
        for (int index = 0; index < targetPool.Length; index++)
        {
            //오브젝트가 비활성화 된 경우 접근하여 활성화 후 반환시킴
            if (targetPool[index].activeSelf == false)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }
    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "EnemyST_G":
                targetPool = enemyST_G;
                break;
            case "EnemyST_R":
                targetPool = enemyST_R;
                break;
            case "EnemyS_G":
                targetPool = enemyST_R;
                break;
            case "EnemyS_R":
                targetPool = enemyS_R;
                break;
            case "EnemyM_G":
                targetPool = enemyM_G;
                break;
            case "EnemyM_R":
                targetPool = enemyM_R;
                break;
            case "EnemyL_G":
                targetPool = enemyL_G;
                break;
            case "EnemyL_R":
                targetPool = enemyL_R;
                break;
            case "EnemyB":
                targetPool = enemyB;
                break;

            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemCoin2":
                targetPool = itemCoin2;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBomb":
                targetPool = itemBomb;
                break;

            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;

            case "Explosion":
                targetPool = explosion;
                break;
        }
        return targetPool;
    }

    public void DeleteAllObj(string type)
    {
        if (type == "B")
        {
            for (int index = 0; index < bulletBossA.Length; index++)
                bulletBossA[index].SetActive(false);

            for (int index = 0; index < bulletBossB.Length; index++)
                bulletBossA[index].SetActive(false);
        }
    }
}
