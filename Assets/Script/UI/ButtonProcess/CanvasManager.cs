using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static SynthesizeVirus;
using static Call.CommonFunction;

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

    GameObject obj;
    SynthesizeVirus syV;

    public enum CANVAS_MODE : int
    {
       CREATE_MODE,
       PREPARE_MODE,
       SUPPLIES_MODE,
       OPTION_MODE,
       MANUAL_MODE,
       TOWER_DEFENCE_MODE,
    }

    public static CANVAS_MODE canvasMode = CANVAS_MODE.CREATE_MODE;

    private readonly Vector3 BACK_POS = new Vector3(3000.0f, 0.0f, 0.0f);

    private void Start()
    {
        syV = GetOtherScriptObject<SynthesizeVirus>(obj);

        //初期位置
        createCanvas.transform.localPosition = Vector3.zero;
        prepareCanvas.transform.localPosition = BACK_POS;
        suppliesCanvas.transform.localPosition = BACK_POS;
        currentCountCanvas.transform.localPosition = Vector3.zero;
        optionCanvas.transform.localPosition = BACK_POS;
        manualImageCanvas.transform.localPosition = BACK_POS;
        manualButtonCanvas.transform.localPosition = BACK_POS;
        simulationCanvas.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Pushing create screen button
    /// </summary>
    public void PushCreateScreenButton()
    {
        syV.PushVirusButton((int)currentCode);
        canvasMode = CANVAS_MODE.CREATE_MODE;
        createCanvas.transform.localPosition = Vector3.zero;
        prepareCanvas.transform.localPosition = BACK_POS;
        suppliesCanvas.transform.localPosition = BACK_POS;
        currentCountCanvas.transform.localPosition = Vector3.zero;
        optionCanvas.transform.localPosition = BACK_POS;
        manualImageCanvas.transform.localPosition = BACK_POS;
        manualButtonCanvas.transform.localPosition = BACK_POS;
        simulationCanvas.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Pushing prepare screen button
    /// </summary>
    public void PushPrepareScreenButton()
    {
        syV.PushVirusButton((int)currentCode);
        canvasMode = CANVAS_MODE.PREPARE_MODE;
        createCanvas.transform.localPosition = BACK_POS;
        prepareCanvas.transform.localPosition = Vector3.zero;
        suppliesCanvas.transform.localPosition = BACK_POS;
        currentCountCanvas.transform.localPosition = Vector3.zero;
        optionCanvas.transform.localPosition = BACK_POS;
        manualImageCanvas.transform.localPosition = BACK_POS;
        manualButtonCanvas.transform.localPosition = BACK_POS;
        simulationCanvas.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Pushing supplies screen button
    /// </summary>
    public void PushSuppliesScreenButton()
    {
        syV.PushVirusButton((int)currentCode);
        canvasMode = CANVAS_MODE.SUPPLIES_MODE;
        createCanvas.transform.localPosition = BACK_POS;
        prepareCanvas.transform.localPosition = BACK_POS;
        suppliesCanvas.transform.localPosition = Vector3.zero;
        currentCountCanvas.transform.localPosition = BACK_POS;
        optionCanvas.transform.localPosition = BACK_POS;
        manualImageCanvas.transform.localPosition = BACK_POS;
        manualButtonCanvas.transform.localPosition = BACK_POS;
        simulationCanvas.transform.localPosition = Vector3.zero;
    }

    public void PushOptionButton()
    {
        syV.PushVirusButton((int)currentCode);
        canvasMode = CANVAS_MODE.OPTION_MODE;
        createCanvas.transform.localPosition = BACK_POS;
        prepareCanvas.transform.localPosition = BACK_POS;
        suppliesCanvas.transform.localPosition = BACK_POS;
        currentCountCanvas.transform.localPosition = BACK_POS;
        optionCanvas.transform.localPosition = Vector3.zero;
        manualImageCanvas.transform.localPosition = BACK_POS;
        manualButtonCanvas.transform.localPosition = BACK_POS;
        simulationCanvas.transform.localPosition = Vector3.zero;
    }

    public void PushManualButton()
    {
        canvasMode = CANVAS_MODE.MANUAL_MODE;
        createCanvas.transform.localPosition = BACK_POS;
        prepareCanvas.transform.localPosition = BACK_POS;
        suppliesCanvas.transform.localPosition = BACK_POS;
        currentCountCanvas.transform.localPosition = BACK_POS;
        optionCanvas.transform.localPosition = BACK_POS;
        manualImageCanvas.transform.localPosition = Vector3.zero;
        manualButtonCanvas.transform.localPosition = Vector3.zero;
        simulationCanvas.transform.localPosition = BACK_POS;
    }
}
