using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static DebugManager;
using static VirusMaterialData;
using static VirusMaterialData.MATERIAL_CODE;
using static SimulationButtonManager;

public class SynthesizeVirus : MonoBehaviour
{
    private VIRUS_NUM currentCode;

    private bool isB;

    // Start is called before the first frame update
    void Start()
    {
        InitSetButton();

        currentCode = CODE_NONE;
        vMatOwned[0] = 2;
        vMatOwned[1] = 3;
        vMatOwned[2] = 1;
        vMatOwned[15] = 0;
        isB = false;
    }

    // Update is called once per frame
    void Update()
    {
        DebugFunc(currentCode);
        
    }

    public void PushCldButton()
    {
       currentCode = CODE_CLD;
       isB = CheckMaterialCount(V1_PROTEIN, V2_PROTEIN, V3_PROTEIN, BLUE_POTION, CODE_CLD);
       DebugFunc(isB);
    }

    public void PushInfButton()
    {
        currentCode = CODE_INF;
    }

    public void Push19Button()
    {
        currentCode = CODE_19;
    }

    public void PushNovButton()
    {
        currentCode = CODE_NOV;
    }

    public void PushEhfButton()
    {
        currentCode = CODE_EHF;
    }

    public void PushEvButton()
    {
        currentCode = CODE_EV;
    }

    public void PushBdButton()
    {
        currentCode = CODE_BD;
    }

    public void PushUltButton()
    {
        currentCode = CODE_ULT;
    }

    private void InitSetButton()
    {
        buttons[0].onClick.AddListener(PushCldButton);
        buttons[1].onClick.AddListener(PushInfButton);
        buttons[2].onClick.AddListener(Push19Button);
        buttons[3].onClick.AddListener(PushNovButton);
        buttons[4].onClick.AddListener(PushEhfButton);
        buttons[5].onClick.AddListener(PushEvButton);
        buttons[6].onClick.AddListener(PushBdButton);
        buttons[7].onClick.AddListener(PushUltButton);
    }
}
