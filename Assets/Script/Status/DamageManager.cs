using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ColonyHealth;
using static Call.VirusData;

public class DamageManager : MonoBehaviour
{
    private const int MAX_LEVEL = 19; //最大レベル
    private const float AVAILABLE_MAX_EXP = 15.0f; //入手可能な最大経験値
    private const float EXPANSION_RANGE = 1.1f; //拡大範囲
    private const float RECOVERY_VALUE = 1000.0f; //回復量

    /// <summary>
    /// 経験値取得
    /// 乱数値 % (入手可能な最大経験値 * 敵ランク)
    /// </summary>
    /// <param name="enemyPower"></param>
    /// <returns></returns>
    public float GetExp(int enemyRank)
    {
        return  AVAILABLE_MAX_EXP + (enemyRank  + 1) * WaveGauge.currentDay * WaveGauge.currentDay;
    }

    /// <summary>
    /// コロニーレベルの計算
    /// 現在の経験値が必要経験値以上かつ、コロニーレベルが最大に達していないときレベルアップ
    /// </summary>
    /// <returns></returns>
    public int CulculationColonyLevel()
    {
        if (exp >= EXP_LIST[colonyLevel] && colonyLevel != MAX_LEVEL)
        {
            isLevelUp = true; //レベルアップ
            vRange *= EXPANSION_RANGE; //ウイルスの範囲を拡大
            maxHp += RECOVERY_VALUE;
            currentHp += RECOVERY_VALUE;
            return 1;
        }
        return 0;
    }
}
