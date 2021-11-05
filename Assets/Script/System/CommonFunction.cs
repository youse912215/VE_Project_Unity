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
        public static void ChangeRangeColor(GameObject obj, Material mtl)
        {
            //obj = other.gameObject; //他ゲームオブジェクトを取得
            obj.GetComponent<Renderer>().material = mtl; //マテリアルを代入
        }

        public static T GetActVirusScript<T>(GameObject obj)
        {
            obj = GameObject.Find(objName);
            return obj.GetComponent<T>();
        }

        public static void culc(GameObject obj, Vector3[] v, int count)
        {
            for (int i = 0; i < count; ++i)
            v[i] = obj.transform.position;
        }
    }
}


