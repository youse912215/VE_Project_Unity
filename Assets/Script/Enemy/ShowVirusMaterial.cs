using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowVirusMaterial : MonoBehaviour
{
    [SerializeField] private Text[] dropTexts = new Text[4];
    public List<string> dropTextList = new List<string>{"", "", "", ""};

    // Update is called once per frame
    void Update()
    {
        dropTexts[0].text = dropTextList[0];
        dropTexts[1].text = dropTextList[1];
        dropTexts[2].text = dropTextList[2];
        dropTexts[3].text = dropTextList[3];
    }
}
