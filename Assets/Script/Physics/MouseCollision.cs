using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ShowMenu;
using static MaterialManager;
using static Call.CommonFunction;

public class MouseCollision : MonoBehaviour
{
    public static bool isMouseCollider; //マウスコライダー
    public static bool isRangeCollision; //範囲コリジョン

    public static GameObject rangeObj; //格納用オブジェクト

    // Start is called before the first frame update
    void Start()
    {
        isMouseCollider = false; //マウスコライダーをfalse
        isRangeCollision = false; //範囲コリジョンをfalse
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        transform.position = ReturnOnScreenMousePos(); //スクリーン→ワールド変換
    }

    /// <summary>
    /// マウスポインタが、ウイルス範囲の中にいるとき
    /// </summary>
    void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != "Range") return;

        isMouseCollider = true; //コライダーをtrue
        rangeObj = other.gameObject;
        if (isRangeCollision) ChangeMaterialColor(rangeObj, rangeMat[2]);
        else ChangeMaterialColor(rangeObj, rangeMat[1]); //色変更
	}

    /// <summary>
    /// マウスポインタが、ウイルス範囲から離れたとき
    /// </summary>
    void OnTriggerExit(Collider other) {
	    if (other.gameObject.tag != "Range") return;

            rangeObj = other.gameObject;
            isMouseCollider = false; //コライダーをfalse
            ChangeMaterialColor(rangeObj, rangeMat[0]); //色変更

    }

    
}
