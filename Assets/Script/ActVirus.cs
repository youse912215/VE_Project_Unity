using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.CommonFunction;
using static Call.ConstantValue.MENU_TYPE;
using static ShowMenu;
using static MouseCollision;

public class ActVirus : MonoBehaviour
{
    private Virus[,] virus = new Virus[CATEGORY, OWNED]; //�E�C���X�\���̔z��
    private GameObject[,] virusObj = new GameObject[CATEGORY, OWNED]; //�E�C���X�I�u�W�F�N�g�z��
    private GameObject vPrefab; //�E�C���X�v���t�@�u

    private bool isGrabbedVirus; //�E�C���X��͂�ł��邩
    private Vector3 worldPos; //���[���h���W
    private int buttonMode; //�{�^���̏�ԁi�E�C���X�̎�ށj
    private bool isOpenMenu;

    private int storage;

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

    // Update is called once per frame
    void Update()
    {
        worldPos = ReturnOnScreenMousePos(); //�X�N���[�������[���h�ϊ� //�E�C���X���ړ�

        Debug.Log("�E�C���X�ԍ�::" + storage);
        Debug.Log("Flag" + isGrabbedVirus);

        if (!isGrabbedVirus && isMouseCollider && Input.GetMouseButtonDown(1)){
            OpenAfterMenu(); //�ݒu�チ�j���[���J��
            isOpenMenu = true;
            SearchVirusArray();
        }

        if (virus[buttonMode, vNum[buttonMode]].isActivity)
            virusObj[buttonMode, vNum[buttonMode]].transform.position = worldPos; //�E�C���X�I�u�W�F�N�g�̈ʒu�����[���h���W�ōX�V����
        if (!isGrabbedVirus) return;
        OpenVirusMenu(); //�E�C���X���j���[���J��
    }

    /// <summary>
    /// Pushing any buttons
    /// </summary>
    /// <param name="n">�E�C���X�̎�ށi�ԍ��j</param>
    public void VirusButtonPush(int n)
    {
        if (isOpenMenu) return; //���j���[�J���Ă���Ƃ��̓X�L�b�v
        if (isGrabbedVirus && buttonMode != n) return; //�E�C���X�������Ă���Ƃ����A�ʂ̃{�^���̏�Ԃ̂Ƃ��̓X�L�b�v
        if (ProcessOutOfRange(n)) return; //��O�����Ȃ�X�L�b�v

        isGrabbedVirus = ReverseFlag(isGrabbedVirus); //�t���O�𔽓]���āA�X�V����

        if (isGrabbedVirus) CreateVirus(n); //�E�C���X���쐬����
        else DestroyBeforeVirus(); // �E�C���X���폜����
    }

    /// <summary>
    /// Pushing set button
    /// </summary>
    public void SetButtonPush()
    {
        //if (!isGrabbedVirus) return;
        SetVirus(); //�E�C���X��ݒu
        ReverseMenuFlag(BACK); //���j���[�����
        isOpenMenu = false;
    }

    /// <summary>
    /// Pushing back button
    /// </summary>
    public void BackButtonPush()
    {
        //if (!isGrabbedVirus) return;
        virus[buttonMode, vNum[buttonMode]].isActivity = true; //�ĂуA�N�e�B�u��Ԃ�
        ReverseMenuFlag(BACK); //���j���[�����
        isOpenMenu = false;
    }

    /// <summary>
    /// pushing delete button
    /// </summary>
    public void DeleteButtonPush()
    {
        //if (!isGrabbedVirus) return;
        if (!menuMode)
        {
            DestroyBeforeVirus(); // �E�C���X���폜����
            isGrabbedVirus = ReverseFlag(isGrabbedVirus);
        }
        else Destroy(virusObj[storage / OWNED, storage % OWNED]);

        ReverseMenuFlag(BACK); //���j���[�����
        isOpenMenu = false;
    }

    /// <summary>
    /// �E�C���X���쐬����
    /// </summary>
    /// <param name="n">�E�C���X�̎�ށi�ԍ��j</param>
    public void CreateVirus(int n)
    {
        GenerationVirus(virus, (VIRUS_NUM)n); //�E�C���X����
        vPrefab = GameObject.Find(VirusName + n.ToString()); //�E�C���X�ԍ��ɍ��v����Prefab���擾
        virusObj[n, vNum[n]] = Instantiate(vPrefab); //�Q�[���I�u�W�F�N�g�𐶐�
        virusObj[n, vNum[n]].SetActive(true); //�A�N�e�B�u��Ԃɂ���
        buttonMode = n; //���݂̃{�^���̏�Ԃ��X�V
    }

    /// <summary>
    /// �E�C���X���폜����
    /// </summary>
    private void DestroyBeforeVirus()
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
        if (isRangeCollision) return;
        if (isOpenMenu) return;

        if (Input.GetMouseButtonDown(1))
        {
            OpenBeforeMenu(); //���j���[���J��
            SaveVirusPosition(virus, (VIRUS_NUM)buttonMode, virusObj, worldPos); //�ݒu�����E�C���X���W��ۑ�
            isOpenMenu = true;
        }
    }

    private void SearchVirusArray()
    {
        Virus[] ary = virus.Cast<Virus>().ToArray(); //�z����ꎟ����
        for (int i = 0; i < CATEGORY * OWNED; ++i)
        {
            //�e�E�C���X�̍��W�Ɣ͈̓I�u�W�F�N�g�̍��W����v����܂ŌJ��Ԃ�
            if (ary[i].pos == rangeObj.transform.position)
            {
                storage = i; //����̗v�f���i�[
                break; //�J��Ԃ����甲���o��
            }
        }
    }
}
