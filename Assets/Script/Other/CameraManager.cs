using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Call.ConstantValue;
using static Call.CommonFunction;
using static ShowMenu;
using static TowerDefenceButtonManager;
using static PrepareVirus;
using static VirusMaterialData;

public class CameraManager : MonoBehaviour
{
    private bool isSetButton;
    private bool isPerChange; //視点変更したか
    private GameObject pMenuButton;
    public static GameObject pVirusButton;
    public static bool isActiveButton;

    private Vector3 pos;
    private Vector3 rot;

    public Canvas canvas0;
    public Canvas canvas1;

    [SerializeField] private GameObject subCam1;
    [SerializeField] private GameObject subCam2;
    GameObject obj;
    ActVirus actV;

    float wheel;
    bool isWheel;
    public static int currentSetNum;
    private GameObject wheelUI;

    [SerializeField] private GameObject water;
    [SerializeField] private Material nonActiveMat;
    [SerializeField] private Material activeMat;
    Renderer waterRenderer;

    [SerializeField] private Text currentOwnedText;

    private const float WHEEL_INTERVAL = 0.3f; //ホイールの間隔時間
    private readonly Vector2 CLICK_POS = new Vector2(1600.0f, 350.0f); //クリックできる位置

    // Start is called before the first frame update
    void Start()
    {
        pMenuButton = GetHierarchyObject("ParentMenuButton");
        pVirusButton = GetHierarchyObject("ParentVirusButton");

        transform.position = CAM_POS;
        pos = CAM_POS;
        rot = CAM_ROT;
        transform.position = pos;
        transform.rotation = Quaternion.Euler(rot);
        isSetButton = false;
        isPerChange = false;
        isActiveButton = true;

        //subCam = GameObject.Find("SubCamera");
        actV = GetOtherScriptObject<ActVirus>(obj);

        wheel = 0.0f;
        isWheel = false;
        //currentSetNum = 0;
        //SetVirusButtonPosition(ACTIVE_POS);
        wheelUI = GameObject.Find("WheelUI");
        wheelUI.SetActive(false);

        waterRenderer = water.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentOwnedText.text = (
            actV.vParents[virusSetList[currentSetNum]].creationCount -
            actV.vParents[virusSetList[currentSetNum]].setCount).ToString();

        wheel = Input.GetAxis("Mouse ScrollWheel");
        if (WheelPos())
        {
            wheelUI.SetActive(true);
            wheelUI.transform.position = new Vector3(mousePos.x + 55.0f, mousePos.y + 55.0f, 0.0f);
            waterRenderer.material = activeMat;
            if (wheel != 0.0f) StartCoroutine("ChangeVirusSets");
        }
        else
        {
            wheelUI.SetActive(false);
            waterRenderer.material = nonActiveMat;
        }

        ChangePerspective();

        if (isSetButton) return;
        StartCoroutine("InitSetButton"); //ボタンの初期化割り当て
    }

    /// <summary>
    /// 視点を変更する
    /// </summary>
    private void ChangePerspective()
    {
        if (!isPerChange) return; //視点変更されていないとき、処理をスキップ

        transform.position = pos;
        transform.rotation = Quaternion.Euler(rot); //視点を変更
        isPerChange = false; //フラグをfalse
    }

    /// <summary>
    /// Pushing change button
    /// </summary>
    public void PushChangeButton()
    {
        if (actV.isGrabbedVirus) return;

        pos = ChangeTransformCamera(pos, CAM_POS, CAM_P_POS); //位置を変更
        rot = ChangeTransformCamera(rot, CAM_ROT, CAM_P_ROT); //角度を変更
        VirusMenuSetActive(); //メニューの状態を変更
        isPerChange = true; //フラグをtrue
    }

    /// <summary>
    /// ウイルスメニューのアクティブ状態を管理
    /// </summary>
    private void VirusMenuSetActive()
    {
        if(isActiveButton){
            //非アクティブ状態に
            SetVirusButtonPosition(NON_ACTIVE_POS);
            isActiveButton = false;
            subCam1.SetActive(false);
            subCam2.SetActive(false);
        }
        else
        {
            //アクティブ状態に
            SetVirusButtonPosition(ACTIVE_POS);
            isActiveButton = true;
            subCam1.SetActive(true);
            subCam2.SetActive(true);
        }
    }

    /// <summary>
    /// 角度を変更する
    /// </summary>
    /// <param name="y">値</param>
    /// <returns>角度</returns>
    private Vector3 ChangeTransformCamera(Vector3 y, Vector3 x1, Vector3 x2)
    {
        y = y == x2 ? x1 : x2;
        return y;
    }

    /// <summary>
    /// ボタンの初期化割り当て
    /// </summary>
    /// <returns></returns>
    IEnumerator InitSetButton()
    {
        changeButton.onClick.AddListener(PushChangeButton);
        isSetButton = true;
        yield return null; //関数から抜ける
    }

    IEnumerator ChangeVirusSets()
    {
        isWheel = true;
        yield return new WaitForSeconds(WHEEL_INTERVAL);

        if (isWheel)
        {
            SetVirusButtonPosition(NON_ACTIVE_POS);
            currentSetNum = currentSetNum == 2 ? 0 : ++currentSetNum;
            SetVirusButtonPosition(ACTIVE_POS);
        }
        isWheel = false;
    }

    public static void SetVirusButtonPosition(Vector3 pos)
    {
        pVirusButton.transform.GetChild(virusSetList[currentSetNum]).transform.localPosition = pos;
    }

    private bool WheelPos()
    {
        return mousePos.x >= CLICK_POS.x && mousePos.y <= CLICK_POS.y;
    }
}
