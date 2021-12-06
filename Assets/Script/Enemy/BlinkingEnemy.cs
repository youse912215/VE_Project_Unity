using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static MaterialManager;

public class BlinkingEnemy : MonoBehaviour
{
    public static void SetMaterials(GameObject obj)
    {
        if (!obj) return; //オブジェクトがなければ、

        if (obj.GetComponent<Renderer>()) obj.GetComponent<Renderer>().material = poison; //毒マテリアル
        foreach (Transform enemyChild in obj.transform){
            SetMaterials(enemyChild.gameObject); //子のマテリアルにセット
        }
    }
}
