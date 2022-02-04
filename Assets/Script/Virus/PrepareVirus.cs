using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepareVirus : MonoBehaviour
{
    public static List<int> virusSetList = new List<int>{0, 1, 2};
    public static List<int> typeSetList = new List<int>{1, 1, 1};
    private int selectSetNumber;

    private bool isSetList;
    private bool isPreVirus;

    [SerializeField] private Sprite[] sprite = new Sprite[8];

    private GameObject prepareButton;

    // Start is called before the first frame update
    void Start()
    {
        isSetList = false;
        isPreVirus = false;
        selectSetNumber = 99;

        prepareButton = GameObject.Find("SetPrepareButtonParent");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPreVirus) return;
        prepareButton.transform.GetChild(selectSetNumber).GetComponent<Image>().sprite =
            sprite[virusSetList[selectSetNumber]];

        if (typeSetList[selectSetNumber] == 1)
        prepareButton.transform.GetChild(selectSetNumber).GetComponent<Image>().color =
            new Color(0.7f, 0.5f, 0.5f, 0.95f);
        else prepareButton.transform.GetChild(selectSetNumber).GetComponent<Image>().color =
            new Color(0.5f, 0.5f, 0.7f, 0.95f);
    }

    public void PushSetListButton(int n)
    {
        isSetList = true;
        selectSetNumber = n; //選択したセット番号を取得
    }

    public void PushPrepareVirusButton(int n)
    {
        if (!isSetList) return;

        isPreVirus = true;
        virusSetList[selectSetNumber] = n;
        prepareButton.transform.GetChild(selectSetNumber).GetComponent<Image>().sprite =
            sprite[virusSetList[selectSetNumber]];
    }

    public void PushTypeButton(int n)
    {
        typeSetList[n % 10] = n / 10;

        if (typeSetList[n % 10] == 1)
        prepareButton.transform.GetChild(n % 10).GetComponent<Image>().color =
            new Color(0.7f, 0.5f, 0.5f, 0.95f);
        else prepareButton.transform.GetChild(n % 10).GetComponent<Image>().color =
            new Color(0.5f, 0.5f, 0.7f, 0.95f);
    }
}
