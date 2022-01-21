using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData : MonoBehaviour
{

    public const int TowerCount = 4;

    public static GameObject Towerprefab;
    
    
    public static GameObject[] towerobj = new GameObject[TowerCount];



    public struct Tower
    {
        public float hp;
        public float atk;
        public bool isActivity;
    }

    public static Tower[] tower = new Tower[TowerCount];

    public static float towerHP;
    public static float towerHP1;
    public static float towerATK;
    public static float [] towerHPe = new float[4];
    //public static float towerHP1;
    //public static float towerATK;



    // Start is called before the first frame update
    void Start()
    {
        towerHP = 1000;
        towerHP1 = 100;
        //towerHPe[0] = 1000;
        //towerHPe[1] = 100;
        //towerHPe[2] = 1000;
        //towerHPe[3] = 1000;

        
        //towerobj[0].SetActive(true);
        //towerobj[1].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void InitTower()
    {
        Towerprefab = GameObject.Find("Tower");

        for (int i = 0; i < TowerCount; ++i)
        {
            tower[i].hp = 1000;
            tower[i].atk = 10;
            tower[i].isActivity = true;
            towerobj[i] = Instantiate(Towerprefab);
            towerobj[i].SetActive(true);

        }
    }
}
