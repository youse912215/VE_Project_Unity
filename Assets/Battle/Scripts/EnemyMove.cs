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
    public static GameObject nearObj;         //�ł��߂��I�u�W�F�N�g
    //private float searchTime = 0;    //�o�ߎ���

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
        //�ł��߂������I�u�W�F�N�g���擾
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
        ////�o�ߎ��Ԃ��擾
        //searchTime += Time.deltaTime;

        //if (searchTime >= 1.0f)
        //{
        //    //�ł��߂������I�u�W�F�N�g���擾
        //    nearObj = serchTag(gameObject, "Target");

        //    //�o�ߎ��Ԃ�������
        //    searchTime = 0;
        //}

        ////�Ώۂ̈ʒu�̕���������
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




        //�������g�̈ʒu���瑊�ΓI�Ɉړ�����

    }



    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Target")
        {                   // �Փ˂�������̃^�O����"DamageArea"�Ȃ�
            if (hit.gameObject.GetComponent<EnemyAttack>())
            {       // �Փ˂������肪 C13_Status �R���|�[�l���g�������Ă���Ȃ�
                GetComponent<EnemyAttack>().damage(hit.gameObject.GetComponent<EnemyAttack>());   // HP�����炷
            }
        }
    }
    //�w�肳�ꂽ�^�O�̒��ōł��߂����̂��擾
    public static GameObject serchTag(GameObject nowObj, string Target)
    {
        float tmpDis = 0;           //�����p�ꎞ�ϐ�
        float nearDis = 0;          //�ł��߂��I�u�W�F�N�g�̋���
        //string nearObjName = "";    //�I�u�W�F�N�g����
        GameObject targetObj = null; //�I�u�W�F�N�g

        //�^�O�w�肳�ꂽ�I�u�W�F�N�g��z��Ŏ擾����
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(Target))
        {
            //���g�Ǝ擾�����I�u�W�F�N�g�̋������擾
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //�I�u�W�F�N�g�̋������߂����A����0�ł���΃I�u�W�F�N�g�����擾
            //�ꎞ�ϐ��ɋ������i�[
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
        //�ł��߂������I�u�W�F�N�g��Ԃ�
        //return GameObject.Find(nearObjName);
        return targetObj;
    }


    public static void moveEnemy(GameObject obj, GameObject Towerobj, float Sokudo)
    {

        //�ł��߂������I�u�W�F�N�g���擾
        nearObj = serchTag(obj, "Target");



        // Debug.Log(searchTime);
        //�Ώۂ̈ʒu�̕���������
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
