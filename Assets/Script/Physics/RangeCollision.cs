using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.CommonFunction;
using static MaterialManager;
using static MouseCollision;

public class RangeCollision : MonoBehaviour
{
    private GameObject obj; //格納用オブジェクト

    // Update is called once per frame
    void Update()
    {
        //位置を親と紐づけ
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
    }

    /// <summary>
    /// 触れているとき
    /// </summary>
    /// <param name="other">他のオブジェクトとのコライダー</param>
    void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != "Range") return;
            obj = other.gameObject;
            ChangeMaterialColor(obj, rangeMat[2]);
            isRangeCollision = true;
	}

    /// <summary>
    /// 離れたとき
    /// </summary>
    /// <param name="other">他のオブジェクトとのコライダー</param>
    void OnTriggerExit(Collider other) {
	    if (other.gameObject.tag != "Range") return;
            obj = other.gameObject; 
            ChangeMaterialColor(obj, rangeMat[0]);
            isRangeCollision = false;
    }
}
