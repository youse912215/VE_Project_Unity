using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.TransformVirus;

public class ActVirus : MonoBehaviour
{
    private Virus[,] virus = new Virus[CATEGORY, OWNED];
    private GameObject[,] virusObj = new GameObject[CATEGORY, OWNED]; 
    private GameObject prefab;
    private bool isButtonActive;

    private Vector3 mousePos; //�}�E�X���W
    private Vector3 worldPos; //���[���h���W
    private Camera cam; //�J�����I�u�W�F�N�g

    private int buttonMode;
    private int oldMode;

    // Start is called before the first frame update
    void Start()
    {
        

        InitValue(virus, CODE_CLD);
        InitValue(virus, CODE_INF);
        InitValue(virus, CODE_19);

        isButtonActive = false;
        cam = Camera.main;

        buttonMode = 0;
        oldMode = buttonMode;
    }

    // Pushing any buttons
    public void ButtonPush(int n)
    {
        if (isButtonActive && buttonMode != n) return;

        if (ProcessOutOfRange(n)) return;
        isButtonActive = ReverseFlag(isButtonActive);

        if (isButtonActive)
        {         
            /* �E�C���X����������� */
            GenerationVirus(virus, (VIRUS_NUM)n); //�E�C���X����
            prefab = GameObject.Find(VirusName + n.ToString()); //�E�C���X�ԍ��ɍ��v����Prefab���擾
            virusObj[n, vNum[n]] = Instantiate(prefab); //�Q�[���I�u�W�F�N�g�𐶐�
            virusObj[n, vNum[n]].SetActive(true); //�A�N�e�B�u��Ԃɂ���
            buttonMode = n;
        }
        else 
        {
            /* �E�C���X���폜���� */
            virus[n, vNum[n]].isActivity = false; //������Ԃ�false
            Destroy(virusObj[n, vNum[n]]); //�Q�[���I�u�W�F�N�g���폜
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveVirus(); //�E�C���X���ړ�

        if (virus[buttonMode, vNum[buttonMode]].isActivity)
            virusObj[buttonMode, vNum[buttonMode]].transform.position = worldPos;

        if (!isButtonActive) return;
        SetVirus(); //�E�C���X��ݒu
    }

    /// <summary>
    /// flag�𔽓]����
    /// </summary>
    private bool ReverseFlag(bool flag)
    {
        return !flag ? true : false;
    }

    /// <summary>
    /// �E�C���X��ݒu
    /// </summary>
    private void SetVirus()
    {
        //�N���b�N
        if (Input.GetMouseButtonDown(1))
        {
            SaveVirusPosition(virus, (VIRUS_NUM)buttonMode, virusObj, worldPos);
            GetVirus((VIRUS_NUM)buttonMode);
            isButtonActive = false;
        }
    }

    /// <summary>
    /// �E�C���X���ړ�
    /// </summary>
    private void MoveVirus()
    {
        mousePos = Input.mousePosition;
        mousePos.z = CAM_DISTANCE;  
        worldPos = cam.ScreenToWorldPoint(mousePos);
    }
}
