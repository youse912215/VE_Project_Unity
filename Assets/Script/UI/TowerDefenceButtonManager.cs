using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Call.CommonFunction;
using static MaterialManager;


public class TowerDefenceButtonManager : MonoBehaviour
{
    public static Button changeButton;

    // Start is called before the first frame update
    void Start()
    {
        changeButton = GameObject.Find("ChangeButton").GetComponent<Button>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
