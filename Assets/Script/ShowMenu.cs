using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.ConstantValue;
using static Call.ConstantValue.MENU_TYPE;

public class ShowMenu : MonoBehaviour
{
    public static Vector3 mousePos; //�}�E�X���W
    public static Camera cam; //�J�����I�u�W�F�N�g
    
    private const int MENU_TYPE = 5;
    public static bool[] isMenuFlag = new bool[MENU_TYPE];
    public static bool[] menuMode = new bool[MENU_TYPE];
    public static GameObject[] buttonObj = new GameObject[MENU_TYPE];

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //���C���J�����擾
        transform.position = INIT_MENU_POS; //�����ʒu�i��ʊO�j

        for (int i = 0; i < MENU_TYPE; i++){
            buttonObj[i] = transform.GetChild(i).gameObject;
            buttonObj[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMenuFlag[(int)SET]) SetMenu(); //���j���[���Z�b�g
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

    public static void SetButtonActive(bool set, bool del, bool mov, bool dta, bool bac)
    {
        buttonObj[(int)SET].SetActive(set);
        buttonObj[(int)DELETE].SetActive(del);
        buttonObj[(int)MOVE].SetActive(mov);
        buttonObj[(int)DETAIL].SetActive(dta);
        buttonObj[(int)BACK].SetActive(bac);
    }

    /// <summary>
    /// ���j���[���J��
    /// </summary>
    public static void OpenSetMenu()
    {
        SetButtonActive(true, true, false, false, true);
        ReverseMenuFlag(SET); //���j���[���J��
    }
    public static void OpenAfterMenu()
    {
        SetButtonActive(false, true, true, true, true);
        ReverseMenuFlag(SET); //���j���[���J��
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
        ReverseMenuFlag(SET); //���j���[�����
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
