using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerData;
using static EnemyData;
using static CalculationManager;

public class EnemyMove : MonoBehaviour
{
    //public GameObject target;
    //[SerializeField]
    //private GameObject[] targets;
    //public float speed;
    public static GameObject nearObj;         //最も近いオブジェクト
    //private float searchTime = 0;    //経過時間

    const float MoveSpeed = 1.0f;
    Vector3[] towerposi = new Vector3[TowerCount];
    Vector3[] enemyposi = new Vector3[EnemyCount];
    //float Sokudo;


    //GameObject Towerobj;
    //GameObject Towerobj1;
    //TowerData Tow;

    //GameObject EnemyD;
    //TowerData EneD;



    // Use this for initialization
    void Start()
    {
        //最も近かったオブジェクトを取得
        //nearObj = serchTag(gameObject, "Target");
        //Towerobj = GameObject.Find("Tower");
        //Towerobj1 = GameObject.Find("Tower1");
        //towerHP = 5;


        //enemyATK = 5;
    }

    // Update is called once per frame
    void Update()
    {
        distanse(TowerCount, EnemyCount, towerobj, enemyobj);
        //Debug.Log("aaa" + towerHP);
        ////経過時間を取得
        //searchTime += Time.deltaTime;

        //if (searchTime >= 1.0f)
        //{
        //    //最も近かったオブジェクトを取得
        //    nearObj = serchTag(gameObject, "Target");

        //    //経過時間を初期化
        //    searchTime = 0;
        //}

        ////対象の位置の方向を向く
        //enemyobj[0,0].transform.LookAt(nearObj.transform);




        //transform.position = Vector3.MoveTowards(transform.position, targets.transform.position, speed);
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        //if (GameManager.distance[0] >= 15.0f)
        //{
        //    Sokudo = MoveSpeed;

        //}
        //else 
        //{
        //    Sokudo = 0;
        //    towerHP = towerHP - enemyATK;


        //    if (towerHP <= 0)
        //    {
        //        Towerobj.SetActive(false);
        //    } 
        //}

        //if (GameManager.distance[1] >= 15.0f)
        //{
        //    Sokudo = MoveSpeed;

        //}
        //else
        //{
        //    Sokudo = 0;
        //    towerHP1 = towerHP1 - enemyATK;


        //    if (towerHP1 <= 0)
        //    {
        //        Towerobj1.SetActive(false);
        //    }
        //}
        //enemyobj[0,0].transform.Translate(Vector3.forward * Sokudo);




        //自分自身の位置から相対的に移動する

    }



    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Target")
        {                   // 衝突した相手のタグ名が"DamageArea"なら
            if (hit.gameObject.GetComponent<EnemyAttack>())
            {       // 衝突した相手が C13_Status コンポーネントを持っているなら
                GetComponent<EnemyAttack>().damage(hit.gameObject.GetComponent<EnemyAttack>());   // HPを減らす
            }
        }
    }
    //指定されたタグの中で最も近いものを取得
    public static GameObject serchTag(GameObject nowObj, string Target)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(Target))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }
            if (nearDis < 30)
            {

            }
        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }


    public static void moveEnemy(GameObject obj, GameObject Towerobj, float Sokudo)
    {

        //最も近かったオブジェクトを取得
        nearObj = serchTag(obj, "Target");



        // Debug.Log(searchTime);
        //対象の位置の方向を向く
        obj.transform.LookAt(nearObj.transform);





        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        //if (kyori[0,0] >= 15.0f)
        //{
        //    Sokudo = MoveSpeed;

        //}
        //else
        //{
        //    Sokudo = 0;
        //    towerHP = towerHP - enemyATK;


        //    if (towerHP <= 0)
        //    {
        //        Towerobj.SetActive(false);
        //    }
        //}

        //if (kyori[0,1] >= 15.0f)
        //{
        //    Sokudo = MoveSpeed;

        //}
        //else
        //{
        //    Sokudo = 0;
        //    towerHP1 = towerHP1 - enemyATK;


        //    if (towerHP1 <= 0)
        //    {
        //        Towerobj1.SetActive(false);
        //    }
        /*/}
        /for (int i = 0; i < TowerCount; ++i)
        {
            for (int j = 0; j < EnemyCount; ++j)
            {
                if (kyori[i, j] >= 15.0f)
                {
                    Sokudo = MoveSpeed;
                    //Debug.Log("if");
                    Debug.Log(Sokudo);
                }
                else //if (kyori[i, j] <= 15.0f)
                {
                    Sokudo = 0;
                    tower[i].hp = tower[i].hp - enemy[0, j].atk;
                    Debug.Log("else");
                    Debug.Log(kyori[0, 0]);


                    if (tower[i].hp <= 0)
                    {
                        Towerobj.SetActive(false);
                    }
                }

            }
             //obj.transform.position = Vector3.MoveTowards(obj.transform.position, nearObj.transform.position, Sokudo);
          
        }*/
        for (int i = 0; i < TowerCount; ++i)
        {
            for (int j = 0; j < EnemyCount; ++j)
            {
                enemyobj[i, j].transform.Translate(Vector3.forward * enemy[i, j].speed);
                if (kyori[i, j] <= 15.0f)
                {
                    enemy[i, j].speed = 0.0f;

                }
            }
        }
    }
}
