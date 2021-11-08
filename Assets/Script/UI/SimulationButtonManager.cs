using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Call.CommonFunction;
using static MaterialManager;

public class SimulationButtonManager : MonoBehaviour
{
    private const int SIMULATION_BUTTON_NUM = 11; //使用ボタン数
    public static Button[] buttons = new Button[SIMULATION_BUTTON_NUM]; //ボタンオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        //ボタンオブジェクトを取得
        for (int i = 0; i < buttons.Length; ++i)
            buttons[i] = GameObject.Find("Button (" + i.ToString() + ")").GetComponent<Button>();

        buttons[8].image.color = Color.grey;
        buttons[9].image.color = Color.gray;
        buttons[10].image.color = Color.grey;
    }
}
