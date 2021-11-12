using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static DebugManager;

public class VirusMaterialData : MonoBehaviour
{
    //ウイルス素材コード
    public enum MATERIAL_CODE
    {
        V1_PROTEIN,
        V2_PROTEIN,
        V3_PROTEIN,
        V24_PROTEIN,
        V40_PROTEIN,
        S_PROTEIN,
        N_PROTEIN,
        FEN1_PROTEIN,
        ULX_PROTEIN,
        F1_ANTIGEN,
        V_ANTIGEN,
        ERYTHROCYTE_AGGLUTININ,
        PROTEASE,
        NEURAMINIDASE,
        ENVELOPE,
        NUCLEOCCAPSID,
        POLYMERASE,
        BLUE_POTION,
        RED_POTION,
        YELLOW_POTION,
        BLOOD_POTION,

        NONE = 99
    }

    //ウイルス作成必要個数
    public static int[,] requiredMaterials = {
        { 1, 1, 1, 1 }, //Cld
        { 2, 1, 3, 1 }, //Inf
        { 3, 3, 3, 1 }, //19
        { 1, 2, 2, 1 }, //Nov
        { 1, 1, 2, 1 }, //Ehf
        { 2, 2, 1, 1 }, //Ev
        { 2, 3, 3, 1 }, //Bd
        { 5, 5, 10, 1 }, //Ult
    };

    public static int vMatNam = System.Enum.GetNames(typeof(MATERIAL_CODE)).Length - 1; //ウイルス素材数
    public static int[] vMatOwned = new int[vMatNam]; //各種素材の保有数
    public static int[] vCreationCount = new int[8];

    /// <summary>
    /// 現在の保有数の初期化
    /// </summary>
    public static void InitOwnedVirus()
    {
        vMatOwned[0] = 20;
        vMatOwned[1] = 30;
        vMatOwned[2] = 10;
        vMatOwned[3] = 20;
        vMatOwned[4] = 30;
        vMatOwned[5] = 10;
        vMatOwned[6] = 20;
        vMatOwned[7] = 30;
        vMatOwned[8] = 10;
        vMatOwned[9] = 20;
        vMatOwned[10] = 30;
        vMatOwned[11] = 40;
        vMatOwned[12] = 10;
        vMatOwned[13] = 40;
        vMatOwned[14] = 10;
        vMatOwned[15] = 10;
        vMatOwned[16] = 10;
        vMatOwned[17] = 10;
        vMatOwned[18] = 40;
        vMatOwned[19] = 10;
        vMatOwned[20] = 10;
        
    }

    /// <summary>
    /// 保有しているウイルスの個数を計算する
    /// </summary>
    /// <param name="list">ウイルス合成素材リスト</param>
    /// <param name="c">現在選択しているウイルスコード</param>
    /// <param name="o">加算か減算か（1 or -1）</param>
    public static void CulcOwnedVirus(MATERIAL_CODE[,] list, VIRUS_NUM c, int o)
    {
        //配列の行数分繰り返す
        for (int i = 0; i < 4; ++i)
           vMatOwned[(int)list[(int)c, i]] += requiredMaterials[(int)c, i] * o;
    }

    public static void SaveOwnedVirusMaterial(MATERIAL_CODE[,] list, List<int> tmp, VIRUS_NUM c)
    {
        tmp.RemoveRange(0, 4);

        for(int i = 0; i < 4; ++i)
            tmp.Add(vMatOwned[(int)list[(int)c, i]]);

        //配列の行数分繰り返す
        for (int i = 0; i < 4; ++i)
            vMatOwned[(int)list[(int)c, i]] = tmp[i];

        //DebugFunc(vMatOwned[(int)list[(int)c, 0]]);
        //DebugFunc(vMatOwned[(int)list[(int)c, 1]]);
        //DebugFunc(vMatOwned[(int)list[(int)c, 2]]);
        //DebugFunc(vMatOwned[(int)list[(int)c, 3]]);
    }

    /// <summary>
    /// 素材の個数を確認する
    /// </summary>
    /// <param name="list">ウイルス合成素材リスト</param>
    /// <param name="c">現在選択しているウイルスコード</param>
    /// <returns></returns>
    public static bool CheckMaterialCount(MATERIAL_CODE[,] list, VIRUS_NUM c)
    {
        //配列の行数分繰り返す
        for (int i = 0; i < 4; ++i)
            if (requiredMaterials[(int)c, i] > vMatOwned[(int)list[(int)c, i]]) return false; //一致しないときfalseを返す

        return true; //すべて一致したときtrueを返す
    }

    public static void SaveCreationVirus(int[] array, VIRUS_NUM c)
    {
        int n = (int)c;
        vCreationCount[n] += array[n];
    }
}
