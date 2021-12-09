using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static VirusMaterialData;
using static RAND.CreateRandom;
using static Call.CommonFunction;
using System.Linq;

public class SuppliesVirus : MonoBehaviour
{
    public static bool isSupplies;

    private List<int> suppliesItemList = new List<int>{99, 99, 99, 99};
    private List<int> getItemNumList = new List<int>{99, 99, 99, 99};

    private const int MAX_GET_NUM = 5;

    // Start is called before the first frame update
    void Start()
    {
        isSupplies = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(suppliesItemList[0] + "::" + suppliesItemList[1] + "::" + suppliesItemList[2] + "::" + suppliesItemList[3]);
        Debug.Log(getItemNumList[0] + "::" + getItemNumList[1] + "::" + getItemNumList[2] + "::" + getItemNumList[3]);
    }

    public void PushGetSuppliesButton()
    {
        if (isSupplies) return;

        StartCoroutine(GetRandomItem()); //ŠJŽn
        suppliesItemList.RemoveRange(0, MATERIAL_LIST_NUM);
        getItemNumList.RemoveRange(0, MATERIAL_LIST_NUM);
        StopCoroutine(GetRandomItem()); //’âŽ~
        isSupplies = true;
    }

    private void GetItem(List<int> list, int n)
    {
        list.Add((int)Integerization(rand % n));

        if (list != getItemNumList) return;
        if (list[list.Count() - 1] == 0) list[list.Count() - 1]++;
    }

    private IEnumerator GetRandomItem ()
    {
        for(int i = 0; i < MATERIAL_LIST_NUM; ++i)
        {
            yield return new WaitForSeconds(0.1f);
            GetItem(suppliesItemList, vMatNam);
            GetItem(getItemNumList, MAX_GET_NUM);
            vMatOwned[suppliesItemList[i]] += getItemNumList[i];
        }
    }
}
