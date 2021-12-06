using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static MaterialManager;

public class BlinkingEnemy : MonoBehaviour
{
    public static void SetMaterials(GameObject obj)
    {
        if (!obj) return; //�I�u�W�F�N�g���Ȃ���΁A

        if (obj.GetComponent<Renderer>()) obj.GetComponent<Renderer>().material = poison; //�Ń}�e���A��
        foreach (Transform enemyChild in obj.transform){
            SetMaterials(enemyChild.gameObject); //�q�̃}�e���A���ɃZ�b�g
        }
    }
}
