using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.ConstantValue;
using static Call.ConstantValue.MENU_TYPE;

public class ShowMenu : MonoBehaviour
{
    public static Vector3 mousePos; //�}�E�X���W
    public static Camera cam; //�J�����I�u�W�F�N�g

    public static bool isOpenMenu;
    private const int MENU_TYPE = 5;
    public static bool[] isMenuFlag = new bool[MENU_TYPE];
    public static bool[] menuMode = new bool[MENU_TYPE];

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //���C���J�����擾
        transform.position = INIT_MENU_POS; //�����ʒu�i��ʊO�j
    }

    // Update is called once per frame
    void Update()
    {
        if (isMenuFlag[(int)OPEN]) SetMenu(); //���j���[���Z�b�g
        if (isMenuFlag[(int)BACK]) RemoveMenu(); //���j���[�������[�u
    }

    /// <summary>
    /// �X�N���[����̃}�E�X���W��Ԃ�
    /// </summary>
    public static Vector3 ReturnOnScreenMousePos()
    {
        mousePos = Input.mousePosition; //�}�E�X�̍��W���擾
        mousePos.z = CAM_DISTANCE; //�}�E�Xz���W�ɃJ�����Ƃ̋�������
        return cam.ScreenToWorldPoint(mousePos); //�X�N���[�������[���h�ϊ�
    }

    /// <summary>
    /// ���j���[���J��
    /// </summary>
    public static void OpenMenu()
    {
        ReverseMenuFlag(OPEN); //���j���[���J��
    }

    /// <summary>
    /// ���j���[���Z�b�g
    /// </summary>
    private void SetMenu()
    {    
        transform.position = new Vector3(
            mousePos.x + DIFF_X,
            mousePos.y + DIFF_Y,
            transform.position.z
        ); //���j���[�̈ʒu���}�E�X�̉E���ɔz�u
        ReverseMenuFlag(OPEN); //���j���[�����
    }

    /// <summary>
    /// ���j���[�������[�u
    /// </summary>
    private void RemoveMenu()
    {
        transform.position = INIT_MENU_POS; //�����ʒu�i��ʊO�j
        ReverseMenuFlag(BACK); //���j���[�����
    }

    /// <summary>
    /// ���j���[�t���O�𔽓]������
    /// </summary>
    /// <param name="type">���j���[�̎��</param>
    public static void ReverseMenuFlag(MENU_TYPE type)
    {
        isMenuFlag[(int)type] = !isMenuFlag[(int)type] ? true : false;
    }
}
