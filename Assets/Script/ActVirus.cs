using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.TransformVirus;

public class ActVirus : MonoBehaviour
{
    Virus[,] virus = new Virus[CATEGORY, OWNED];

    public static bool isButtonActive;

    public GameObject obj;

    GameObject[,] virusObj2D = new GameObject[CATEGORY, OWNED];

    public static bool[] act = new bool[10000];

    Vector3 pos;

    Vector3 mousePos;
    Vector3 worldPos;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        InitValue(virus, CODE_CLD);
        InitValue(virus, CODE_INF);
        InitValue(virus, CODE_19);

        isButtonActive = false;
    }

    // Pushing any buttons
    public void ButtonPush()
    {
        if (ProcessOutOfRange(CODE_CLD)) return;
        isButtonActive = ReverseFlag(isButtonActive);

        if (isButtonActive)
        {         
            /* �E�C���X����������� */
            GenerationVirus(virus, CODE_CLD, virusObj2D);
            virusObj2D[(int)CODE_CLD, vNum[(int)CODE_CLD]] = Instantiate(obj); //�Q�[���I�u�W�F�N�g�𐶐�
            virusObj2D[(int)CODE_CLD, vNum[(int)CODE_CLD]].SetActive(true); //�A�N�e�B�u��Ԃɂ��� 
        }
        else 
        {
            virus[(int)CODE_CLD, vNum[(int)CODE_CLD]].isActivity = false;
            Destroy(virusObj2D[(int)CODE_CLD, vNum[(int)CODE_CLD]]); //�Q�[���I�u�W�F�N�g���폜
        }
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = CAM_DISTANCE;

        cam = Camera.main;
        worldPos = cam.ScreenToWorldPoint(mousePos);

        if (virus[(int)CODE_CLD, vNum[(int)CODE_CLD]].isActivity)
            virusObj2D[(int)CODE_CLD, vNum[(int)CODE_CLD]].transform.position = worldPos;

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
        if (Input.GetMouseButtonDown(1))
        {
            SaveVirusPosition(virus, CODE_CLD, virusObj2D, worldPos);
            GetVirus(CODE_CLD);
            isButtonActive = false;
        }
    }
}
