using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static SynthesizeVirus;
using static Call.CommonFunction;
using static Call.ConstantValue;
using static SuppliesVirus;
using static VirusMaterialData;

using static PrepareVirus;
using static CameraManager;
using static PhotoManager;

public class CanvasManager : MonoBehaviour
{
    //各キャンバス
    [SerializeField] private GameObject createCanvas;
    [SerializeField] private GameObject prepareCanvas;
    [SerializeField] private GameObject suppliesCanvas;
    [SerializeField] private GameObject currentCountCanvas;
    [SerializeField] private GameObject optionCanvas;
    [SerializeField] private GameObject manualImageCanvas;
    [SerializeField] private GameObject manualButtonCanvas;
    [SerializeField] private GameObject simulationCanvas;
    [SerializeField] private GameObject mainCanvas;

    GameObject obj;
    SynthesizeVirus syV;

    GameObject act;
    ActVirus actV;

    public enum CANVAS_MODE : int
    {
       CREATE_MODE,
       PREPARE_MODE,
       SUPPLIES_MODE,
       OPTION_MODE,
       MANUAL_MODE,
       TOWER_DEFENCE_MODE,
    }

    public static CANVAS_MODE canvasMode = CANVAS_MODE.SUPPLIES_MODE;

    private readonly Vector3 BACK_POS = new Vector3(3000.0f, 0.0f, 0.0f);

    private void Start()
    {
        syV = GetOtherScriptObject<SynthesizeVirus>(obj);
        actV = GetOtherScriptObject<ActVirus>(act);

        //初期位置
        isSupplies = true;
        SetCanvas(false, false, true, false, false, false, false, true, false);
    }

    private Vector3 Vec3Pos(bool isActive)
    {
        return (isActive) ? Vector3.zero : BACK_POS;
    }

    private void SetCanvas(bool cre, bool pre, bool sup, bool cou, bool opt, bool img,
        bool but, bool sim, bool main)
    {
        createCanvas.transform.localPosition = Vec3Pos(cre);
        prepareCanvas.transform.localPosition = Vec3Pos(pre);
        suppliesCanvas.transform.localPosition = Vec3Pos(sup);
        currentCountCanvas.transform.localPosition = Vec3Pos(cou);
        optionCanvas.transform.localPosition = Vec3Pos(opt);
        manualImageCanvas.transform.localPosition = Vec3Pos(img);
        manualButtonCanvas.transform.localPosition = Vec3Pos(but);
        simulationCanvas.transform.localPosition = Vec3Pos(sim);
        mainCanvas.transform.localPosition = Vec3Pos(main);
    }

    /// <summary>
    /// Pushing create screen button
    /// </summary>
    public void PushCreateScreenButton()
    {
        syV.PushVirusButton((int)currentCode);
        canvasMode = CANVAS_MODE.CREATE_MODE;
        SetCanvas(true, false, false, true, false, false, false, true, false);
    }

    /// <summary>
    /// Pushing prepare screen button
    /// </summary>
    public void PushPrepareScreenButton()
    {
        syV.PushVirusButton((int)currentCode);
        canvasMode = CANVAS_MODE.PREPARE_MODE;
        SetCanvas(false, true, false, true, false, false, false, true, false);
    }

    /// <summary>
    /// Pushing supplies screen button
    /// </summary>
    public void PushSuppliesScreenButton()
    {
        syV.PushVirusButton((int)currentCode);
        canvasMode = CANVAS_MODE.SUPPLIES_MODE;
        isSupplies = true;
        SetCanvas(false, false, true, false, false, false, false, true, false);
    }

    public void PushOptionButton()
    {
        syV.PushVirusButton((int)currentCode);
        canvasMode = CANVAS_MODE.OPTION_MODE;
        SetCanvas(false, false, false, false, true, false, false, true, false);

        actV.SetUIActivity(false);
        actV.comCanvas.SetActive(true);

        SetVirusButtonPosition(NON_ACTIVE_POS);

        if (WaveGauge.currentDay == Scene.DAY) return;
        Scene.DAY++;
    }

    public void PushManualButton()
    {
        canvasMode = CANVAS_MODE.MANUAL_MODE;
        SetCanvas(false, false, false, false, false, true, true, false, false);
    }

    public void PushDefenceButton()
    {
        canvasMode = CANVAS_MODE.TOWER_DEFENCE_MODE;
        SetCanvas(false, false, false, false, false, false, false, false, true);

        actV.SetUIActivity(true);
        
        currentSetNum = 0;
        SetVirusButtonPosition(ACTIVE_POS);

        for (int i = 0; i < 8; ++i)
        {
            actV.vParents[i].creationCount = vCreationCount[i];
        }
        
        if (ImageObj == null) return;
        ImageObj.SetActive(true);
    }
}
