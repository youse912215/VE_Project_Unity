using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionProcess : MonoBehaviour
{
    private const int IMAGE_NUM = 3;
    [SerializeField] private GameObject[] img = new GameObject[IMAGE_NUM];

    private int selectImageNum;

    // Start is called before the first frame update
    void Start()
    {
        selectImageNum = 0;
        PushNextButton();
    }

    public void PushNextButton()
    {
        if (selectImageNum <= 2)
        {
            TurnThePage(); //ページをめくる
            selectImageNum++; //加算
        }
        else
        {
            selectImageNum = 0;
            TurnThePage(); //ページをめくる
        }
    }

    private void TurnThePage()
    {
        for (int i = 0; i < IMAGE_NUM; ++i) img[i].SetActive(false);
            img[selectImageNum].SetActive(true);
    }
}
