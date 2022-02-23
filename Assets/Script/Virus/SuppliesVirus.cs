using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

using static VirusMaterialData;
using static RAND.CreateRandom;
using static Call.CommonFunction;
using static Scene;
using static SynthesizeVirus;
using static Call.VirusData;

public class SuppliesVirus : MonoBehaviour
{
    private const int MAX_GET_NUM = 4; //�ő�����
    private const float WAIT_TIME = 0.5f; //�҂�����
    private readonly Vector3 ENABLED_POS = new Vector3(1000, 1000, 1000); //��\���̍��W
    private int dayCount; //day�J�E���g

    public static bool isGetItem; //����t���O
    public static bool isSupplies; //�x���t���O
    public bool endCoroutine; //�R���[�`���t���O

    private List<int> suppliesItemList = new List<int> {}; //�x���A�C�e�������X�g
    private List<int> getItemNumList = new List<int> {}; //����A�C�e���ʃ��X�g
    public static Text[] itemListText = new Text[V_CATEGORY]; //�A�C�e���e�L�X�g
    public static GameObject[] textObject = new GameObject[V_CATEGORY]; //�e�L�X�g�I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SynthesizeVirus>().InitText(V_CATEGORY, "supText", textObject, itemListText); //�e�L�X�g�̏�����
        dayCount = 0;
        isGetItem = false;
        isSupplies = true;
        endCoroutine = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�L�����o�X���[�h��TowerDefense�ȊO�̂Ƃ��A�������X�L�b�v
        if (CanvasManager.canvasMode != CanvasManager.CANVAS_MODE.SUPPLIES_MODE) return;
        //�X�V����
        UpdateList();
        UpdateText();
    }

    /// <summary>
    /// �x���i���X�g���X�V
    /// </summary>
    private void UpdateList()
    {
        if (dayCount != DAY) return;
        if (!isSupplies) return;
        suppliesItemList.Clear(); //���X�g���N���A
        getItemNumList.Clear(); //���X�g���N���A
        StartCoroutine(GetRandomInformation()); //�����_���ŃA�C�e��������肷��R���[�`�����܂킷
        textObject[0].transform.parent.gameObject.transform.localPosition = Vector3.zero; //�\��
        dayCount++; //day���J�E���g
    }

    /// <summary>
    /// �x���i�e�L�X�g���X�V
    /// </summary>
    private void UpdateText()
    {
        if (!endCoroutine) return;
        if (isSupplies) return;
        if (isGetItem) return;
        /* �A�C�e���� */
        itemListText[0].text = VIRUS_NAME[suppliesItemList[0]];
        itemListText[1].text = VIRUS_NAME[suppliesItemList[1]];
        itemListText[2].text = VIRUS_NAME[suppliesItemList[2]];
        itemListText[3].text = VIRUS_NAME[suppliesItemList[3]];
        /* �A�C�e���� */
        itemListText[4].text = "x" + getItemNumList[0].ToString();
        itemListText[5].text = "x" + getItemNumList[1].ToString();
        itemListText[6].text = "x" + getItemNumList[2].ToString();
        itemListText[7].text = "x" + getItemNumList[3].ToString();
    }

    /// <summary>
    /// �x���i����肷��{�^�����������Ƃ�
    /// </summary>
    public void PushGetSuppliesButton()
    {
        if (isGetItem) return;
        StartCoroutine(GetRandomItem()); //�J�n
        StopCoroutine(GetRandomItem()); //��~
        isGetItem = true; //����ς�
        textObject[0].transform.parent.gameObject.transform.localPosition = ENABLED_POS; //��\��
    }

    /// <summary>
    /// �A�C�e������
    /// </summary>
    /// <param name="list"></param>
    /// <param name="n"></param>
    private void GetItem(List<int> list, int n)
    {
        list.Add((int)Integerization(rand % n)); //�����_���l���擾
        if (list != getItemNumList) return; //�A�C�e���ʃ��X�g�ȊO�́A�������X�L�b�v
        if (list[list.Count() - 1] == 0) list[list.Count() - 1]++; //����ʂ�0�̂Ƃ��A1���₷
    }

    /// <summary>
    /// ���X�g�ɂ��郉���_���ȃA�C�e������肷��
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetRandomItem()
    {
        for (int i = 0; i < MATERIAL_LIST_NUM; ++i)
        {
            yield return new WaitForSeconds(0.1f);
            vMatOwned[suppliesItemList[i]] += getItemNumList[i];
        }
    }

    /// <summary>
    /// �����_���ŃA�C�e���������
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetRandomInformation()
    {
        //�A�C�e���ʂ̃��X�g���Ō�܂œ��B���Ă��Ȃ��ԁA�J��Ԃ�
        while (getItemNumList.Count != MATERIAL_LIST_NUM)
        {
            //MATERIAL_LIST_NUM����
            for (int i = 0; i < MATERIAL_LIST_NUM; ++i)
            {
                yield return new WaitForSeconds(WAIT_TIME); //WAIT_TIME �҂�
                GetItem(suppliesItemList, vMatNam); //����A�C�e�������擾
                GetItem(getItemNumList, MAX_GET_NUM); //�A�C�e���ʂ��擾

                //�J��Ԃ��̍Ō�ɁA�e�t���O���Z�b�g
                if (i == MATERIAL_LIST_NUM - 1)
                {
                    isSupplies = false;
                    isGetItem = false;
                    endCoroutine = true;
                }
            }
        }

    }
}
