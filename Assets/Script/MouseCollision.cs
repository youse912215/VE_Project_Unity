using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ShowMenu;

public class MouseCollision : MonoBehaviour
{
    public static bool isColllider; //コライダー
    private GameObject obj; //格納用オブジェクト
    public Material normalMtl; //ノーマルマテリアル
    public Material activeMtl; //アクション時マテリアル

    // Start is called before the first frame update
    void Start()
    {
        isColllider = false; //コライダーをfalse
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ReturnOnScreenMousePos(); //スクリーン→ワールド変換
    }

    /// <summary>
    /// マウスポインタが、ウイルス範囲の中にいるとき
    /// </summary>
    void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Range")
        {
            isColllider = true; //コライダーをtrue
            ChangeRangeColor(other, activeMtl); //色変更
        }
	}

    /// <summary>
    /// マウスポインタが、ウイルス範囲から離れたとき
    /// </summary>
    void OnTriggerExit(Collider other) {
	    if (other.gameObject.tag == "Range")
        {
            isColllider = false; //コライダーをfalse
            ChangeRangeColor(other, normalMtl); //色変更
        }
    }

    /// <summary>
    /// 他のオブジェクトのマテリアルを指定のマテリアルカラーに変える
    /// </summary>
    /// <param name="other">他のオブジェクト</param>
    /// <param name="mtl">マテリアル</param>
    private void ChangeRangeColor(Collider other, Material mtl)
    {
        obj = other.gameObject; //他ゲームオブジェクトを取得
        obj.GetComponent<Renderer>().material = mtl; //マテリアルを代入
    }
}
