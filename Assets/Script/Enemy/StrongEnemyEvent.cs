using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StrongEnemyEvent : MonoBehaviour
{
    [SerializeField] private Text message; //メッセージテキスト
    private Image image; //格納用

    //定数
    private const int WAR_TIMES = 2; //回数
    private const float INTERVAL = 1.0f; //間隔時間

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Stop();
        image = GetComponent<Image>();
        this.image.enabled = false;
    }

    /// <summary>
    /// UIの有効状態を切替
    /// </summary>
    /// <param name="state"></param>
    private void ChangeEnabled(bool state)
    {
        this.image.enabled = state;
        message.enabled = state;
    }

    /// <summary>
    /// 警告するコルーチン
    /// </summary>
    public IEnumerator WarningCoroutine()
    {
        //初期化
        GetComponent<AudioSource>().Play();
        message.text = "強敵出現!";
        var state = true;

        //WAR_TIMES * 2回分回す
        for (int i = 0; i < WAR_TIMES * 2; ++i)
        {
            ChangeEnabled(state); //状態を切替
            yield return new WaitForSeconds(INTERVAL); //待機時間
            state = !state; //bool反転
        }

        //終了処理
        message.text = "";
        GetComponent<AudioSource>().Stop();
    }
}
