using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static Call.ConstantValue;

public class ColonyHealth : MonoBehaviour
{
    private float maxHp = 5000.0f; //�ő�HP
    public static float currentHp; //���݂�HP
    
    [SerializeField] private Slider slider; //Slider������
    [SerializeField] private ParticleSystem fire; //���p�[�e�B�N��
    private ParticleSystem fireEffect; //���G�t�F�N�g
    private bool isFire; //���G�t�F�N�g�t���O

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1; //Slider�𖞃^��
        currentHp = maxHp; //���݂�HP�ɍő�HP����
        isFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = currentHp / maxHp;

        if (currentHp > 1000.0f) return;
        GetFireEffect(); //���G�t�F�N�g���擾

        if (currentHp > 0.0f) return;
            SceneManager.LoadScene("LOSE"); //�Q�[���I�[�o�[
    }

    /// <summary>
    /// ���G�t�F�N�g���擾
    /// </summary>
    private void GetFireEffect()
    {
        if (isFire) return;
        fireEffect = Instantiate(fire);
        fireEffect.transform.position = FIRE_POS;
        fireEffect.transform.rotation = Quaternion.Euler(FIRE_ROT);
        fireEffect.Play(); //��
        isFire = true;
    }
}
