using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.CommonFunction;
using static ShowMenu;
using static MouseCollision;
using static EnemyCollision;
using static VirusMaterialData;
using static CameraManager;

public class ActVirus : MonoBehaviour
{
    /* private */
    private GameObject vPrefab; //ウイルスプレファブ
    private Vector3 worldPos; //ワールド座標
    private int element; //要素格納変数
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] public Material defaultPs;
    

    /* public */
    public VirusParents[] vParents = new VirusParents[V_CATEGORY]; //親ウイルス構造体配列
    public VirusChildren[,] vChildren = new VirusChildren[V_CATEGORY, OWNED]; //子ウイルス構造体配列
    public GameObject[,] vObject = new GameObject[V_CATEGORY, OWNED]; //ウイルスオブジェクト配列
    public bool isGrabbedVirus; //ウイルスを掴んでいるか
    public bool isOpenMenu; //メニューフラグ
    public int buttonMode; //ボタンの状態（ウイルスの種類）
    public int column; //列（ウイルス種類）
    public int row; //行（ウイルス保有番号）
    [SerializeField] public GameObject comCanvas;
    [SerializeField] public GameObject subCamera;
    [SerializeField] public GameObject vButton;
    [SerializeField] public GameObject mainScreenUI;
    [SerializeField] public GameObject healthUI;
   
    // Start is called before the first frame update
    void Start()
    {
        for (VIRUS_NUM i = CODE_CLD; i < (VIRUS_NUM)V_CATEGORY; ++i)
            InitValue(vParents, vChildren, i); //ウイルス情報の初期化
        
        isGrabbedVirus = false; //何も掴んでいない状態
        buttonMode = 0; //ボタンの状態をoff
        element = 0; //要素格納変数をリセット
        column = 0; //列を0に初期化
        row = 0; //行を0に初期化

        for (int i = 0; i < V_CATEGORY; ++i){
            vParents[i].tag = VirusTagName[i]; //タグを保存
            vSetCount[i] = 0;
            vCreationCount[i] = 0;
        }

        //キャンバス上のUIの初期化
        comCanvas.SetActive(true);
        subCamera.SetActive(false);
        mainScreenUI.SetActive(false);
        healthUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        worldPos = ReturnOnScreenMousePos(); //スクリーン→ワールド変換

        vParents[buttonMode].setCount = GameObject.FindGameObjectsWithTag(vParents[buttonMode].tag).Length - 1; //ウイルスの設置数を計算

        AfterMenuAction(); //設置後メニューを開く

        if (vParents[buttonMode].setCount == 0) return; //指定のウイルスが存在してないとき、処理をスキップ
        UpdateViursPosition(); //ウイルスの位置を更新

        if (!isGrabbedVirus) return; //何も掴んでいないとき、処理をスキップ
        BeforeMenuAction(); //設置前メニューを開く
    }

    /// <summary>
    /// Pushing any buttons
    /// </summary>
    /// <param name="n">ウイルスの種類（番号）</param>
    public void VirusButtonPush(int n)
    {
        //if (vCreationCount[PrepareVirus.virusSetList[currentSetNum]] == 0) return; //ウイルスを持っていないとき、処理をスキップ
        if (vCreationCount[n] == vSetCount[n]) return;
        if (isOpenMenu) return; //メニュー開いているときはスキップ
        if (isGrabbedVirus && buttonMode != n) return; //ウイルスを持っているときかつ、別のボタンの状態のときはスキップ
        if (isLimitCapacity[n]) return; //例外処理ならスキップ

        isGrabbedVirus = ReverseFlag(isGrabbedVirus); //フラグを反転して、更新する
        SetUIActivity(false);

        if (isGrabbedVirus) CreateVirus(n); //ウイルスを作成する
        else Destroy(vObject[buttonMode, vParents[buttonMode].setCount - 1]); //ゲームオブジェクトを削除 // ウイルスを削除する

        column = buttonMode; //列（ウイルス種類）
        row = vParents[buttonMode].setCount; //行（ウイルス保有番号）
    }

    /// <summary>
    /// ウイルスを作成する
    /// </summary>
    /// <param name="n">ウイルスの種類（番号）</param>
    public void CreateVirus(int n)
    {
        GenerationVirus(vParents, vChildren, n); //ウイルス生成
        vPrefab = GameObject.Find(VirusHeadName + n.ToString()); //ウイルス番号に合致するPrefabを取得
        vObject[n, vParents[n].setCount] = Instantiate(vPrefab); //ゲームオブジェクトを生成
        vObject[n, vParents[n].setCount].transform.localScale = V_SIZE; //スケールサイズを取得
        vObject[n, vParents[n].setCount].transform.GetChild(0).gameObject.transform.localScale =
            vRange * vChildren[n, vParents[n].setCount].force.y; //範囲を取得
        vObject[n, vParents[n].setCount].SetActive(true); //アクティブ状態にする
        buttonMode = n; //現在のボタンの状態を更新
        vSetCount[n]++;
    }

    //掴んでいるウイルスの位置を更新
    private void UpdateViursPosition()
    {
        if (vChildren[column, row].isActivity)
            vObject[column, row].transform.position = worldPos; //マウスポインタと同じ位置
    }

    /// <summary>
    /// 設置前メニューを開く
    /// </summary>
    private void BeforeMenuAction()
    {
        if (isRangeCollision) return; //ウイルスの範囲同士が重なっているとき、処理をスキップ
        if (isOpenMenu) return; //メニュー表示時、処理をスキップ

        //右クリック時
        if (Input.GetMouseButtonDown(1))
        {
            SetUIActivity(false);
            OpenBeforeMenu(); //メニューを開く
            SaveVirusPosition(vChildren, column, row, vObject, worldPos); //設置したウイルス座標を保存
            isOpenMenu = true; //メニューフラグをtrue
        }
    }

    /// <summary>
    /// 設置後メニューを開く
    /// </summary>
    private void AfterMenuAction()
    {
        if (!isMouseCollider) return; //マウスポインタがウイルスの範囲に重なっていないとき、処理をスキップ
        if (isEnemyCollision) return;
        if (isGrabbedVirus) return; //ウイルスを掴んでいるとき、処理をスキップ
        
        //右クリック時
        if (Input.GetMouseButtonDown(1))
        {
            SetUIActivity(false);
            OpenAfterMenu(); //設置後メニューを開く
            isOpenMenu = true; //メニューフラグをtrue
            SearchVirusArray(); //ウイルス配列を検索する
            UpdateVirusArray(); //ウイルス配列の更新
        }
    }

    /// <summary>
    /// ウイルス配列の特定の要素を検索
    /// </summary>
    private void SearchVirusArray()
    {
        var list = new List<VirusChildren>(); //リスト定義
        VirusChildren[] array1 = vChildren.Cast<VirusChildren>().ToArray(); //配列を一次元化
        //全要素分繰り返す
        for (int i = 0; i < V_CATEGORY * OWNED; ++i)
        {
            //各ウイルスの座標と範囲オブジェクトの座標が一致するまで繰り返す
            if (array1[i].pos == rangeObj.transform.position)
            {
                element = i; //特定の要素を格納
                break; //繰り返しから抜け出す
            }
        }
    }

    /// <summary>
    /// ウイルス配列の情報を更新する
    /// </summary>
    private void UpdateVirusArray()
    {
        column = element / OWNED; //列（ウイルス種類）
        row = element % OWNED; //行（ウイルス保有番号）
        buttonMode = column; //ボタンの状態を更新
    }

    /// <summary>
    /// ウイルス爆発エフェクト
    /// </summary>
    /// <param name="pos"></param>
    public void ExplosionVirus(Vector3 pos)
    {
        explosion.transform.position = pos;
        explosion.Play();
    }

    //UI状態の設定
    public void SetUIActivity(bool state)
    {
        comCanvas.SetActive(state);
        subCamera.SetActive(state);
        vButton.SetActive(state);
        mainScreenUI.SetActive(state);
        healthUI.SetActive(state);
    }
}
