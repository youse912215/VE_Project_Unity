using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.CommonFunction;
using static MaterialManager;
using static MouseCollision;

public class RangeCollision : MonoBehaviour
{
    private GameObject obj; //�i�[�p�I�u�W�F�N�g

    // Update is called once per frame
    void Update()
    {
        //�ʒu��e�ƕR�Â�
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
    }

    /// <summary>
    /// �G��Ă���Ƃ�
    /// </summary>
    /// <param name="other">���̃I�u�W�F�N�g�Ƃ̃R���C�_�[</param>
    void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != "Range") return;
            obj = other.gameObject;
            ChangeMaterialColor(obj, rangeMat[2]);
            isRangeCollision = true;
	}

    /// <summary>
    /// ���ꂽ�Ƃ�
    /// </summary>
    /// <param name="other">���̃I�u�W�F�N�g�Ƃ̃R���C�_�[</param>
    void OnTriggerExit(Collider other) {
	    if (other.gameObject.tag != "Range") return;
            obj = other.gameObject; 
            ChangeMaterialColor(obj, rangeMat[0]);
            isRangeCollision = false;
    }
}
