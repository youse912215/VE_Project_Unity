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

        public static float Integerization(int n)
        {
            return (n <= -1) ? n * -1.0f : n * 1.0f;
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

        public static GameObject GetHierarchyObject(string s)
        {
            return GameObject.Find(s);
        }

        /// <summary>
        /// 他のスクリプトのオブジェクトを取得
        /// </summary>
        /// <typeparam name="T">他のスクリプト</typeparam>
        /// <param name="obj">オブジェクト</param>
        public static T GetOtherScriptObject<T>(GameObject obj)
        {
            obj = GameObject.Find(objName);
            return obj.GetComponent<T>();
        }
    }
}


