using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{ 
    [SerializeField] private Image loadImage; //���[�h�摜
    [SerializeField] private Text hintText; //�q���g�e�L�X�g

    private readonly Color ADD_LOAD_COLOR = new Color(0.001f, 0.001f, 0.001f, 0.0f); //�ǂݍ��ݎ��̉��Z�F
    private const float ADD_COUNT = 0.005f; //���Z�J�E���g
    private const float LOAD_COUNT = 5.0f; //���[�h����
    private float count; //�J�E���g��
    private List<string> comma = new List<string>{" ", " .", " ..", " ...", " ....", " ....."}; //���[�h���̓_

    public bool isLoading; //���[�h�t���O

    private void Start()
    {
        count = 0.0f;
        isLoading = true;
    }

    // Start is called before the first frame update
    private void Update()
    {
        hintText.text = "Day " + (Scene.DAY + 1).ToString() + comma[(int)count];

        if(isLoading && this.GetComponent<SuppliesVirus>().endCoroutine)
            StartCoroutine(CountLoadTime()); //���[�h�J�n

        if (count < LOAD_COUNT) return; //���[�h���Ԃɖ����Ȃ��Ƃ��A�������X�L�b�v
        this.GetComponent<CanvasManager>().LoadCanvasEnabled(false); //�L�����o�X��off
        StopCoroutine(CountLoadTime()); //���[�h�̃R���[�`�����~
        count = 0.0f; //�J�E���g��������
        isLoading = false; //���[�h�t���O��false
        this.GetComponent<SuppliesVirus>().endCoroutine = false; //�R���[�`���t���O��false       
    }

    /// <summary>
    /// ���[�h���Ԃ��J�E���g
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountLoadTime()
    {
        //���[�h�t���O��true�̊ԁA�J��Ԃ�
        while (isLoading)
        {
            yield return new WaitForSeconds(0.1f);
            loadImage.color += ADD_LOAD_COLOR; //RGB�l�����Z
            count += ADD_COUNT; //�J�E���g
        }
        
    }
}
