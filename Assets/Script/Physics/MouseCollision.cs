using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ShowMenu;
using static MaterialManager;
using static Call.CommonFunction;

public class MouseCollision : MonoBehaviour
{
    public static bool isMouseCollider; //�}�E�X�R���C�_�[
    public static bool isRangeCollision; //�͈̓R���W����

    public static GameObject rangeObj; //�i�[�p�I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        isMouseCollider = false; //�}�E�X�R���C�_�[��false
        isRangeCollision = false; //�͈̓R���W������false
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

        isMouseCollider = true; //�R���C�_�[��true
        rangeObj = other.gameObject;
        if (isRangeCollision) ChangeRangeColor(rangeObj, mat[2]);
        else ChangeRangeColor(rangeObj, mat[1]); //�F�ύX
	}

    /// <summary>
    /// �}�E�X�|�C���^���A�E�C���X�͈͂��痣�ꂽ�Ƃ�
    /// </summary>
    void OnTriggerExit(Collider other) {
	    if (other.gameObject.tag != "Range") return;

            rangeObj = other.gameObject;
            isMouseCollider = false; //�R���C�_�[��false
            ChangeRangeColor(rangeObj, mat[0]); //�F�ύX

    }

    
}
