using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.CommonFunction;
using static Call.ConstantValue.MENU_TYPE;
using static ShowMenu;
using static MouseCollision;

public class ActVirus : MonoBehaviour
{
    private VirusChildren[,] vChildren = new VirusChildren[CATEGORY, OWNED]; //�E�C���X�\���̔z��
    private GameObject[,] vObject = new GameObject[CATEGORY, OWNED]; //�E�C���X�I�u�W�F�N�g�z��
    private GameObject vPrefab; //�E�C���X�v���t�@�u

    private bool isGrabbedVirus; //�E�C���X��͂�ł��邩
    private Vector3 worldPos; //���[���h���W
    private int buttonMode; //�{�^���̏�ԁi�E�C���X�̎�ށj
    private bool isOpenMenu;

    private int storage;

   
    private VirusParents[] vParents = new VirusParents[CATEGORY];
    

    // Start is called before the first frame update
    void Start()
    {
        /* �E�C���X���̏����� */
        InitValue(vParents, vChildren, CODE_CLD);
        InitValue(vParents, vChildren, CODE_INF);
        InitValue(vParents, vChildren, CODE_19);
        
        isGrabbedVirus = false;
        buttonMode = 0;

        for (int i = 0; i < CATEGORY; ++i)
            vParents[i].tag = VirusTagName[i];
        vParents[0].creationCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        worldPos = ReturnOnScreenMousePos(); //�X�N���[�������[���h�ϊ� //�E�C���X���ړ�

        vParents[0].obj = GameObject.FindGameObjectsWithTag(vParents[0].tag);
        vParents[0].setCount = vParents[0].obj.Length - 1;
        //Debug.Log("�E�C���X��::" + vParents[0].setCount);
        Debug.Log("storage::" + storage);


        if (!isGrabbedVirus && isMouseCollider && Input.GetMouseButtonDown(1)){
            OpenAfterMenu(); //�ݒu�チ�j���[���J��
            isOpenMenu = true;
            SearchVirusArray();
        }

        if (vParents[buttonMode].setCount == 0) return;
        if (vChildren[buttonMode, /*vNum[buttonMode]*/vParents[buttonMode].setCount - 1].isActivity)
            vObject[buttonMode, /*vNum[buttonMode]*/vParents[buttonMode].setCount - 1].transform.position = worldPos; //�E�C���X�I�u�W�F�N�g�̈ʒu�����[���h���W�ōX�V����
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
        if (isLimitCapacity[n]) return; //��O�����Ȃ�X�L�b�v

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
        vChildren[buttonMode, /*vNum[buttonMode]*/vParents[buttonMode].setCount - 1].isActivity = true; //�ĂуA�N�e�B�u��Ԃ�
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
        else
        {
            int column = storage / OWNED;
            int row = storage % OWNED;
            if (vParents[column].setCount == vParents[column].creationCount)
                isLimitCapacity[column] = false;
            Debug.Log("aaa:::" + vObject[column, row].activeInHierarchy);
            Destroy(vObject[column, row]); 
        }

        ReverseMenuFlag(BACK); //���j���[�����
        isOpenMenu = false;
    }

    /// <summary>
    /// �E�C���X���쐬����
    /// </summary>
    /// <param name="n">�E�C���X�̎�ށi�ԍ��j</param>
    public void CreateVirus(int n)
    {
        GenerationVirus(vParents, vChildren, (VIRUS_NUM)n); //�E�C���X����
        vPrefab = GameObject.Find(VirusHeadName + n.ToString()); //�E�C���X�ԍ��ɍ��v����Prefab���擾
        vObject[n, /*vNum[n]*/vParents[n].setCount] = Instantiate(vPrefab); //�Q�[���I�u�W�F�N�g�𐶐�
        vObject[n, /*vNum[n]*/vParents[n].setCount].SetActive(true); //�A�N�e�B�u��Ԃɂ���
        buttonMode = n; //���݂̃{�^���̏�Ԃ��X�V
    }

    /// <summary>
    /// �E�C���X���폜����
    /// </summary>
    private void DestroyBeforeVirus()
    {
        //vChildren[buttonMode, vNum[buttonMode]].isActivity = false; //������Ԃ�false
        Destroy(vObject[buttonMode, /*vNum[buttonMode]*/vParents[buttonMode].setCount - 1]); //�Q�[���I�u�W�F�N�g���폜
    }

    /// <summary>
    /// �E�C���X��ݒu
    /// </summary>
    private void SetVirus()
    {
        GetVirus((VIRUS_NUM)buttonMode, vParents); //�E�C���X���A�N�e�B�u�ɂ���
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
            SaveVirusPosition(vParents, vChildren, (VIRUS_NUM)buttonMode, vObject, worldPos); //�ݒu�����E�C���X���W��ۑ�
            isOpenMenu = true;
        }
    }

    private void SearchVirusArray()
    {
        VirusChildren[] ary = vChildren.Cast<VirusChildren>().ToArray(); //�z����ꎟ����
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
