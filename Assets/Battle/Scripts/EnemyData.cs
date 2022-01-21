using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyMove;
using static TowerData;

public class EnemyData : MonoBehaviour
{
    public const int EnemyCount = 15;

    private GameObject Enemyprefab;

    //public static GameObject Enemytest;


    public static GameObject[,] enemyobj = new GameObject[2, EnemyCount];
    public struct Enemy
    {
        public float hp;
        public float atk;
        public float speed;
        public bool isActivity;

    }
    public static Enemy[,] enemy = new Enemy[2, EnemyCount];
    public static float enemyHP;
    public static float enemyATK;
    public static float[] enemyATKe = new float[50];
    public int virusCount;

    // Start is called before the first frame update
    void Start()
    {
        enemyATK = 5;

        Enemyprefab = GameObject.Find("Enemy");

        for (int i = 0; i < EnemyCount; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                enemy[j, i].hp = 100;
                enemy[j, i].atk = 10;
                enemy[j, i].speed = 1.0f;
                enemy[j, i].isActivity = true;
                enemyobj[j, i] = Instantiate(Enemyprefab);
                enemyobj[j, i].SetActive(true); 
                enemyobj[j, i].transform.position = new Vector3(1492 + i, 20 + i, 492);
            }
        }

        //Enemytest = Instantiate(Enemyprefab);
        //enemyobj[0, 0].SetActive(true);
       
        InitTower();
    }

    // Update is called once per frame
    void Update()
    {
        //Enemytest.SetActive(true);





        for (int i = 0; i < EnemyCount; ++i)
        {
            moveEnemy(enemyobj[0, i], towerobj[0], 10);
        }

    }
}



