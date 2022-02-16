using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static MaterialManager;

public class BlinkingEnemy : MonoBehaviour
{
    public static void SetMaterials(GameObject obj, Material mat)
    {
        if (!obj) return; //オブジェクトがなければ、

        if (obj.GetComponent<Renderer>()) obj.GetComponent<Renderer>().material = mat; //毒マテリアル
        foreach (Transform enemyChild in obj.transform){
            SetMaterials(enemyChild.gameObject, mat); //子のマテリアルにセット
        }
    }
}
