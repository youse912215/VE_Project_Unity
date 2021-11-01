using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.CommonFunction;
using static Call.ConstantValue.MENU_TYPE;
using static ShowMenu;
using static MouseCollision;

public class ActVirus : MonoBehaviour
{
    private Virus[,] virus = new Virus[CATEGORY, OWNED]; //ウイルス構造体配列
    private GameObject[,] virusObj = new GameObject[CATEGORY, OWNED]; //ウイルスオブジェクト配列
    private GameObject vPrefab; //ウイルスプレファブ

    private bool isGrabbedVirus; //ウイルスを掴んでいるか
    private Vector3 worldPos; //ワールド座標
    private int buttonMode; //ボタンの状態（ウイルスの種類）
    private bool isOpenMenu;

    private int storage;

    // Start is called before the first frame update
    void Start()
    {
        /* ウイルス情報の初期化 */
        InitValue(virus, CODE_CLD);
        InitValue(virus, CODE_INF);
        InitValue(virus, CODE_19);
        
        isGrabbedVirus = false;
        buttonMode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        worldPos = ReturnOnScreenMousePos(); //スクリーン→ワールド変換 //ウイルスを移動

        Debug.Log("ウイルス番号::" + storage);
        Debug.Log("Flag" + isGrabbedVirus);

        if (!isGrabbedVirus && isMouseCollider && Input.GetMouseButtonDown(1)){
            OpenAfterMenu(); //設置後メニューを開く
            isOpenMenu = true;
            SearchVirusArray();
        }

        if (virus[buttonMode, vNum[buttonMode]].isActivity)
            virusObj[buttonMode, vNum[buttonMode]].transform.position = worldPos; //ウイルスオブジェクトの位置をワールド座標で更新する
        if (!isGrabbedVirus) return;
        OpenVirusMenu(); //ウイルスメニューを開く
    }

    /// <summary>
    /// Pushing any buttons
    /// </summary>
    /// <param name="n">ウイルスの種類（番号）</param>
    public void VirusButtonPush(int n)
    {
        if (isOpenMenu) return; //メニュー開いているときはスキップ
        if (isGrabbedVirus && buttonMode != n) return; //ウイルスを持っているときかつ、別のボタンの状態のときはスキップ
        if (ProcessOutOfRange(n)) return; //例外処理ならスキップ

        isGrabbedVirus = ReverseFlag(isGrabbedVirus); //フラグを反転して、更新する

        if (isGrabbedVirus) CreateVirus(n); //ウイルスを作成する
        else DestroyBeforeVirus(); // ウイルスを削除する
    }

    /// <summary>
    /// Pushing set button
    /// </summary>
    public void SetButtonPush()
    {
        //if (!isGrabbedVirus) return;
        SetVirus(); //ウイルスを設置
        ReverseMenuFlag(BACK); //メニューを閉じる
        isOpenMenu = false;
    }

    /// <summary>
    /// Pushing back button
    /// </summary>
    public void BackButtonPush()
    {
        //if (!isGrabbedVirus) return;
        virus[buttonMode, vNum[buttonMode]].isActivity = true; //再びアクティブ状態に
        ReverseMenuFlag(BACK); //メニューを閉じる
        isOpenMenu = false;
    }

    /// <summary>
    /// pushing delete button
    /// </summary>
    public void DeleteButtonPush()
    {
        //if (!isGrabbedVirus) return;
        if (!menuMode)
        {
            DestroyBeforeVirus(); // ウイルスを削除する
            isGrabbedVirus = ReverseFlag(isGrabbedVirus);
        }
        else Destroy(virusObj[storage / OWNED, storage % OWNED]);

        ReverseMenuFlag(BACK); //メニューを閉じる
        isOpenMenu = false;
    }

    /// <summary>
    /// ウイルスを作成する
    /// </summary>
    /// <param name="n">ウイルスの種類（番号）</param>
    public void CreateVirus(int n)
    {
        GenerationVirus(virus, (VIRUS_NUM)n); //ウイルス生成
        vPrefab = GameObject.Find(VirusName + n.ToString()); //ウイルス番号に合致するPrefabを取得
        virusObj[n, vNum[n]] = Instantiate(vPrefab); //ゲームオブジェクトを生成
        virusObj[n, vNum[n]].SetActive(true); //アクティブ状態にする
        buttonMode = n; //現在のボタンの状態を更新
    }

    /// <summary>
    /// ウイルスを削除する
    /// </summary>
    private void DestroyBeforeVirus()
    {
        virus[buttonMode, vNum[buttonMode]].isActivity = false; //生存状態をfalse
        Destroy(virusObj[buttonMode, vNum[buttonMode]]); //ゲームオブジェクトを削除
    }

    /// <summary>
    /// ウイルスを設置
    /// </summary>
    private void SetVirus()
    {
        GetVirus((VIRUS_NUM)buttonMode); //ウイルスをアクティブにする
        isGrabbedVirus = false; //ボタン操作をfalse
    }

    /// <summary>
    /// ウイルスメニューを開く
    /// </summary>
    private void OpenVirusMenu()
    {
        if (isRangeCollision) return;
        if (isOpenMenu) return;

        if (Input.GetMouseButtonDown(1))
        {
            OpenBeforeMenu(); //メニューを開く
            SaveVirusPosition(virus, (VIRUS_NUM)buttonMode, virusObj, worldPos); //設置したウイルス座標を保存
            isOpenMenu = true;
        }
    }

    private void SearchVirusArray()
    {
        Virus[] ary = virus.Cast<Virus>().ToArray(); //配列を一次元化
        for (int i = 0; i < CATEGORY * OWNED; ++i)
        {
            //各ウイルスの座標と範囲オブジェクトの座標が一致するまで繰り返す
            if (ary[i].pos == rangeObj.transform.position)
            {
                storage = i; //特定の要素を格納
                break; //繰り返しから抜け出す
            }
        }
    }
}
