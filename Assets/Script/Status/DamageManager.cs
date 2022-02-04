using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.CommonFunction;
using static RAND.CreateRandom;
using static ColonyHealth;

public class DamageManager : MonoBehaviour
{
    private const int MAX_LEVEL = 19; //�ő僌�x��
    private const float AVAILABLE_MAX_EXP = 30.0f; //����\�ȍő�o���l
    private const float LOWER_LIMIT = 5.0f; //�����l

    /// <summary>
    /// �o���l�擾
    /// �����l % (����\�ȍő�o���l * �G�����N)
    /// </summary>
    /// <param name="enemyPower"></param>
    /// <returns></returns>
    public float GetExp(int enemyRank)
    {
        return LOWER_LIMIT + Integerization(rand) % (AVAILABLE_MAX_EXP * enemyRank);
    }

    /// <summary>
    /// �R���j�[���x���̌v�Z
    /// ���݂̌o���l���K�v�o���l�ȏォ�A�R���j�[���x�����ő�ɒB���Ă��Ȃ��Ƃ����x���A�b�v
    /// </summary>
    /// <returns></returns>
    public int CulculationColonyLevel()
    {
        return (exp >= EXP_LIST[colonyLevel] && colonyLevel != MAX_LEVEL) ? 1 : 0;
    }
}
