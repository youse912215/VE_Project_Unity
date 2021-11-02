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
    private VirusParents[] vParents = new VirusParents[CATEGORY]; //�e�E�C���X�\���̔z��
    private VirusChildren[,] vChildren = new VirusChildren[CATEGORY, OWNED]; //�q�E�C���X�\���̔z��
    private GameObject[,] vObject = new GameObject[CATEGORY, OWNED]; //�E�C���X�I�u�W�F�N�g�z��
    private GameObject vPrefab; //�E�C���X�v���t�@�u

    private bool isGrabbedVirus; //�E�C���X��͂�ł��邩
    private Vector3 worldPos; //���[���h���W
    private int buttonMode; //�{�^���̏�ԁi�E�C���X�̎�ށj
    private bool isOpenMenu; //���j���[�t���O
    private int element; //�v�f�i�[�ϐ�
   
    // Start is called before the first frame update
    void Start()
    {
        for (VIRUS_NUM i = CODE_CLD; i < (VIRUS_NUM)CATEGORY; ++i)
            InitValue(vParents, vChildren, i); //�E�C���X���̏�����
        
        isGrabbedVirus = false; //�����͂�ł��Ȃ����
        buttonMode = 0; //�{�^���̏�Ԃ�off

        for (int i = 0; i < CATEGORY; ++i)
            vParents[i].tag = VirusTagName[i]; //�^�O��ۑ�
        vParents[0].creationCount = 5; //�쐬�����E�C���X����
        vParents[1].creationCount = 5;
        vParents[2].creationCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        worldPos = ReturnOnScreenMousePos(); //�X�N���[�������[���h�ϊ� //�E�C���X���ړ�

        vParents[buttonMode].setCount = GameObject.FindGameObjectsWithTag(vParents[buttonMode].tag).Length - 1; //�E�C���X�̐ݒu�����v�Z

        if (!isGrabbedVirus && isMouseCollider && Input.GetMouseButtonDown(1)){
            OpenAfterMenu(); //�ݒu�チ�j���[���J��
            isOpenMenu = true; //���j���[�t���O��true
            SearchVirusArray(); //�E�C���X�z�����������
        }

        if (vParents[buttonMode].setCount == 0) return; //�w��̃E�C���X�����݂��ĂȂ��Ƃ��A�������X�L�b�v
        //�E�C���X�I�u�W�F�N�g�̈ʒu�����[���h���W�ōX�V����
        if (vChildren[buttonMode, vParents[buttonMode].setCount - 1].isActivity)
            vObject[buttonMode, vParents[buttonMode].setCount - 1].transform.position = worldPos;
        if (!isGrabbedVirus) return; //�����͂�ł��Ȃ��Ƃ��A�������X�L�b�v
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
        if (!menuMode)
        {
            vChildren[buttonMode, vParents[buttonMode].setCount - 1].isActivity = true; //�ĂуA�N�e�B�u��Ԃ�
            ReverseMenuFlag(BACK); //���j���[�����
            isOpenMenu = false;
        }
        else
        {

        }
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
            int column = element / OWNED; //��i�E�C���X��ށj
            int row = element % OWNED; //�s�i�E�C���X�ۗL�ԍ��j
            if (vParents[column].setCount == vParents[column].creationCount)
                isLimitCapacity[column] = false; //�e�ʂ̌��E��Ԃ�����   
            Destroy(vObject[column, row]); //�E�C���X���폜����
            SortVirusArray(column, row); //�E�C���X�z����\�[�g
        }

        ReverseMenuFlag(BACK); //���j���[�����
        isOpenMenu = false; //���j���[�t���O��false
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
        Destroy(vObject[buttonMode, vParents[buttonMode].setCount - 1]); //�Q�[���I�u�W�F�N�g���폜
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
        if (isRangeCollision) return; //�E�C���X�͈͓̔��m���d�Ȃ��Ă���Ƃ��A�������X�L�b�v
        if (isOpenMenu) return; //���j���[�\�����A�������X�L�b�v

        //�E�N���b�N��
        if (Input.GetMouseButtonDown(1))
        {
            OpenBeforeMenu(); //���j���[���J��
            SaveVirusPosition(vParents, vChildren, (VIRUS_NUM)buttonMode, vObject, worldPos); //�ݒu�����E�C���X���W��ۑ�
            isOpenMenu = true; //���j���[�t���O��true
        }
    }

    /// <summary>
    /// �E�C���X�z��̓���̗v�f������
    /// </summary>
    private void SearchVirusArray()
    {
        var list = new List<VirusChildren>(); //���X�g��`
        VirusChildren[] array1 = vChildren.Cast<VirusChildren>().ToArray(); //�z����ꎟ����
        //�S�v�f���J��Ԃ�
        for (int i = 0; i < CATEGORY * OWNED; ++i)
        {
            //�e�E�C���X�̍��W�Ɣ͈̓I�u�W�F�N�g�̍��W����v����܂ŌJ��Ԃ�
            if (array1[i].pos == rangeObj.transform.position)
            {
                element = i; //����̗v�f���i�[
                break; //�J��Ԃ����甲���o��
            }
        }
    }

    /// <summary>
    /// �E�C���X�z����\�[�g����
    /// </summary>
    private void SortVirusArray(int column, int row)
    {
        var list1 = new List<VirusChildren>(); //���X�g��`
        var list2 = new List<GameObject>(); //���X�g��`
        VirusChildren[] structArray = vChildren.Cast<VirusChildren>().ToArray(); //�E�C���X�\���̔z����ꎟ����
        GameObject[] objectArray = vObject.Cast<GameObject>().ToArray(); //�E�C���X�Q�[���I�u�W�F�N�g�z����ꎟ����

        //�w��̃E�C���X�̃��X�g���쐬
        for (int i = 0; i < OWNED; ++i)
        {
            list1.Add(structArray[column * OWNED + i]);
            list2.Add(objectArray[column * OWNED + i]);
        }

        //�v�f�𖖔��ɒǉ�
        list1.Add(list1[row]); 
        list2.Add(list2[row]);
        //�w��̗v�f���폜
        list1.RemoveAt(row);
        list2.RemoveAt(row); 

        //���񂳂����Q�[���I�u�W�F�N�g���X�g�ɍX�V
        for (int i = 0; i < OWNED; ++i)
        {
            vChildren[column, i] = list1[i];
            vObject[column, i] = list2[i];
        }
    }
}
