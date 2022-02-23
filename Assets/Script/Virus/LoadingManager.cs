using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{ 
    [SerializeField] private Image loadImage; //ロード画像
    [SerializeField] private Text hintText; //ヒントテキスト

    private readonly Color ADD_LOAD_COLOR = new Color(0.001f, 0.001f, 0.001f, 0.0f); //読み込み時の加算色
    private const float ADD_COUNT = 0.005f; //加算カウント
    private const float LOAD_COUNT = 5.0f; //ロード時間
    private float count; //カウント量
    private List<string> comma = new List<string>{" ", " .", " ..", " ...", " ....", " ....."}; //ロード時の点

    public bool isLoading; //ロードフラグ

    private void Start()
    {
        count = 0.0f;
        isLoading = true;
    }

    // Start is called before the first frame update
    private void Update()
    {
        hintText.text = "Day " + (Scene.DAY + 1).ToString() + comma[(int)count];

        if(isLoading && this.GetComponent<SuppliesVirus>().endCoroutine)
            StartCoroutine(CountLoadTime()); //ロード開始

        if (count < LOAD_COUNT) return; //ロード時間に満たないとき、処理をスキップ
        this.GetComponent<CanvasManager>().LoadCanvasEnabled(false); //キャンバスをoff
        StopCoroutine(CountLoadTime()); //ロードのコルーチンを停止
        count = 0.0f; //カウントを初期化
        isLoading = false; //ロードフラグをfalse
        this.GetComponent<SuppliesVirus>().endCoroutine = false; //コルーチンフラグをfalse       
    }

    /// <summary>
    /// ロード時間をカウント
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountLoadTime()
    {
        //ロードフラグがtrueの間、繰り返す
        while (isLoading)
        {
            yield return new WaitForSeconds(0.1f);
            loadImage.color += ADD_LOAD_COLOR; //RGB値を加算
            count += ADD_COUNT; //カウント
        }
        
    }
}
