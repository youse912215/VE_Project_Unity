using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    /// <summary>
    /// デバッグ用
    /// </summary>
    /// <typeparam name="T">型</typeparam>
    /// <param name="value">値</param>
    public static void DebugFunc<T>(T value)
    {
        Debug.Log("{ " + value.GetType() + " }:" + value);
    }
}
