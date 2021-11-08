using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static DebugManager;

public class VirusMaterialData : MonoBehaviour
{
    public const int matNum = 18;

    public enum MATERIAL_CODE
    {
        V1_PROTEIN,
        V2_PROTEIN,
        V3_PROTEIN,
        V24_PROTEIN,
        V40_PROTEIN,
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

    public static int[,] REQUIRED_MATERIALS = {
        { 1, 1, 1, 1 }, //Cld
        { 2, 1, 3, 1 }, //Inf
        { 3, 3, 3, 1 }, //19
        { 1, 2, 2, 1 }, //Nov
        { 1, 1, 2, 1 }, //Ehf
        { 2, 2, 1, 1 }, //Ev
        { 2, 3, 3, 1 }, //Bd
        { 5, 5, 10, 1 }, //Ult
    };

    public static int vMatNam = System.Enum.GetNames(typeof(MATERIAL_CODE)).Length - 1; //�E�C���X�f�ސ�
    public static int[] vMatOwned = new int[vMatNam]; //�e��f�ނۗ̕L��

    /// <summary>
    /// �f�ނ̌����m�F����
    /// </summary>
    /// <param name="m"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool CheckMaterialCount(MATERIAL_CODE m1, MATERIAL_CODE m2, 
        MATERIAL_CODE m3, MATERIAL_CODE m4, VIRUS_NUM c)
    {
        int[] list = { (int)m1, (int)m2, (int)m3, (int)m4 };
        
        //�z�񕪌J��Ԃ�
        for (int i = 0; i < list.Length; ++i)
            if (REQUIRED_MATERIALS[(int)c, i] > vMatOwned[list[i]]) return false; //��v���Ȃ��Ƃ�false��Ԃ�

        return true; //���ׂĈ�v�����Ƃ�true��Ԃ�
    }
}
