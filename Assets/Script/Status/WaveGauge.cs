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
    
    [SerializeField] private Slider slider; //Slider�i�[

    private void Start()
    {
        slider.value = 1; //Slider�𖞃^��
        currentGauge = maxGauge; //���݂�HP�ɍő�HP����
    }

    private void Update()
    {
        //Debug.Log(currentDay);

        //�L�����o�X���[�h��TowerDefense�ȊO�Ȃ�A�������X�L�b�v
        if (canvasMode != CANVAS_MODE.TOWER_DEFENCE_MODE) return;
        slider.value = currentGauge / maxGauge; //�Q�[�W���X�V

        if(currentDay != Scene.DAY) return;
        currentGauge -= DECREASE_AMOUNT; //���݂̃Q�[�W������

        if(slider.value > 0) return; 
        currentGauge = maxGauge; //WAVE�Q�[�W�����Z�b�g
        currentDay++; //����DAY�ֈڍs
    }
}
