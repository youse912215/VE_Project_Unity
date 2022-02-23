using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

using static VirusMaterialData;
using static RAND.CreateRandom;
using static Call.CommonFunction;
using static Scene;
using static SynthesizeVirus;
using static Call.VirusData;

public class SuppliesVirus : MonoBehaviour
{
    private const int MAX_GET_NUM = 4; //最大入手量
    private const float WAIT_TIME = 0.5f; //待ち時間
    private readonly Vector3 ENABLED_POS = new Vector3(1000, 1000, 1000); //非表示の座標
    private int dayCount; //dayカウント

    public static bool isGetItem; //入手フラグ
    public static bool isSupplies; //支給フラグ
    public bool endCoroutine; //コルーチンフラグ

    private List<int> suppliesItemList = new List<int> {}; //支給アイテム名リスト
    private List<int> getItemNumList = new List<int> {}; //入手アイテム量リスト
    public static Text[] itemListText = new Text[V_CATEGORY]; //アイテムテキスト
    public static GameObject[] textObject = new GameObject[V_CATEGORY]; //テキストオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SynthesizeVirus>().InitText(V_CATEGORY, "supText", textObject, itemListText); //テキストの初期化
        dayCount = 0;
        isGetItem = false;
        isSupplies = true;
        endCoroutine = false;
    }

    // Update is called once per frame
    void Update()
    {
        //キャンバスモードがTowerDefense以外のとき、処理をスキップ
        if (CanvasManager.canvasMode != CanvasManager.CANVAS_MODE.SUPPLIES_MODE) return;
        //更新処理
        UpdateList();
        UpdateText();
    }

    /// <summary>
    /// 支給品リストを更新
    /// </summary>
    private void UpdateList()
    {
        if (dayCount != DAY) return;
        if (!isSupplies) return;
        suppliesItemList.Clear(); //リストをクリア
        getItemNumList.Clear(); //リストをクリア
        StartCoroutine(GetRandomInformation()); //ランダムでアイテム情報を入手するコルーチンをまわす
        textObject[0].transform.parent.gameObject.transform.localPosition = Vector3.zero; //表示
        dayCount++; //dayをカウント
    }

    /// <summary>
    /// 支給品テキストを更新
    /// </summary>
    private void UpdateText()
    {
        if (!endCoroutine) return;
        if (isSupplies) return;
        if (isGetItem) return;
        /* アイテム名 */
        itemListText[0].text = VIRUS_NAME[suppliesItemList[0]];
        itemListText[1].text = VIRUS_NAME[suppliesItemList[1]];
        itemListText[2].text = VIRUS_NAME[suppliesItemList[2]];
        itemListText[3].text = VIRUS_NAME[suppliesItemList[3]];
        /* アイテム量 */
        itemListText[4].text = "x" + getItemNumList[0].ToString();
        itemListText[5].text = "x" + getItemNumList[1].ToString();
        itemListText[6].text = "x" + getItemNumList[2].ToString();
        itemListText[7].text = "x" + getItemNumList[3].ToString();
    }

    /// <summary>
    /// 支給品を入手するボタンを押したとき
    /// </summary>
    public void PushGetSuppliesButton()
    {
        if (isGetItem) return;
        StartCoroutine(GetRandomItem()); //開始
        StopCoroutine(GetRandomItem()); //停止
        isGetItem = true; //入手済み
        textObject[0].transform.parent.gameObject.transform.localPosition = ENABLED_POS; //非表示
    }

    /// <summary>
    /// アイテム入手
    /// </summary>
    /// <param name="list"></param>
    /// <param name="n"></param>
    private void GetItem(List<int> list, int n)
    {
        list.Add((int)Integerization(rand % n)); //ランダム値を取得
        if (list != getItemNumList) return; //アイテム量リスト以外は、処理をスキップ
        if (list[list.Count() - 1] == 0) list[list.Count() - 1]++; //入手量が0のとき、1増やす
    }

    /// <summary>
    /// リストにあるランダムなアイテムを入手する
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetRandomItem()
    {
        for (int i = 0; i < MATERIAL_LIST_NUM; ++i)
        {
            yield return new WaitForSeconds(0.1f);
            vMatOwned[suppliesItemList[i]] += getItemNumList[i];
        }
    }

    /// <summary>
    /// ランダムでアイテム情報を入手
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetRandomInformation()
    {
        //アイテム量のリストが最後まで到達していない間、繰り返す
        while (getItemNumList.Count != MATERIAL_LIST_NUM)
        {
            //MATERIAL_LIST_NUM分回す
            for (int i = 0; i < MATERIAL_LIST_NUM; ++i)
            {
                yield return new WaitForSeconds(WAIT_TIME); //WAIT_TIME 待つ
                GetItem(suppliesItemList, vMatNam); //入手アイテム情報を取得
                GetItem(getItemNumList, MAX_GET_NUM); //アイテム量を取得

                //繰り返しの最後に、各フラグをセット
                if (i == MATERIAL_LIST_NUM - 1)
                {
                    isSupplies = false;
                    isGetItem = false;
                    endCoroutine = true;
                }
            }
        }

    }
}
