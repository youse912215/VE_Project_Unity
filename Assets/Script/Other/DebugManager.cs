using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    /// <summary>
    /// �f�o�b�O�p
    /// </summary>
    /// <typeparam name="T">�^</typeparam>
    /// <param name="value">�l</param>
    public static void DebugFunc<T>(T value)
    {
        Debug.Log("{ " + value.GetType() + " }:" + value);
    }
}
