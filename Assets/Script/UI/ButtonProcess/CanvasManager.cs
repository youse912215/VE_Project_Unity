using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static SynthesizeVirus;
using static Call.CommonFunction;

public class CanvasManager : MonoBehaviour
{
    //�e�L�����o�X
    [SerializeField] private GameObject createCanvas;
    [SerializeField] private GameObject prepareCanvas;
    [SerializeField] private GameObject suppliesCanvas;
    [SerializeField] private GameObject currentCountCanvas;

    GameObject obj;
    SynthesizeVirus syV;

    public enum CANVAS_MODE : int
    {
       CREATE_MODE,
       PREPARE_MODE,
       SUPPLIES_MODE,
       TOWER_DEFENCE_MODE,
    }

    public static CANVAS_MODE canvasMode = CANVAS_MODE.CREATE_MODE;

    private readonly Vector3 BACK_POS = new Vector3(3000.0f, 0.0f, 0.0f);

    private void Start()
    {
        syV = GetOtherScriptObject<SynthesizeVirus>(obj);

        //�����ʒu
        createCanvas.transform.localPosition = Vector3.zero;
        prepareCanvas.transform.localPosition = BACK_POS;
        suppliesCanvas.transform.localPosition = BACK_POS;
        currentCountCanvas.transform.localPosition = Vector3.zero;
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
    }

}
