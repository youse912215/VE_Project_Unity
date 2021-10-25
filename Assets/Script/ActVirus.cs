using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.CommonFunction;
using static Call.ConstantValue.MENU_TYPE;
using static ShowMenu;


public class ActVirus : MonoBehaviour
{
    private Virus[,] virus = new Virus[CATEGORY, OWNED]; //ウイルス構造体配列
    private GameObject[,] virusObj = new GameObject[CATEGORY, OWNED]; //ウイルスオブジェクト配列
    private GameObject prefab; //ウイルスプレファブ

    private bool isGrabbedVirus; //ウイルスを掴んでいるか
    private Vector3 worldPos; //ワールド座標
    private int buttonMode; //ボタンの状態（ウイルスの種類）

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

    /// <summary>
    /// Pushing any buttons
    /// </summary>
    /// <param name="n">ウイルスの種類（番号）</param>
    public void VirusButtonPush(int n)
    {
        if (isGrabbedVirus && buttonMode != n) return;

        if (ProcessOutOfRange(n)) return;
        isGrabbedVirus = ReverseFlag(isGrabbedVirus);

        if (isGrabbedVirus) CreateVirus(n); //ウイルスを作成する
        else DestroyVirus(); // ウイルスを削除する
    }

    /// <summary>
    /// Pushing set button
    /// </summary>
    public void SetButtonPush()
    {
        if (!isGrabbedVirus) return;
        SetVirus(); //ウイルスを設置
        ReverseMenuFlag(BACK); //メニューを閉じる
    }

    /// <summary>
    /// Pushing back button
    /// </summary>
    public void BackButtonPush()
    {
        if (!isGrabbedVirus) return;
        virus[buttonMode, vNum[buttonMode]].isActivity = true; //再びアクティブ状態に
        ReverseMenuFlag(BACK); //メニューを閉じる
    }

    /// <summary>
    /// pushing delete button
    /// </summary>
    public void DeleteButtonPush()
    {
        if (!isGrabbedVirus) return;
        DestroyVirus(); // ウイルスを削除する
        isGrabbedVirus = ReverseFlag(isGrabbedVirus);
        ReverseMenuFlag(BACK); //メニューを閉じる
    }

    // Update is called once per frame
    void Update()
    {
        worldPos = ReturnOnScreenMousePos(); //スクリーン→ワールド変換 //ウイルスを移動

        //ウイルスオブジェクトの位置をワールド座標で更新する
        if (virus[buttonMode, vNum[buttonMode]].isActivity)
            virusObj[buttonMode, vNum[buttonMode]].transform.position = worldPos;

        if (!isGrabbedVirus) return;
        OpenVirusMenu(); //ウイルスメニューを開く
    }

    /// <summary>
    /// ウイルスを作成する
    /// </summary>
    /// <param name="n">ウイルスの種類（番号）</param>
    public void CreateVirus(int n)
    {
        GenerationVirus(virus, (VIRUS_NUM)n); //ウイルス生成
        prefab = GameObject.Find(VirusName + n.ToString()); //ウイルス番号に合致するPrefabを取得
        virusObj[n, vNum[n]] = Instantiate(prefab); //ゲームオブジェクトを生成
        virusObj[n, vNum[n]].SetActive(true); //アクティブ状態にする
        buttonMode = n; //現在のボタンの状態を更新
    }

    /// <summary>
    /// ウイルスを削除する
    /// </summary>
    private void DestroyVirus()
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
        if (Input.GetMouseButtonDown(1))
        {
            OpenMenu(); //メニューを開く
            SaveVirusPosition(virus, (VIRUS_NUM)buttonMode, virusObj, worldPos); //設置したウイルス座標を保存
        }
    }
}
