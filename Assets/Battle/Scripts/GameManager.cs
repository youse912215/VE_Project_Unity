using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    const int Enemyvalue = 100;



    public static float[] distance = new float[Enemyvalue];

    private GameObject obj;

    private GameObject Towerobj;

    private GameObject Towerobj1;


    float EnemyX;
    float EnemyZ;

    float TowerX;
    float TowerZ;

    float TowerX1;
    float TowerZ1;



    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("Enemy");
        Towerobj = GameObject.Find("Tower");
        Towerobj1 = GameObject.Find("Tower1");


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("座標" + obj.transform.position);
        //Debug.Log("タワー座標" + Towerobj.transform.position);
        
        EnemyX = obj.transform.position.x;
        EnemyZ = obj.transform.position.z;

        TowerX = Towerobj.transform.position.x;
        TowerZ = Towerobj.transform.position.z;


        TowerX1 = Towerobj1.transform.position.x;
        TowerZ1 = Towerobj1.transform.position.z;


        distance[0] = Mathf.Sqrt((TowerX - EnemyX) * (TowerX - EnemyX) + (TowerZ - EnemyZ) * (TowerZ - EnemyZ));
        distance[1] = Mathf.Sqrt((TowerX1 - EnemyX) * (TowerX1 - EnemyX) + (TowerZ1 - EnemyZ) * (TowerZ1 - EnemyZ));

        //Debug.Log("a" + distance[0]);

    }
}
