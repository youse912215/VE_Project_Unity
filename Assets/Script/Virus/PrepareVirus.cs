using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareVirus : MonoBehaviour
{
    private const int SET_LIST_COUNT = 3;
    public static List<int> virusSetList = new List<int>{99, 99, 99};
    public static List<int> typeSetList = new List<int>{99, 99, 99};
    private int selectSetNumber;

    private bool isSetList;

    // Start is called before the first frame update
    void Start()
    {
        isSetList = false;
        selectSetNumber = 99;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushSetListButton(int n)
    {
        isSetList = true;
        selectSetNumber = n; //選択したセット番号を取得
    }

    public void PushPrepareVirusButton(int n)
    {
        if (!isSetList) return;

        virusSetList[selectSetNumber] = n;
    }

    public void PushTypeButton(int n)
    {
        typeSetList[n % 10] = n / 10;
    }
}
