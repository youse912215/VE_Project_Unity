using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.CommonFunction;
using static Call.ConstantValue.MENU_TYPE;
using static ShowMenu;


public class ActVirus : MonoBehaviour
{
    private Virus[,] virus = new Virus[CATEGORY, OWNED]; //�E�C���X�\���̔z��
    private GameObject[,] virusObj = new GameObject[CATEGORY, OWNED]; //�E�C���X�I�u�W�F�N�g�z��
    private GameObject prefab; //�E�C���X�v���t�@�u

    private bool isGrabbedVirus; //�E�C���X��͂�ł��邩
    private Vector3 worldPos; //���[���h���W
    private int buttonMode; //�{�^���̏�ԁi�E�C���X�̎�ށj

    // Start is called before the first frame update
    void Start()
    {
        /* �E�C���X���̏����� */
        InitValue(virus, CODE_CLD);
        InitValue(virus, CODE_INF);
        InitValue(virus, CODE_19);
        
        isGrabbedVirus = false;
        buttonMode = 0;
    }

    /// <summary>
    /// Pushing any buttons
    /// </summary>
    /// <param name="n">�E�C���X�̎�ށi�ԍ��j</param>
    public void VirusButtonPush(int n)
    {
        if (isGrabbedVirus && buttonMode != n) return;

        if (ProcessOutOfRange(n)) return;
        isGrabbedVirus = ReverseFlag(isGrabbedVirus);

        if (isGrabbedVirus) CreateVirus(n); //�E�C���X���쐬����
        else DestroyVirus(); // �E�C���X���폜����
    }

    /// <summary>
    /// Pushing set button
    /// </summary>
    public void SetButtonPush()
    {
        if (!isGrabbedVirus) return;
        SetVirus(); //�E�C���X��ݒu
        ReverseMenuFlag(BACK); //���j���[�����
    }

    /// <summary>
    /// Pushing back button
    /// </summary>
    public void BackButtonPush()
    {
        if (!isGrabbedVirus) return;
        virus[buttonMode, vNum[buttonMode]].isActivity = true; //�ĂуA�N�e�B�u��Ԃ�
        ReverseMenuFlag(BACK); //���j���[�����
    }

    /// <summary>
    /// pushing delete button
    /// </summary>
    public void DeleteButtonPush()
    {
        if (!isGrabbedVirus) return;
        DestroyVirus(); // �E�C���X���폜����
        isGrabbedVirus = ReverseFlag(isGrabbedVirus);
        ReverseMenuFlag(BACK); //���j���[�����
    }

    // Update is called once per frame
    void Update()
    {
        worldPos = ReturnOnScreenMousePos(); //�X�N���[�������[���h�ϊ� //�E�C���X���ړ�

        //�E�C���X�I�u�W�F�N�g�̈ʒu�����[���h���W�ōX�V����
        if (virus[buttonMode, vNum[buttonMode]].isActivity)
            virusObj[buttonMode, vNum[buttonMode]].transform.position = worldPos;

        if (!isGrabbedVirus) return;
        OpenVirusMenu(); //�E�C���X���j���[���J��
    }

    /// <summary>
    /// �E�C���X���쐬����
    /// </summary>
    /// <param name="n">�E�C���X�̎�ށi�ԍ��j</param>
    public void CreateVirus(int n)
    {
        GenerationVirus(virus, (VIRUS_NUM)n); //�E�C���X����
        prefab = GameObject.Find(VirusName + n.ToString()); //�E�C���X�ԍ��ɍ��v����Prefab���擾
        virusObj[n, vNum[n]] = Instantiate(prefab); //�Q�[���I�u�W�F�N�g�𐶐�
        virusObj[n, vNum[n]].SetActive(true); //�A�N�e�B�u��Ԃɂ���
        buttonMode = n; //���݂̃{�^���̏�Ԃ��X�V
    }

    /// <summary>
    /// �E�C���X���폜����
    /// </summary>
    private void DestroyVirus()
    {
        virus[buttonMode, vNum[buttonMode]].isActivity = false; //������Ԃ�false
        Destroy(virusObj[buttonMode, vNum[buttonMode]]); //�Q�[���I�u�W�F�N�g���폜
    }

    /// <summary>
    /// �E�C���X��ݒu
    /// </summary>
    private void SetVirus()
    {
        GetVirus((VIRUS_NUM)buttonMode); //�E�C���X���A�N�e�B�u�ɂ���
        isGrabbedVirus = false; //�{�^�������false
    }

    /// <summary>
    /// �E�C���X���j���[���J��
    /// </summary>
    private void OpenVirusMenu()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OpenMenu(); //���j���[���J��
            SaveVirusPosition(virus, (VIRUS_NUM)buttonMode, virusObj, worldPos); //�ݒu�����E�C���X���W��ۑ�
        }
    }
}
