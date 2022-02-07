using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static CanvasManager;

public class WaveGauge : MonoBehaviour
{
    private const float DECREASE_AMOUNT = 5.0f;
    private const float maxGauge = 1000000.0f;
    private float currentGauge = 0.0f;
    public static int currentDay = 1;
    
    [SerializeField] private Slider slider; //Slider格納

    private void Start()
    {
        slider.value = 1; //Sliderを満タン
        currentGauge = maxGauge; //現在のHPに最大HPを代入
    }

    private void Update()
    {
        //Debug.Log(currentDay);

        //キャンバスモードがTowerDefense以外なら、処理をスキップ
        if (canvasMode != CANVAS_MODE.TOWER_DEFENCE_MODE) return;
        slider.value = currentGauge / maxGauge; //ゲージを更新

        if(currentDay != Scene.DAY) return;
        currentGauge -= DECREASE_AMOUNT; //現在のゲージを減少

        if(slider.value > 0) return; 
        currentGauge = maxGauge; //WAVEゲージをリセット
        currentDay++; //次のDAYへ移行
    }
}
