using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.CommonFunction;
using static Call.ConstantValue.MENU_TYPE;
using static ShowMenu;
using static MouseCollision;

public class ActVirus : MonoBehaviour
{
    private VirusParents[] vParents = new VirusParents[CATEGORY]; //親ウイルス構造体配列
    private VirusChildren[,] vChildren = new VirusChildren[CATEGORY, OWNED]; //子ウイルス構造体配列
    private GameObject[,] vObject = new GameObject[CATEGORY, OWNED]; //ウイルスオブジェクト配列
    private GameObject vPrefab; //ウイルスプレファブ

    private bool isGrabbedVirus; //ウイルスを掴んでいるか
    private Vector3 worldPos; //ワールド座標
    private int buttonMode; //ボタンの状態（ウイルスの種類）
    private bool isOpenMenu; //メニューフラグ
    private int element; //要素格納変数
   
    // Start is called before the first frame update
    void Start()
    {
        for (VIRUS_NUM i = CODE_CLD; i < (VIRUS_NUM)CATEGORY; ++i)
            InitValue(vParents, vChildren, i); //ウイルス情報の初期化
        
        isGrabbedVirus = false; //何も掴んでいない状態
        buttonMode = 0; //ボタンの状態をoff

        for (int i = 0; i < CATEGORY; ++i)
            vParents[i].tag = VirusTagName[i]; //タグを保存
        vParents[0].creationCount = 5; //作成したウイルスを代入
        vParents[1].creationCount = 5;
        vParents[2].creationCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        worldPos = ReturnOnScreenMousePos(); //スクリーン→ワールド変換 //ウイルスを移動

        vParents[buttonMode].setCount = GameObject.FindGameObjectsWithTag(vParents[buttonMode].tag).Length - 1; //ウイルスの設置数を計算

        if (!isGrabbedVirus && isMouseCollider && Input.GetMouseButtonDown(1)){
            OpenAfterMenu(); //設置後メニューを開く
            isOpenMenu = true; //メニューフラグをtrue
            SearchVirusArray(); //ウイルス配列を検索する
        }

        if (vParents[buttonMode].setCount == 0) return; //指定のウイルスが存在してないとき、処理をスキップ
        //ウイルスオブジェクトの位置をワールド座標で更新する
        if (vChildren[buttonMode, vParents[buttonMode].setCount - 1].isActivity)
            vObject[buttonMode, vParents[buttonMode].setCount - 1].transform.position = worldPos;
        if (!isGrabbedVirus) return; //何も掴んでいないとき、処理をスキップ
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
        if (isLimitCapacity[n]) return; //例外処理ならスキップ

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
        if (!menuMode)
        {
            vChildren[buttonMode, vParents[buttonMode].setCount - 1].isActivity = true; //再びアクティブ状態に
            ReverseMenuFlag(BACK); //メニューを閉じる
            isOpenMenu = false;
        }
        else
        {

        }
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
        else
        {
            int column = element / OWNED; //列（ウイルス種類）
            int row = element % OWNED; //行（ウイルス保有番号）
            if (vParents[column].setCount == vParents[column].creationCount)
                isLimitCapacity[column] = false; //容量の限界状態を解除   
            Destroy(vObject[column, row]); //ウイルスを削除する
            SortVirusArray(column, row); //ウイルス配列をソート
        }

        ReverseMenuFlag(BACK); //メニューを閉じる
        isOpenMenu = false; //メニューフラグをfalse
    }

    /// <summary>
    /// ウイルスを作成する
    /// </summary>
    /// <param name="n">ウイルスの種類（番号）</param>
    public void CreateVirus(int n)
    {
        GenerationVirus(vParents, vChildren, (VIRUS_NUM)n); //ウイルス生成
        vPrefab = GameObject.Find(VirusHeadName + n.ToString()); //ウイルス番号に合致するPrefabを取得
        vObject[n, /*vNum[n]*/vParents[n].setCount] = Instantiate(vPrefab); //ゲームオブジェクトを生成
        vObject[n, /*vNum[n]*/vParents[n].setCount].SetActive(true); //アクティブ状態にする
        buttonMode = n; //現在のボタンの状態を更新
    }

    /// <summary>
    /// ウイルスを削除する
    /// </summary>
    private void DestroyBeforeVirus()
    {
        Destroy(vObject[buttonMode, vParents[buttonMode].setCount - 1]); //ゲームオブジェクトを削除
    }

    /// <summary>
    /// ウイルスを設置
    /// </summary>
    private void SetVirus()
    {
        GetVirus((VIRUS_NUM)buttonMode, vParents); //ウイルスをアクティブにする
        isGrabbedVirus = false; //ボタン操作をfalse
    }

    /// <summary>
    /// ウイルスメニューを開く
    /// </summary>
    private void OpenVirusMenu()
    {
        if (isRangeCollision) return; //ウイルスの範囲同士が重なっているとき、処理をスキップ
        if (isOpenMenu) return; //メニュー表示時、処理をスキップ

        //右クリック時
        if (Input.GetMouseButtonDown(1))
        {
            OpenBeforeMenu(); //メニューを開く
            SaveVirusPosition(vParents, vChildren, (VIRUS_NUM)buttonMode, vObject, worldPos); //設置したウイルス座標を保存
            isOpenMenu = true; //メニューフラグをtrue
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
        for (int i = 0; i < CATEGORY * OWNED; ++i)
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
    /// ウイルス配列をソートする
    /// </summary>
    private void SortVirusArray(int column, int row)
    {
        var list1 = new List<VirusChildren>(); //リスト定義
        var list2 = new List<GameObject>(); //リスト定義
        VirusChildren[] structArray = vChildren.Cast<VirusChildren>().ToArray(); //ウイルス構造体配列を一次元化
        GameObject[] objectArray = vObject.Cast<GameObject>().ToArray(); //ウイルスゲームオブジェクト配列を一次元化

        //指定のウイルスのリストを作成
        for (int i = 0; i < OWNED; ++i)
        {
            list1.Add(structArray[column * OWNED + i]);
            list2.Add(objectArray[column * OWNED + i]);
        }

        //要素を末尾に追加
        list1.Add(list1[row]); 
        list2.Add(list2[row]);
        //指定の要素を削除
        list1.RemoveAt(row);
        list2.RemoveAt(row); 

        //整列させたゲームオブジェクトリストに更新
        for (int i = 0; i < OWNED; ++i)
        {
            vChildren[column, i] = list1[i];
            vObject[column, i] = list2[i];
        }
    }
}
