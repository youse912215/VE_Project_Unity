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

    [SerializeField] private Slider slider; //Slider�i�[
    [SerializeField] private GameObject redEventObject;
    [SerializeField] private AudioSource seNext;
    [SerializeField] public AudioSource seBlood;
    
    private void Start()
    {
        currentDay = 1;
        maxGauge = ENEMY_COUNTS_PER_WAVE[0];
        slider.value = 1; //Slider�𖞃^��
        currentGauge = maxGauge; //���݂�HP�ɍő�HP����
        isEventDanger = false;
        isWin = false;
    }

    private void Update()
    {
        Debug.Log("�c��E�F�[�u�Q�[�W::" + currentGauge + " ����::" + currentDay + " DAY::" + Scene.DAY);

        //�L�����o�X���[�h��TowerDefense�ȊO�Ȃ�A�������X�L�b�v
        if (canvasMode != CANVAS_MODE.TOWER_DEFENCE_MODE) return;

        EventDanger();
        
        slider.value = (float)currentGauge / maxGauge; //�Q�[�W���X�V

        if(currentDay != Scene.DAY) return;
        if(slider.value > 0) return; 
        
        WinGame(); //�N���A����
        seNext.PlayOneShot(seNext.clip);

        if (isWin) return;
        
        currentDay++; //����DAY�ֈڍs
        maxGauge = ENEMY_COUNTS_PER_WAVE[currentDay - 1]; //WAVE�Q�[�W�����Z�b�g
        currentGauge = maxGauge;
        deadCount = 0;
        
        this.GetComponent<ActEnemy>().ResetSurvivalCount();
        this.GetComponent<CanvasManager>().PushSuppliesScreenButton();
        this.GetComponent<CanvasManager>().LoadCanvasEnabled(true);
        this.GetComponent<CanvasManager>().BackGroundEnabled(true);
        this.GetComponent<LoadingManager>().isLoading = true; //���[�h�J�n
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
        SceneManager.LoadScene("WinMovie"); //�Q�[���N���A
        isWin = true;
    }

    public void UpdateGauge()
    {
        currentGauge = maxGauge - deadCount;
    }
}
