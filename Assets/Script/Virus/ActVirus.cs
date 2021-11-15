using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.CommonFunction;
using static ShowMenu;
using static MouseCollision;
using static DebugManager;

public class ActVirus : MonoBehaviour
{
    /* private */
    private GameObject vPrefab; //�E�C���X�v���t�@�u
    private Vector3 worldPos; //���[���h���W
    private int element; //�v�f�i�[�ϐ�

    /* public */
    public VirusParents[] vParents = new VirusParents[CATEGORY]; //�e�E�C���X�\���̔z��
    public VirusChildren[,] vChildren = new VirusChildren[CATEGORY, OWNED]; //�q�E�C���X�\���̔z��
    public GameObject[,] vObject = new GameObject[CATEGORY, OWNED]; //�E�C���X�I�u�W�F�N�g�z��
    public bool isGrabbedVirus; //�E�C���X��͂�ł��邩
    public bool isOpenMenu; //���j���[�t���O
    public int buttonMode; //�{�^���̏�ԁi�E�C���X�̎�ށj
    public int column; //��i�E�C���X��ށj
    public int row; //�s�i�E�C���X�ۗL�ԍ��j
   
    // Start is called before the first frame update
    void Start()
    {
        for (VIRUS_NUM i = CODE_CLD; i < (VIRUS_NUM)CATEGORY; ++i)
            InitValue(vParents, vChildren, i); //�E�C���X���̏�����
        
        isGrabbedVirus = false; //�����͂�ł��Ȃ����
        buttonMode = 0; //�{�^���̏�Ԃ�off
        element = 0; //�v�f�i�[�ϐ������Z�b�g
        column = 0; //���0�ɏ�����
        row = 0; //�s��0�ɏ�����

        for (int i = 0; i < CATEGORY; ++i){
            vParents[i].tag = VirusTagName[i]; //�^�O��ۑ�
            vParents[i].creationCount = 5;
        }
        //vParents[0].creationCount = 5; //�쐬�����E�C���X����
        //vParents[1].creationCount = 5;
        //vParents[2].creationCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        worldPos = ReturnOnScreenMousePos(); //�X�N���[�������[���h�ϊ�

        vParents[buttonMode].setCount = GameObject.FindGameObjectsWithTag(vParents[buttonMode].tag).Length - 1; //�E�C���X�̐ݒu�����v�Z

        AfterMenuAction(); //�ݒu�チ�j���[���J��

        if (vParents[buttonMode].setCount == 0) return; //�w��̃E�C���X�����݂��ĂȂ��Ƃ��A�������X�L�b�v
        UpdateViursPosition(); //�E�C���X�̈ʒu���X�V

        if (!isGrabbedVirus) return; //�����͂�ł��Ȃ��Ƃ��A�������X�L�b�v
        BeforeMenuAction(); //�ݒu�O���j���[���J��
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
        else Destroy(vObject[buttonMode, vParents[buttonMode].setCount - 1]); //�Q�[���I�u�W�F�N�g���폜 // �E�C���X���폜����

        column = buttonMode; //��i�E�C���X��ށj
        row = vParents[buttonMode].setCount; //�s�i�E�C���X�ۗL�ԍ��j

    }

    /// <summary>
    /// �E�C���X���쐬����
    /// </summary>
    /// <param name="n">�E�C���X�̎�ށi�ԍ��j</param>
    public void CreateVirus(int n)
    {
        GenerationVirus(vParents, vChildren, n); //�E�C���X����
        vPrefab = GameObject.Find(VirusHeadName + n.ToString()); //�E�C���X�ԍ��ɍ��v����Prefab���擾
        vObject[n, vParents[n].setCount] = Instantiate(vPrefab); //�Q�[���I�u�W�F�N�g�𐶐�
        vObject[n, vParents[n].setCount].transform.localScale = V_SIZE; //�X�P�[���T�C�Y���擾
        vObject[n, vParents[n].setCount].transform.GetChild(0).gameObject.transform.localScale = vRange; //�͈͂��擾
        vObject[n, vParents[n].setCount].SetActive(true); //�A�N�e�B�u��Ԃɂ���
        buttonMode = n; //���݂̃{�^���̏�Ԃ��X�V
    }

    //�͂�ł���E�C���X�̈ʒu���X�V
    private void UpdateViursPosition()
    {
        if (vChildren[column, row].isActivity) vObject[column, row].transform.position = worldPos; //�}�E�X�|�C���^�Ɠ����ʒu
    }

    /// <summary>
    /// �ݒu�O���j���[���J��
    /// </summary>
    private void BeforeMenuAction()
    {
        if (isRangeCollision) return; //�E�C���X�͈͓̔��m���d�Ȃ��Ă���Ƃ��A�������X�L�b�v
        if (isOpenMenu) return; //���j���[�\�����A�������X�L�b�v

        //�E�N���b�N��
        if (Input.GetMouseButtonDown(1))
        {
            OpenBeforeMenu(); //���j���[���J��
            SaveVirusPosition(vChildren, column, row, vObject, worldPos); //�ݒu�����E�C���X���W��ۑ�
            isOpenMenu = true; //���j���[�t���O��true
        }
    }

    /// <summary>
    /// �ݒu�チ�j���[���J��
    /// </summary>
    private void AfterMenuAction()
    {
        if (!isMouseCollider) return; //�}�E�X�|�C���^���E�C���X�͈̔͂ɏd�Ȃ��Ă��Ȃ��Ƃ��A�������X�L�b�v
        if (isGrabbedVirus) return; //�E�C���X��͂�ł���Ƃ��A�������X�L�b�v
        
        if (Input.GetMouseButtonDown(1)){
            OpenAfterMenu(); //�ݒu�チ�j���[���J��
            isOpenMenu = true; //���j���[�t���O��true
            SearchVirusArray(); //�E�C���X�z�����������
            UpdateVirusArray(); //�E�C���X�z��̍X�V
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
    /// �E�C���X�z��̏����X�V����
    /// </summary>
    private void UpdateVirusArray()
    {
        column = element / OWNED; //��i�E�C���X��ށj
        row = element % OWNED; //�s�i�E�C���X�ۗL�ԍ��j
        buttonMode = column; //�{�^���̏�Ԃ��X�V
    }
}
