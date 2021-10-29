using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ShowMenu;
using static RangeCollision;
using static MaterialManager;
using static Call.CommonFunction;

public class MouseCollision : MonoBehaviour
{
    public static bool isColllider; //�R���C�_�[
    private GameObject obj; //�i�[�p�I�u�W�F�N�g

    public static GameObject vObj;

    // Start is called before the first frame update
    void Start()
    {
        isColllider = false; //�R���C�_�[��false
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ReturnOnScreenMousePos(); //�X�N���[�������[���h�ϊ�
    }

    /// <summary>
    /// �}�E�X�|�C���^���A�E�C���X�͈͂̒��ɂ���Ƃ�
    /// </summary>
    void OnTriggerStay(Collider other) {
		if (other.gameObject.tag != "Range") return;
            isColllider = true; //�R���C�_�[��true
            if (isCollisionTrigger)
            {
                ChangeRangeColor(obj, other, mat[2]);
            }
            else ChangeRangeColor(obj, other, mat[1]); //�F�ύX
            vObj = other.gameObject;
	}

    /// <summary>
    /// �}�E�X�|�C���^���A�E�C���X�͈͂��痣�ꂽ�Ƃ�
    /// </summary>
    void OnTriggerExit(Collider other) {
	    if (other.gameObject.tag != "Range") return;

            isColllider = false; //�R���C�_�[��false
            ChangeRangeColor(obj, other, mat[0]); //�F�ύX

    }
}
