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
    public GameObject prefab;
    private bool isButtonActive;

    private Vector3 mousePos; //�}�E�X���W
    private Vector3 worldPos; //���[���h���W
    private Camera cam; //�J�����I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        InitValue(virus, CODE_CLD);
        InitValue(virus, CODE_INF);
        InitValue(virus, CODE_19);

        isButtonActive = false;
        cam = Camera.main;
    }

    // Pushing any buttons
    private void ButtonPush()
    {
        if (ProcessOutOfRange(CODE_CLD)) return;
        isButtonActive = ReverseFlag(isButtonActive);

        if (isButtonActive)
        {         
            /* �E�C���X����������� */
            GenerationVirus(virus, CODE_CLD, virusObj); //�E�C���X����
            virusObj[(int)CODE_CLD, vNum[(int)CODE_CLD]] = Instantiate(prefab); //�Q�[���I�u�W�F�N�g�𐶐�
            virusObj[(int)CODE_CLD, vNum[(int)CODE_CLD]].SetActive(true); //�A�N�e�B�u��Ԃɂ��� 
        }
        else 
        {
            /* �E�C���X���폜���� */
            virus[(int)CODE_CLD, vNum[(int)CODE_CLD]].isActivity = false; //������Ԃ�false
            Destroy(virusObj[(int)CODE_CLD, vNum[(int)CODE_CLD]]); //�Q�[���I�u�W�F�N�g���폜
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveVirus(); //�E�C���X���ړ�

        if (virus[(int)CODE_CLD, vNum[(int)CODE_CLD]].isActivity)
            virusObj[(int)CODE_CLD, vNum[(int)CODE_CLD]].transform.position = worldPos;

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
            SaveVirusPosition(virus, CODE_CLD, virusObj, worldPos);
            GetVirus(CODE_CLD);
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
