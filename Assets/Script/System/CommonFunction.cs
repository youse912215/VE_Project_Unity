using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Call
{
    public class CommonFunction : MonoBehaviour
    {
        private const string objName = "GameManager"; //ゲームオブジェクト名

        /// <summary>
        /// flagを反転する
        /// </summary>
        public static bool ReverseFlag(bool flag)
        {
            return !flag ? true : false;
        }

        /// <summary>
        /// 他のオブジェクトのマテリアルを指定のマテリアルカラーに変える
        /// </summary>
        /// <param name="other">他のオブジェクト</param>
        /// <param name="mtl">マテリアル</param>
        public static void ChangeMaterialColor(GameObject obj, Material mtl)
        {
            obj.GetComponent<Renderer>().material = mtl; //マテリアルを代入
        }

        public static T GetActVirusScript<T>(GameObject obj)
        {
            obj = GameObject.Find(objName);
            return obj.GetComponent<T>();
        }
    }
}


