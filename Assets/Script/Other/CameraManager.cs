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
    [SerializeField] private GameObject wheelUI;

    [SerializeField] private GameObject water;
    [SerializeField] private Material nonActiveMat;
    [SerializeField] private Material activeMat;
    Renderer waterRenderer;

    [SerializeField] private Text currentOwnedText;

    private const float WHEEL_INTERVAL = 0.3f; //ホイールの間隔時間
    private readonly Vector2 CLICK_POS = new Vector2(1600.0f, 350.0f); //クリックできる位置
    private const float MOUSE_DIFF_POS = 55.0f; //マウスの差分座標

    public int owned = 0;

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
        //wheelUI = GameObject.Find("WheelUI");
        wheelUI.SetActive(false);

        waterRenderer = water.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /* 使用可能ウイルス数を更新 */
        owned = vCreationCount[virusSetList[currentSetNum]]
            - vSetCount[virusSetList[currentSetNum]]; //現在の保有ウイルス数UIを更新
        FixedOutOfRange(); //範囲外の数値を修正
        currentOwnedText.text = owned.ToString(); //現在の保有ウイルス数UIを更新

        /* ホイール更新処理 */
        wheel = Input.GetAxis("Mouse ScrollWheel"); //ホイールを取得
        ChangeWheelUIActivity(); //ホイールUIのアクティブ状態の変更
        ChangePerspective(); //視点変更

        /* ボタン初期化処理 */
        if (isSetButton) return;
        StartCoroutine("InitSetButton"); //ボタンの初期化割り当て
    }

    /// <summary>
    /// 範囲外の数値を修正
    /// </summary>
    void FixedOutOfRange()
    {
        if (owned > -1) return;
        owned = 0;
    }

    /// <summary>
    /// ホイールUIのアクティブ状態の変更
    /// </summary>
    private void ChangeWheelUIActivity()
    {
        if (currentOwnedText.enabled && WheelPos())
        {
            wheelUI.SetActive(true); //ホイールをアクティブに
            wheelUI.transform.position =
                new Vector3(mousePos.x + MOUSE_DIFF_POS, mousePos.y + MOUSE_DIFF_POS, 0.0f); //マウスの差分座標を加算
            waterRenderer.material = activeMat; //マテリアルをアクティブに
            if (wheel != 0.0f) StartCoroutine("ChangeVirusSets"); //ウイルスUIを切替
        }
        else
        {
            wheelUI.SetActive(false); //ホイールを非アクティブに
            waterRenderer.material = nonActiveMat; //マテリアルを非アクティブに
        }
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
            actV.comCanvas.SetActive(false);
            currentOwnedText.enabled = false;        
        }
        else
        {
            //アクティブ状態に
            SetVirusButtonPosition(ACTIVE_POS);
            isActiveButton = true;
            subCam1.SetActive(true);
            subCam2.SetActive(true);
            actV.comCanvas.SetActive(true);
            currentOwnedText.enabled = true;
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

    /// <summary>
    /// ウイルスUI切替
    /// 切替時のタイミングを調整するコルーチン
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// ウイルスボタンの位置をセット
    /// </summary>
    /// <param name="pos"></param>
    public static void SetVirusButtonPosition(Vector3 pos)
    {
        pVirusButton.transform.GetChild(virusSetList[currentSetNum]).transform.localPosition = pos;
    }

    /// <summary>
    /// ホイールの座標とクリック可能座標の位置関係を返す
    /// </summary>
    /// <returns></returns>
    private bool WheelPos()
    {
        return mousePos.x >= CLICK_POS.x && mousePos.y <= CLICK_POS.y;
    }
}
