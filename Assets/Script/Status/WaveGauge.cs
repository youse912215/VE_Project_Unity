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
    private int currentGauge;
    public static int currentDay;
    
    [SerializeField] private Slider slider; //Slider格納
    [SerializeField] private AudioSource seNext;
    [SerializeField] public AudioSource seBlood;

    private void Start()
    {
        currentDay = 1;
        maxGauge = ENEMY_COUNTS_PER_WAVE[0];
        slider.value = 1; //Sliderを満タン
        currentGauge = maxGauge; //現在のHPに最大HPを代入
    }

    private void Update()
    {
        Debug.Log("残りウェーブゲージ::" + currentGauge + " 現在::" + currentDay + " DAY::" + Scene.DAY);

        //キャンバスモードがTowerDefense以外なら、処理をスキップ
        if (canvasMode != CANVAS_MODE.TOWER_DEFENCE_MODE) return;

        if (Input.GetKeyDown(KeyCode.A)) Destroy(GameObject.Find("YellowDamage"));
        
        slider.value = (float)currentGauge / maxGauge; //ゲージを更新

        if(currentDay != Scene.DAY) return;
        if(slider.value > 0) return; 
        
        seNext.time = 500.0f;
        seNext.PlayOneShot(seNext.clip);
        currentDay++; //次のDAYへ移行
        maxGauge = ENEMY_COUNTS_PER_WAVE[currentDay - 1]; //WAVEゲージをリセット
        currentGauge = maxGauge;
        deadCount = 0;
        GameObject.Find("Main Camera").GetComponent<CameraManager>().PushChangeButton();
        this.GetComponent<ActEnemy>().ResetSurvivalCount();
        this.GetComponent<CanvasManager>().PushSuppliesScreenButton();
        this.GetComponent<CanvasManager>().LoadCanvasEnabled(true);
        this.GetComponent<CanvasManager>().BackGroundEnabled(true);
        this.GetComponent<LoadingManager>().isLoading = true; //ロード開始
        canvasMode = CANVAS_MODE.SUPPLIES_MODE;

        //Scene.DAY++;
        //Debug.Log("[DAY " + Scene.DAY + " ]");
    }

    public void UpdateGauge()
    {
        currentGauge = maxGauge - deadCount;
    }
}
