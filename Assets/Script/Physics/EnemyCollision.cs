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
    private GameObject enemyObj; //敵オブジェクト格納用

    private GameObject obj; //オブジェクト
    private ActVirus actV; //スクリプト
    private float cCount; //衝突カウント
    private bool isCollision; //衝突状態
    private const float ACTIVE_COUNT = 10.0f; //アクティブカウント

    // Start is called before the first frame update
    void Start()
    {
        actV = GetOtherScriptObject<ActVirus>(obj); //ActVirusスクリプトを取得
        cCount = 0; //衝突カウントを0に
        isCollision = false; //衝突状態をfalse
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

        ChangeMaterialColor(this.gameObject, rangeMat[3]); //マテリアルカラーを変更
        enemyObj = other.gameObject; //範囲に入ったオブジェクトを格納
        isCollision = true; //衝突状態をtrue
        GetEnemyDamage(enemyObj); //敵のダメージを取得
        ChangeVirusEffect(enemyObj); //ウイルスのエフェクトを変更
    }

    /// <summary>
    /// 範囲から離れたとき
    /// </summary>
    /// <param name="other">他のオブジェクト</param>
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != "Enemy") return; //敵以外は、処理をスキップ

        ChangeMaterialColor(this.gameObject, rangeMat[0]); //マテリアルカラーを変更
        
    }

    /// <summary>
    /// 衝突カウントを計算し、ウイルスを消す
    /// </summary>
    private void CountCollisionTime()
    {
        cCount += 0.1f; //カウント開始

        if (cCount <= ACTIVE_COUNT) return; //

        this.gameObject.transform.parent.gameObject.SetActive(false); //範囲を非アクティブ状態に
        cCount = 0; //衝突カウントをリセット
        isCollision = false; //衝突状態をfalse
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

    /// <summary>
    /// 敵のダメージを取得
    /// </summary>
    /// <param name="obj">敵オブジェクト</param>
    private void GetEnemyDamage(GameObject obj)
    {
        EnemyHealth eH; //EenemyHealthスクリプト
        eH = obj.GetComponent<EnemyHealth>(); //スクリプトを取得
        eH.isInfection = true; //感染状態をtrue
        eH.damage |= (uint)(0b0001 << GetVirusNumber()); //取得したウイルス番号分、シフトしてからORで代入演算
        eH.total = (float)eH.CulculationHealth(eH.damage); //計算したダメージをトータル値として格納
    }

    /// <summary>
    /// ウイルスエフェクトを変更
    /// </summary>
    /// <param name="obj">敵オブジェクト</param>
    private void ChangeVirusEffect(GameObject obj)
    {
        var ps = obj.GetComponentsInChildren<ParticleSystem>(); //範囲に入った敵のパーティクルを取得
        var renderer = obj.GetComponentsInChildren<ParticleSystemRenderer>(); // //範囲に入った敵のパーティクルレンダラーを取得
        renderer[GetVirusNumber()].material = virusMat[GetVirusNumber()]; //マテリアルをウイルスの種類によって変更
        ps[GetVirusNumber()].Play(); //パーティクル発生 
    }
}
