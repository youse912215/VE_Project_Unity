using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static CanvasManager;


public class WaveGauge : MonoBehaviour
{
    //private const int DECREASE_AMOUNT = 5;
    private int maxGauge;
    private int currentGauge = 0;
    public static int currentDay = 1;
    
    [SerializeField] private Slider slider; //Slider格納

    private void Start()
    {
        maxGauge = WarriorData.ENEMY_COUNTS_PER_WAVE[currentDay];
        slider.value = 1; //Sliderを満タン
        currentGauge = maxGauge; //現在のHPに最大HPを代入
    }

    private void Update()
    {
        Debug.Log(currentGauge);

        //キャンバスモードがTowerDefense以外なら、処理をスキップ
        if (canvasMode != CANVAS_MODE.TOWER_DEFENCE_MODE) return;
        
        slider.value = (float)currentGauge / maxGauge; //ゲージを更新

        if(currentDay != Scene.DAY) return;

        if(slider.value > 0) return; 
        currentGauge = maxGauge; //WAVEゲージをリセット
        currentDay++; //次のDAYへ移行
    }

    public void UpdateGauge()
    {
        currentGauge = maxGauge - WarriorData.deadCount;
    }
}
