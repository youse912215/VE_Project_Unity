using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static CanvasManager;
using static WarriorData;

public class WaveGauge : MonoBehaviour
{
    //private const int DECREASE_AMOUNT = 5;
    private int maxGauge;
    private int currentGauge;
    public static int currentDay;
    
    public bool isEventDanger;
    private bool isWin;
    private const int END_WAVE = 6;

    [SerializeField] private Slider slider; //Slider格納
    [SerializeField] private GameObject redEventObject;
    [SerializeField] private AudioSource seNext;
    [SerializeField] public AudioSource seBlood;
    
    private void Start()
    {
        currentDay = 1;
        maxGauge = ENEMY_COUNTS_PER_WAVE[0];
        slider.value = 1; //Sliderを満タン
        currentGauge = maxGauge; //現在のHPに最大HPを代入
        isEventDanger = false;
        isWin = false;
    }

    private void Update()
    {
        Debug.Log("残りウェーブゲージ::" + currentGauge + " 現在::" + currentDay + " DAY::" + Scene.DAY);

        //キャンバスモードがTowerDefense以外なら、処理をスキップ
        if (canvasMode != CANVAS_MODE.TOWER_DEFENCE_MODE) return;

        EventDanger();
        
        slider.value = (float)currentGauge / maxGauge; //ゲージを更新

        if(currentDay != Scene.DAY) return;
        if(slider.value > 0) return; 
        
        WinGame(); //クリア判定
        seNext.PlayOneShot(seNext.clip);

        if (isWin) return;
        
        currentDay++; //次のDAYへ移行
        maxGauge = ENEMY_COUNTS_PER_WAVE[currentDay - 1]; //WAVEゲージをリセット
        currentGauge = maxGauge;
        deadCount = 0;
        
        this.GetComponent<ActEnemy>().ResetSurvivalCount();
        this.GetComponent<CanvasManager>().PushSuppliesScreenButton();
        this.GetComponent<CanvasManager>().LoadCanvasEnabled(true);
        this.GetComponent<CanvasManager>().BackGroundEnabled(true);
        this.GetComponent<LoadingManager>().isLoading = true; //ロード開始
        canvasMode = CANVAS_MODE.SUPPLIES_MODE;

        if (!GameObject.Find("Main Camera").GetComponent<CameraManager>().isPerChange) return;
            GameObject.Find("Main Camera").GetComponent<CameraManager>().PushChangeButton();
    }

    private void EventDanger()
    {
        if (!isEventDanger) return;
        redEventObject.GetComponent<StrongEnemyEvent>().StartCoroutine("WarningCoroutine");
        isEventDanger = false;
    }

    private void WinGame()
    {
        if (currentDay != END_WAVE) return;
        SceneManager.LoadScene("WinMovie"); //ゲームクリア
        isWin = true;
    }

    public void UpdateGauge()
    {
        currentGauge = maxGauge - deadCount;
    }
}
