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
    
    [SerializeField] private Slider slider; //Slider�i�[

    private void Start()
    {
        maxGauge = ENEMY_COUNTS_PER_WAVE[0];
        slider.value = 1; //Slider�𖞃^��
        currentGauge = maxGauge; //���݂�HP�ɍő�HP����
    }

    private void Update()
    {
        //Debug.Log("�c��E�F�[�u�Q�[�W::" + currentGauge + " ����::" + currentDay + " DAY::" + Scene.DAY);

        //�L�����o�X���[�h��TowerDefense�ȊO�Ȃ�A�������X�L�b�v
        if (canvasMode != CANVAS_MODE.TOWER_DEFENCE_MODE) return;

        if (Input.GetKeyDown(KeyCode.A)) Destroy(GameObject.Find("YellowDamage"));
        
        slider.value = (float)currentGauge / maxGauge; //�Q�[�W���X�V

        if(currentDay != Scene.DAY) return;
        if(slider.value > 0) return; 
        
        currentDay++; //����DAY�ֈڍs
        maxGauge = ENEMY_COUNTS_PER_WAVE[currentDay - 1]; //WAVE�Q�[�W�����Z�b�g
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
