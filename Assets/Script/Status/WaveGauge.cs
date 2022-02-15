using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static CanvasManager;
using static WarriorData;

public class WaveGauge : MonoBehaviour
{
    //private const int DECREASE_AMOUNT = 5;
    private int maxGauge;
    private int currentGauge = 0;
    public static int currentDay = 1;
    
    [SerializeField] private Slider slider; //Slider格納

    private void Start()
    {
        maxGauge = ENEMY_COUNTS_PER_WAVE[0];
        slider.value = 1; //Sliderを満タン
        currentGauge = maxGauge; //現在のHPに最大HPを代入
    }

    private void Update()
    {
        //Debug.Log("残りウェーブゲージ::" + currentGauge + " 現在::" + currentDay + " DAY::" + Scene.DAY);

        //キャンバスモードがTowerDefense以外なら、処理をスキップ
        if (canvasMode != CANVAS_MODE.TOWER_DEFENCE_MODE) return;

        if (Input.GetKeyDown(KeyCode.A)) Destroy(GameObject.Find("YellowDamage"));
        
        slider.value = (float)currentGauge / maxGauge; //ゲージを更新

        if(currentDay != Scene.DAY) return;
        if(slider.value > 0) return; 
        
        currentDay++; //次のDAYへ移行
        maxGauge = ENEMY_COUNTS_PER_WAVE[currentDay - 1]; //WAVEゲージをリセット
        currentGauge = maxGauge;
        deadCount = 0;
        this.GetComponent<ActEnemy>().ResetSurvivalCount();
        this.GetComponent<CanvasManager>().PushSuppliesScreenButton();

        Scene.DAY++;
        //Debug.Log("[DAY " + Scene.DAY + " ]");
    }

    public void UpdateGauge()
    {
        currentGauge = maxGauge - deadCount;
    }
}
