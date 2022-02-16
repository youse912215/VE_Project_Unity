using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static MaterialManager;

public class BlinkingEnemy : MonoBehaviour
{
    public static void SetMaterials(GameObject obj, Material mat)
    {
        if (!obj) return; //�I�u�W�F�N�g���Ȃ���΁A

        if (obj.GetComponent<Renderer>()) obj.GetComponent<Renderer>().material = mat; //�Ń}�e���A��
        foreach (Transform enemyChild in obj.transform){
            SetMaterials(enemyChild.gameObject, mat); //�q�̃}�e���A���ɃZ�b�g
        }
    }
}
