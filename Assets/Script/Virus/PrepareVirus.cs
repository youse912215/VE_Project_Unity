using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepareVirus : MonoBehaviour
{
    public static List<int> virusSetList = new List<int>{0, 1, 2};
    public static List<int> oldVirusSetList = new List<int>{0, 0, 0};
    public static List<int> typeSetList = new List<int>{1, 1, 1};

    private List<int> selectList;
    private int num_;

    private int selectSetNumber;

    private bool isSetList;
    private bool isPreVirus;

    [SerializeField] private Sprite[] sprite = new Sprite[8];

    [SerializeField] private AudioSource[] seVirus = new AudioSource[8]; 

    private GameObject prepareButton;

    // Start is called before the first frame update
    void Start()
    {
        isSetList = false;
        isPreVirus = false;
        selectSetNumber = 99;

        selectList = new List<int>{0, 1, 2, 3, 4, 7};
        num_ = 0;

        prepareButton = GameObject.Find("SetPrepareButtonParent");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPreVirus) return;
        prepareButton.transform.GetChild(selectSetNumber).GetComponent<Image>().sprite =
            sprite[virusSetList[selectSetNumber]];

        //if (typeSetList[selectSetNumber] == 1)
        //prepareButton.transform.GetChild(selectSetNumber).GetComponent<Image>().color =
        //    new Color(0.7f, 0.5f, 0.5f, 0.95f);
        //else prepareButton.transform.GetChild(selectSetNumber).GetComponent<Image>().color =
        //    new Color(0.5f, 0.5f, 0.7f, 0.95f);
    }

    public void PushSetListButton(int n)
    {
        

        isSetList = true;
        selectSetNumber = n; //選択したセット番号を取得

        if (!isSetList) return;

        isPreVirus = true;
        virusSetList[n] = selectList[num_];
        prepareButton.transform.GetChild(n).GetComponent<Image>().sprite =
            sprite[virusSetList[n]];

        seVirus[virusSetList[selectSetNumber]].PlayOneShot(seVirus[virusSetList[n]].clip);
        num_ = num_ != 5 ? ++num_ : 0;
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

        //if (typeSetList[n % 10] == 1)
        //prepareButton.transform.GetChild(n % 10).GetComponent<Image>().color =
        //    new Color(0.7f, 0.5f, 0.5f, 0.95f);
        //else prepareButton.transform.GetChild(n % 10).GetComponent<Image>().color =
        //    new Color(0.5f, 0.5f, 0.7f, 0.95f);
    }
}
