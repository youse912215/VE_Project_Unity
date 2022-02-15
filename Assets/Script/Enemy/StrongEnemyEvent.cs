using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StrongEnemyEvent : MonoBehaviour
{
    [SerializeField] private Text message; //���b�Z�[�W�e�L�X�g
    private Image image; //�i�[�p

    //�萔
    private const int WAR_TIMES = 2; //��
    private const float INTERVAL = 1.0f; //�Ԋu����

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Stop();
        image = GetComponent<Image>();
        this.image.enabled = false;
    }

    /// <summary>
    /// UI�̗L����Ԃ�ؑ�
    /// </summary>
    /// <param name="state"></param>
    private void ChangeEnabled(bool state)
    {
        this.image.enabled = state;
        message.enabled = state;
    }

    /// <summary>
    /// �x������R���[�`��
    /// </summary>
    public IEnumerator WarningCoroutine()
    {
        //������
        GetComponent<AudioSource>().Play();
        message.text = "���G�o��!";
        var state = true;

        //WAR_TIMES * 2�񕪉�
        for (int i = 0; i < WAR_TIMES * 2; ++i)
        {
            ChangeEnabled(state); //��Ԃ�ؑ�
            yield return new WaitForSeconds(INTERVAL); //�ҋ@����
            state = !state; //bool���]
        }

        //�I������
        message.text = "";
        GetComponent<AudioSource>().Stop();
    }
}
