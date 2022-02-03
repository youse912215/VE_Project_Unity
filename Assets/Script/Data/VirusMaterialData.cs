using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static DebugManager;

public class VirusMaterialData : MonoBehaviour
{
    //�E�C���X�f�ރR�[�h
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

    //�E�C���X�쐬�K�v��
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

    public static int vMatNam = System.Enum.GetNames(typeof(MATERIAL_CODE)).Length - 1; //�E�C���X�f�ސ�
    public static int[] vMatOwned = new int[vMatNam]; //�e��f�ނۗ̕L��
    public static int[] vCreationCount = new int[8]; //�쐬��
    public static int[] vDestroyCount = new int[8]; //�j��
    public static int[] vSetCount = new int[8];
    public const int MATERIAL_LIST_NUM = 4;

    /// <summary>
    /// ���݂ۗ̕L���̏�����
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

    public static string[] VIRUS_NAME = {
        "V1.protein", "V2.protein", "V3.protein", "V24.protein",
        "V40.protein", "S.protein", "N.protein", "FEN1.protein",
        "ULX.protein", "F1.antigen", "V.antigen", "H.agglutinin",
        "Protease", "Neuraminidase", "Envelope", "Nucleoccapsid",
        "Polymerase", "B.portion", "R.portion", "Y.portion",
        "Bl.portion",
    };

    /// <summary>
    /// �ۗL���Ă���E�C���X�̌����v�Z����
    /// </summary>
    /// <param name="list">�E�C���X�����f�ރ��X�g</param>
    /// <param name="c">���ݑI�����Ă���E�C���X�R�[�h</param>
    /// <param name="o">���Z�����Z���i1 or -1�j</param>
    public static void CulcOwnedVirus(MATERIAL_CODE[,] list, VIRUS_NUM c, int o)
    {
        //�z��̍s�����J��Ԃ�
        for (int i = 0; i < 4; ++i)
           vMatOwned[(int)list[(int)c, i]] += requiredMaterials[(int)c, i] * o;
    }

    public static void SaveOwnedVirusMaterial(MATERIAL_CODE[,] list, List<int> tmp, VIRUS_NUM c)
    {
        tmp.RemoveRange(0, 4);

        //��x���X�g�ɕۑ�
        for(int i = 0; i < 4; ++i)
            tmp.Add(vMatOwned[(int)list[(int)c, i]]);
    }

    public static void ResetOwnedVirusMaterial(MATERIAL_CODE[,] list, List<int> tmp, VIRUS_NUM c)
    {
        //�z��̍s�����J��Ԃ�
        for (int i = 0; i < 4; ++i)
            vMatOwned[(int)list[(int)c, i]] = tmp[i];
    }

    /// <summary>
    /// �f�ނ̌����m�F����
    /// </summary>
    /// <param name="list">�E�C���X�����f�ރ��X�g</param>
    /// <param name="c">���ݑI�����Ă���E�C���X�R�[�h</param>
    /// <returns></returns>
    public static bool CheckMaterialCount(MATERIAL_CODE[,] list, VIRUS_NUM c)
    {
        //�z��̍s�����J��Ԃ�
        for (int i = 0; i < 4; ++i)
            if (requiredMaterials[(int)c, i] > vMatOwned[(int)list[(int)c, i]]) return false; //��v���Ȃ��Ƃ�false��Ԃ�

        return true; //���ׂĈ�v�����Ƃ�true��Ԃ�
    }

    public static void SaveCreationVirus(int[] array, VIRUS_NUM c)
    {
        int n = (int)c;
        vCreationCount[n] += array[n];
    }
}
