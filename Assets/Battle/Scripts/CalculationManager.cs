using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerData;
using static EnemyData;


public class CalculationManager : MonoBehaviour
{
    //const int Enemyvalue = 100;



    //public static float[] distance = new float[Enemyvalue];

    private GameObject Enemyobj;

    private GameObject Towerobj;

    private GameObject Towerobj1;


    float EnemyX;
    float EnemyZ;

    float TowerX;
    float TowerZ;

    float TowerX1;
    float TowerZ1;

   //Vector3[] towerposi = new Vector3[TowerCount];
    //Vector3[] enemyposi = new Vector3[EnemyCount];

    public static float [,] kyori = new float [TowerCount, EnemyCount];



    // Start is called before the first frame update
    void Start()
    {
        Enemyobj = GameObject.Find("Enemy");
        Towerobj = GameObject.Find("Tower");
        Towerobj1 = GameObject.Find("Tower1");


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("座標" + obj.transform.position);
        //Debug.Log("タワー座標" + Towerobj.transform.position);

       

        //Debug.Log("a" + distance[0]);

    }

    public static void culc(GameObject obj, Vector3[] v, int n)
    {
        //EnemyX = Enemyobj.transform.position.x;
        //EnemyZ = Enemyobj.transform.position.z;

        for (int i = 0; i < n; ++i)
        {
            v[i] = obj.transform.position;
        }

        
        //TowerX = Towerobj.transform.position.x;
        //TowerZ = Towerobj.transform.position.z;


        //TowerX1 = Towerobj1.transform.position.x;
        //TowerZ1 = Towerobj1.transform.position.z;


        //distance[0] = Mathf.Sqrt((TowerX - EnemyX) * (TowerX - EnemyX) + (TowerZ - EnemyZ) * (TowerZ - EnemyZ));
        //distance[1] = Mathf.Sqrt((TowerX1 - EnemyX) * (TowerX1 - EnemyX) + (TowerZ1 - EnemyZ) * (TowerZ1 - EnemyZ));

        
    }

    public static void distanse(int TCou, int ECou,GameObject[] v, GameObject[,] w)
    {
        //for (int i = 0; i < TowerCount; ++i)
        //{
        //    for (int j = 0; j < EnemyCount; ++j)
        //    {

        //        kyori[TowerCount,EnemyCount] = Mathf.Sqrt((v[i].x - w[j].x) * (v[i].x - w[j].x) + (v[i].z - w[j].z) * (v[i].z - w[j].z));

        //    }
        //}
        for (int i = 0; i < TCou; ++i)
        {
            for (int j = 0; j < ECou; ++j)
            {
                kyori[i, j] = Mathf.Sqrt(
                    (v[i].transform.position.x - w[0, j].transform.position.x)
                    * (v[i].transform.position.x - w[0, j].transform.position.x)
                    + (v[i].transform.position.z - w[0, j].transform.position.z)
                    * (v[i].transform.position.z - w[0, j].transform.position.z));
            }
        }
    }
}


