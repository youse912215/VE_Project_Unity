using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ShowMenu;
using static RangeCollision;
using static MaterialManager;
using static Call.CommonFunction;

public class MouseCollision : MonoBehaviour
{
    public static bool isColllider; //コライダー
    private GameObject obj; //格納用オブジェクト

    public static GameObject vObj;

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
		if (other.gameObject.tag != "Range") return;
            isColllider = true; //コライダーをtrue
            if (isCollisionTrigger)
            {
                ChangeRangeColor(obj, other, mat[2]);
            }
            else ChangeRangeColor(obj, other, mat[1]); //色変更
            vObj = other.gameObject;
	}

    /// <summary>
    /// マウスポインタが、ウイルス範囲から離れたとき
    /// </summary>
    void OnTriggerExit(Collider other) {
	    if (other.gameObject.tag != "Range") return;

            isColllider = false; //コライダーをfalse
            ChangeRangeColor(obj, other, mat[0]); //色変更

    }
}
