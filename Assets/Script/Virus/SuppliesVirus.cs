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

public class SuppliesVirus : MonoBehaviour
{
    public static bool isGetItem;
    public static bool isSupplies;

    private List<int> suppliesItemList = new List<int> { 0, 0, 0, 0 };
    private List<int> getItemNumList = new List<int> { 99, 99, 99, 99 };

    private const int MAX_GET_NUM = 5;
    private int dayCount;

    // Start is called before the first frame update
    void Start()
    {
        dayCount = 0;
        isGetItem = false;
        isSupplies = false;
    }

    // Update is called once per frame
    void Update()
    {
        sup[0].text = VIRUS_NAME[suppliesItemList[0]];
        sup[1].text = VIRUS_NAME[suppliesItemList[1]];
        sup[2].text = VIRUS_NAME[suppliesItemList[2]];
        sup[3].text = VIRUS_NAME[suppliesItemList[3]];
        sup[4].text = "x" + getItemNumList[0].ToString();
        sup[5].text = "x" + getItemNumList[1].ToString();
        sup[6].text = "x" + getItemNumList[2].ToString();
        sup[7].text = "x" + getItemNumList[3].ToString();

        if (dayCount != DAY) return;
        if (!isSupplies) return;
        StartCoroutine(GetRandomInformation());
        suppliesItemList.RemoveRange(0, MATERIAL_LIST_NUM);
        getItemNumList.RemoveRange(0, MATERIAL_LIST_NUM);
        isSupplies = false;
        dayCount++;
        StopCoroutine(GetRandomInformation());
    }

    public void PushGetSuppliesButton()
    {
        if (isGetItem) return;

        StartCoroutine(GetRandomItem()); //ŠJŽn
        StopCoroutine(GetRandomItem()); //’âŽ~
        isGetItem = true;

        supText[0].transform.parent.gameObject.transform.localPosition = new Vector3(1000, 1000, 1000);
    }

    private void GetItem(List<int> list, int n)
    {
        list.Add((int)Integerization(rand % n));

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
            GetItem(suppliesItemList, vMatNam);
            GetItem(getItemNumList, MAX_GET_NUM);
        }
    }
}
