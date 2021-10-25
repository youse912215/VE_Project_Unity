using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ShowMenu;

public class MouseCollision : MonoBehaviour
{
    public static bool isColllider; //�R���C�_�[
    private GameObject obj; //�i�[�p�I�u�W�F�N�g
    public Material normalMtl; //�m�[�}���}�e���A��
    public Material activeMtl; //�A�N�V�������}�e���A��

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
		if (other.gameObject.tag == "Range")
        {
            isColllider = true; //�R���C�_�[��true
            ChangeRangeColor(other, activeMtl); //�F�ύX
        }
	}

    /// <summary>
    /// �}�E�X�|�C���^���A�E�C���X�͈͂��痣�ꂽ�Ƃ�
    /// </summary>
    void OnTriggerExit(Collider other) {
	    if (other.gameObject.tag == "Range")
        {
            isColllider = false; //�R���C�_�[��false
            ChangeRangeColor(other, normalMtl); //�F�ύX
        }
    }

    /// <summary>
    /// ���̃I�u�W�F�N�g�̃}�e���A�����w��̃}�e���A���J���[�ɕς���
    /// </summary>
    /// <param name="other">���̃I�u�W�F�N�g</param>
    /// <param name="mtl">�}�e���A��</param>
    private void ChangeRangeColor(Collider other, Material mtl)
    {
        obj = other.gameObject; //���Q�[���I�u�W�F�N�g���擾
        obj.GetComponent<Renderer>().material = mtl; //�}�e���A������
    }
}
