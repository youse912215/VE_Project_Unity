using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationButtonManager : MonoBehaviour
{
    private const int SIMULATION_BUTTON_NUM = 8;
    public static Button[] buttons = new Button[SIMULATION_BUTTON_NUM];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; ++i)
            buttons[i] = GameObject.Find("Button (" + i.ToString() + ")").GetComponent<Button>();
    }
}
