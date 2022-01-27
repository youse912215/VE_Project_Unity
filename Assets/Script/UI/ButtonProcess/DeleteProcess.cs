using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static Call.CommonFunction;
using static Call.VirusData;
using static Call.ConstantValue.MENU_TYPE;
using static ShowMenu;

public class DeleteProcess : MonoBehaviour
{
    GameObject obj;
    ActVirus actV;

    // Start is called before the first frame update
    void Start()
    {
        actV = GetOtherScriptObject<ActVirus>(obj);
    }

    /// <summary>
    /// pushing delete button
    /// </summary>
    public void DeleteButtonPush()
    {
        int mode = actV.buttonMode;
        actV.SetUIActivity(true);

        if (!menuMode)
        {
            DestroyBeforeVirus(actV.buttonMode); // ウイルスを削除する
            actV.isGrabbedVirus = false;
        }
        else
        {
            Destroy(actV.vObject[actV.column, actV.row]); //ウイルスを削除する
            SortVirusArray(actV.column, actV.row, actV); //ウイルス配列をソート
        }
        
        ReverseMenuFlag(BACK); //メニューを閉じる
        actV.isOpenMenu = false; //メニューフラグをfalse

        //限界容量に達していないとき、処理をスキップ
        if (actV.vParents[mode].setCount != actV.vParents[mode].creationCount) return;
        isLimitCapacity[mode] = false; //容量の限界状態を解除
    }

    /// <summary>
    /// ウイルスを削除する
    /// </summary>
    private void DestroyBeforeVirus(int mode)
    {
        Destroy(actV.vObject[mode, actV.row]); //ゲームオブジェクトを削除
    }

    /// <summary>
    /// ウイルス配列をソートする
    /// </summary>
    private void SortVirusArray(int column, int row, ActVirus act)
    {
        var list1 = new List<VirusChildren>(); //リスト定義
        var list2 = new List<GameObject>(); //リスト定義
        VirusChildren[] structArray = act.vChildren.Cast<VirusChildren>().ToArray(); //ウイルス構造体配列を一次元化
        GameObject[] objectArray = act.vObject.Cast<GameObject>().ToArray(); //ウイルスゲームオブジェクト配列を一次元化

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
            act.vChildren[column, i] = list1[i];
            act.vObject[column, i] = list2[i];
        }
    }
}
