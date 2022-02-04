using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.CommonFunction;
using static RAND.CreateRandom;
using static ColonyHealth;

public class DamageManager : MonoBehaviour
{
    private const int MAX_LEVEL = 19; //最大レベル
    private const float AVAILABLE_MAX_EXP = 30.0f; //入手可能な最大経験値
    private const float LOWER_LIMIT = 5.0f; //下限値

    /// <summary>
    /// 経験値取得
    /// 乱数値 % (入手可能な最大経験値 * 敵ランク)
    /// </summary>
    /// <param name="enemyPower"></param>
    /// <returns></returns>
    public float GetExp(int enemyRank)
    {
        return LOWER_LIMIT + Integerization(rand) % (AVAILABLE_MAX_EXP * enemyRank);
    }

    /// <summary>
    /// コロニーレベルの計算
    /// 現在の経験値が必要経験値以上かつ、コロニーレベルが最大に達していないときレベルアップ
    /// </summary>
    /// <returns></returns>
    public int CulculationColonyLevel()
    {
        return (exp >= EXP_LIST[colonyLevel] && colonyLevel != MAX_LEVEL) ? 1 : 0;
    }
}
