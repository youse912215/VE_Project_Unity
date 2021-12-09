using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    //各キャンバス
    [SerializeField] private GameObject createCanvas;
    [SerializeField] private GameObject prepareCanvas;
    [SerializeField] private GameObject suppliesCanvas;

    private readonly Vector3 BACK_POS = new Vector3(3000.0f, 0.0f, 0.0f);

    private void Start()
    {
        PushCreateScreenButton();
    }

    /// <summary>
    /// Pushing create screen button
    /// </summary>
    public void PushCreateScreenButton()
    {
        Debug.Log("create");
        createCanvas.transform.localPosition = Vector3.zero;
        prepareCanvas.transform.localPosition = BACK_POS;
        suppliesCanvas.transform.localPosition = BACK_POS;
    }

    /// <summary>
    /// Pushing prepare screen button
    /// </summary>
    public void PushPrepareScreenButton()
    {
        Debug.Log("prepare");
        createCanvas.transform.localPosition = BACK_POS;
        prepareCanvas.transform.localPosition = Vector3.zero;
        suppliesCanvas.transform.localPosition = BACK_POS;
    }

    /// <summary>
    /// Pushing supplies screen button
    /// </summary>
    public void PushSuppliesScreenButton()
    {
        Debug.Log("supplies");
        createCanvas.transform.localPosition = BACK_POS;
        prepareCanvas.transform.localPosition = BACK_POS;
        suppliesCanvas.transform.localPosition = Vector3.zero;
    }

}
