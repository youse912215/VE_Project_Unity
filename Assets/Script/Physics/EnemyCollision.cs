using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static Call.CommonFunction;
using static MaterialManager;
using static ActVirus;
using static WarriorData;
using static Call.VirusData;

public class EnemyCollision : MonoBehaviour
{
    private GameObject enemyObj;

    private GameObject obj;
    private ActVirus actV;
    private float cCount;
    private bool isCollision;
    private const float ACTIVE_COUNT = 30.0f;

    GameObject[] eList = new GameObject[ALL_ENEMEY_MAX * E_CATEGORY];

    // Start is called before the first frame update
    void Start()
    {
        actV = GetOtherScriptObject<ActVirus>(obj);
        cCount = 0;
        isCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCollision) return;
        CountCollisionTime();
    }

    /// <summary>
    /// 範囲に入っている間
    /// </summary>
    /// <param name="other">他のオブジェクト</param>
    void OnTriggerStay(Collider other) {
        if (actV.isGrabbedVirus) return; //ウイルスを持っているときは、処理をスキップ
		if (other.gameObject.tag != "Enemy") return; //敵以外は、処理をスキップ

        ChangeMaterialColor(this.gameObject, rangeMat[3]);
        enemyObj = other.gameObject;
        isCollision = true;

        EnemyHealth eH;
        eH = enemyObj.GetComponent<EnemyHealth>();
        eH.isInfection = true;
        eH.damage |= (uint)(0b0001 << GetVirusNumber());
        Debug.Log(eH.damage);
        eH.total = (float)eH.CulculationHealth(eH.damage);
        

        var ps = enemyObj.GetComponentsInChildren<ParticleSystem>(); //範囲に入った敵のパーティクルを取得
        var renderer = enemyObj.GetComponentsInChildren<ParticleSystemRenderer>(); // //範囲に入った敵のパーティクルレンダラーを取得
        renderer[GetVirusNumber()].material = virusMat[GetVirusNumber()]; //マテリアルをウイルスの種類によって変更
        ps[GetVirusNumber()].Play(); //パーティクル発生      
    }

    /// <summary>
    /// 範囲から離れたとき
    /// </summary>
    /// <param name="other">他のオブジェクト</param>
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != "Enemy") return;
        ChangeMaterialColor(this.gameObject, rangeMat[0]);
        
    }

    /// <summary>
    /// 衝突カウントを計算し、ウイルスを消す
    /// </summary>
    private void CountCollisionTime()
    {
        cCount += 0.1f; //カウント開始

        if (cCount <= ACTIVE_COUNT) return;
        this.gameObject.transform.parent.gameObject.SetActive(false);
        cCount = 0;
        isCollision = false;
    }

    /// <summary>
    /// ウイルスの種類を取得
    /// </summary>
    /// <returns></returns>
    private int GetVirusNumber()
    {
        int n = 0; //格納用変数
        //ウイルスタグの値と要素番号を返し、繰り返す
        foreach (var (value, index) in VirusTagName.Select((value, index) => (value, index)))
        {
            //タグと一致したとき
            if (value == this.gameObject.transform.parent.tag)
            {
                n = index; //要素番号を格納
                break; //ループを抜け出す
            }
        }        
        return n;
    }
}
