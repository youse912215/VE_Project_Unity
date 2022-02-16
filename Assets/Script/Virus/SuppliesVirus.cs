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
    public static bool isGetItem;
    public static bool isSupplies;
    public bool endCoroutine;

    public static List<int> suppliesItemList = new List<int> { 0, 0, 0, 0 };
    public static List<int> getItemNumList = new List<int> { 99, 99, 99, 99 };

    private const int MAX_GET_NUM = 4;
    private int dayCount;

    public static Text[] sup = new Text[V_CATEGORY];
    public static GameObject[] supText = new GameObject[V_CATEGORY];

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SynthesizeVirus>().InitText(V_CATEGORY, "supText", supText, sup);
        dayCount = 0;
        isGetItem = false;
        isSupplies = true;
        endCoroutine = false;
    }

    // Update is called once per frame
    void Update()
    {
        //キャンバスモードがTowerDefense以外のとき、処理をスキップ
        if (CanvasManager.canvasMode != CanvasManager.CANVAS_MODE.SUPPLIES_MODE) return; 
        UpdateText();

        if (dayCount != DAY) return;
        if (!isSupplies) return;
        SetSuppliesMaterial();
    }
    private void UpdateText()
    {
        if (!endCoroutine) return;
        sup[0].text = VIRUS_NAME[suppliesItemList[0]];
        sup[1].text = VIRUS_NAME[suppliesItemList[1]];
        sup[2].text = VIRUS_NAME[suppliesItemList[2]];
        sup[3].text = VIRUS_NAME[suppliesItemList[3]];
        sup[4].text = "x" + getItemNumList[0].ToString();
        sup[5].text = "x" + getItemNumList[1].ToString();
        sup[6].text = "x" + getItemNumList[2].ToString();
        sup[7].text = "x" + getItemNumList[3].ToString();
    }

    private void SetSuppliesMaterial()
    {
        StartCoroutine(GetRandomInformation());

        supText[0].transform.parent.gameObject.transform.localPosition = Vector3.zero;


        StopCoroutine(GetRandomInformation());
        //suppliesItemList.RemoveRange(0, MATERIAL_LIST_NUM);
        //getItemNumList.RemoveRange(0, MATERIAL_LIST_NUM);

    }

    public void PushGetSuppliesButton()
    {
        if (isGetItem) return;

        StartCoroutine(GetRandomItem()); //開始
        StopCoroutine(GetRandomItem()); //停止
        isGetItem = true;

        supText[0].transform.parent.gameObject.transform.localPosition =
            new Vector3(1000, 1000, 1000);
    }

    private void GetItem(List<int> list, int n, int i)
    {
        //list.Add((int)Integerization(rand % n));
        list[i] = ((int)Integerization(rand % n)) + 1;

        if (list != getItemNumList) return;
        if (list[list.Count() - 1] == 0) list[list.Count() - 1]++;
    }

    private IEnumerator GetRandomItem()
    {
        for (int i = 0; i < MATERIAL_LIST_NUM; ++i)
        {
            yield return new WaitForSeconds(0.1f);
            vMatOwned[suppliesItemList[i]] += getItemNumList[i];
        }
    }

    private IEnumerator GetRandomInformation()
    {
        for (int i = 0; i < MATERIAL_LIST_NUM; ++i)
        {
            yield return new WaitForSeconds(0.1f);
            GetItem(suppliesItemList, vMatNam, i);
            GetItem(getItemNumList, MAX_GET_NUM, i);

            if (i == MATERIAL_LIST_NUM - 1)
            {
                isSupplies = false;
                isGetItem = false;
                endCoroutine = true;
                dayCount++;
            }
        }
    }
}
