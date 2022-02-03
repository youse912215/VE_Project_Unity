using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static Call.CommonFunction;
using static MaterialManager;
using static Call.VirusData;
using static BlinkingEnemy;
using static VirusMaterialData;

public class EnemyCollision : MonoBehaviour
{
    private GameObject opponent; //相手（敵）オブジェクト格納用
    private GameObject obj; //オブジェクト
    private ActVirus actV; //スクリプト
    private float rangeActiveTime; //衝突カウント  
    private const float ACTIVE_COUNT = 10.0f; //アクティブカウント
    private const float WAIT_FOR_SECONDS = 0.5f; //待機時間
    private const float INCREASED_SECONDS = 0.1f; //増加時間

    public static bool cool; //クールダウン
    public static float cooldown; //クールダウンタイム
    public static bool isEnemyCollision; //衝突状態

    // Start is called before the first frame update
    void Start()
    {
        actV = GetOtherScriptObject<ActVirus>(obj); //ActVirusスクリプトを取得
        rangeActiveTime = 0.0f; //衝突カウントを0に
        isEnemyCollision = false; //衝突状態をfalse
        cool = false;
        cooldown = 0.0f;
    }

    /// <summary>
    /// 範囲に入っている間
    /// </summary>
    /// <param name="other">他のオブジェクト</param>
    void OnTriggerStay(Collider other)
    {
        if (actV.isGrabbedVirus) return; //ウイルスを持っているときは、処理をスキップ
        if (other.gameObject.tag != "Enemy") return; //敵以外は、処理をスキップ

        ChangeMaterialColor(this.gameObject, rangeMat[3]); //マテリアルカラーを変更
        opponent = other.gameObject; //範囲に入ったオブジェクトを格納
        isEnemyCollision = true; //衝突状態をtrue
        GetEnemyDamage(opponent); //敵のダメージを取得
        ChangeVirusEffect(opponent); //ウイルスのエフェクトを変更
        StartCoroutine(CountRangeTime()); //衝突時間をカウント

        if (rangeActiveTime <= ACTIVE_COUNT) return;
        var pObject = this.gameObject.transform.parent.gameObject; //親オブジェクトを格納
        actV.ExplosionVirus(gameObject.transform.parent.gameObject.transform.position); //ウイルス装置を爆発させる
        DecreaseCountVirus(pObject); //ウイルス数を減らす
        Destroy(pObject); //親オブジェクトを削除
        isEnemyCollision = false; //衝突状態をfalse
        rangeActiveTime = 0; //衝突カウントをリセット
        if (cooldown == 0.0f) cool = true; //クールダウン状態
    }

    /// <summary>
    /// ウイルス数を減らす
    /// </summary>
    /// <param name="pObject"></param>
    void DecreaseCountVirus(GameObject pObject)
    {
        if (!pObject) return; //例外はスキップ
        vSetCount[GetVirusNumber()]--; //設置数を減らす

        //タグが一致したとき
        if (pObject.tag == VirusTagName[GetVirusNumber()])
        {
            vCreationCount[GetVirusNumber()]--; //作成数を減らす
            isLimitCapacity[GetVirusNumber()] = false; //容量の限界状態を解除
        }
    }

    /// <summary>
    /// 範囲から離れたとき
    /// </summary>
    /// <param name="other">他のオブジェクト</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Enemy") return; //敵以外は、処理をスキップ
        ChangeMaterialColor(this.gameObject, rangeMat[0]); //マテリアルカラーを変更
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
        eH.takenDamage |= (uint)(0b0001 << GetVirusNumber()); //取得したウイルス番号分、シフトしてからORで代入演算
        eH.totalDamage = (float)eH.CulculationHealth(eH.takenDamage); //計算したダメージをトータル値として格納
    }

    /// <summary>
    /// ウイルスエフェクトを変更
    /// </summary>
    /// <param name="obj">敵オブジェクト</param>
    private void ChangeVirusEffect(GameObject obj)
    {
        var ps = obj.GetComponentsInChildren<ParticleSystem>(); //範囲に入った敵のパーティクルを取得
        var renderer = obj.GetComponentsInChildren<ParticleSystemRenderer>(); // //範囲に入った敵のパーティクルレンダラーを取得
        SetMaterials(obj);
        renderer[0].material = actV.defaultPs; //マテリアルをウイルスの種類によって変更
        ps[0].Play(); //パーティクル発生
        renderer[1].material = actV.defaultPs; //マテリアルをウイルスの種類によって変更
        ps[1].Play();
    }

    /// <summary>
    /// 範囲効果時間をカウント
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountRangeTime()
    {
        yield return new WaitForSeconds(WAIT_FOR_SECONDS);
        rangeActiveTime += INCREASED_SECONDS; //カウント開始   
    }
}
