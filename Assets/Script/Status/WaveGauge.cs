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
    
    [SerializeField] private Slider slider; //Slider�i�[

    private void Start()
    {
        maxGauge = WarriorData.ENEMY_COUNTS_PER_WAVE[currentDay];
        slider.value = 1; //Slider�𖞃^��
        currentGauge = maxGauge; //���݂�HP�ɍő�HP����
    }

    private void Update()
    {
        Debug.Log(currentGauge);

        //�L�����o�X���[�h��TowerDefense�ȊO�Ȃ�A�������X�L�b�v
        if (canvasMode != CANVAS_MODE.TOWER_DEFENCE_MODE) return;
        
        slider.value = (float)currentGauge / maxGauge; //�Q�[�W���X�V

        if(currentDay != Scene.DAY) return;

        if(slider.value > 0) return; 
        currentGauge = maxGauge; //WAVE�Q�[�W�����Z�b�g
        currentDay++; //����DAY�ֈڍs
    }

    public void UpdateGauge()
    {
        currentGauge = maxGauge - WarriorData.deadCount;
    }
}
