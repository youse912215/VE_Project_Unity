using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

using static Call.ConstantValue;

public class ColonyHealth : MonoBehaviour
{
    public static float maxHp = 5000.0f; //�ő�HP
    public static float currentHp; //���݂�HP
    public static int colonyLevel = 0; //�R���j�[���x��
    public static float exp = 0.0f; //���݂̌o���l
    private const float DANGER_HEALTH = 1000.0f; //�댯��Ԏ���HP

    private PlayableDirector playableDirector;
    [SerializeField] private GameObject time;
    [SerializeField] private GameObject levelUpTimeline;
    [SerializeField] private GameObject levelupText;
    [SerializeField] private GameObject levelTextObjcet;

    [SerializeField] private Slider hpSlider; //Slider�i�[
    [SerializeField] private Slider expSlider; //Slider�i�[
    [SerializeField] private ParticleSystem fire; //���p�[�e�B�N��
    private ParticleSystem fireEffect; //���G�t�F�N�g
    private Text levelText;
    private bool isFire; //���G�t�F�N�g�t���O
    public static bool isLevelUp = false;

    [SerializeField] private GameObject redd;

    //���x�����Ƃ̌o���l���X�g�i�K�v�o���l�j
    public static readonly List<float> EXP_LIST = new List<float>{
        100, 550, 1000, 2500, 4000,
        6500, 9000, 12000, 15000, 18000,
        22000, 27000, 33000, 40000, 50000,
        60000, 70000, 80000, 90000, 100000,
        };

    //���x�����Ƃ̗͂̏d��
    public static readonly List<float> FORCE_WEIGHT = new List<float>
    {
        1.05f, 1.10f, 1.20f, 1.30f, 1.40f,
        1.55f, 1.70f, 1.85f, 2.00f, 2.20f,
        2.40f, 2.60f, 2.80f, 3.00f, 3.20f,
        3.40f, 3.60f, 3.90f, 4.40f, 5.00f,
    };

    private void Awake()
    {
        playableDirector = time.GetComponent<PlayableDirector>();
        levelUpTimeline.SetActive(false);
        levelupText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        hpSlider.value = 1; //Slider�𖞃^��
        currentHp = maxHp; //���݂�HP�ɍő�HP����
        isFire = false;
        levelText = levelTextObjcet.GetComponent<Text>();
    }

    

    // Update is called once per frame
    void Update()
    {
        Debug.Log("HP::" + currentHp + " / " + maxHp);

        //�L�����o�X���[�h��TowerDefense�ȊO�̂Ƃ��A�������X�L�b�v
        if (CanvasManager.canvasMode != CanvasManager.CANVAS_MODE.TOWER_DEFENCE_MODE) return;

        if (Input.GetKeyDown(KeyCode.A))
            redd.GetComponent<StrongEnemyEvent>().StartCoroutine("WarningCoroutine");

        levelText.text = "Colony Lv " + (colonyLevel + 1).ToString(); //���x���\��
        StartLevelEffect(); //���x���A�b�v�G�t�F�N�g�J�n

        /* Health�Ǘ� */
        hpSlider.value = currentHp / maxHp; //HP�X���C�_�[���X�V
        expSlider.value = exp / EXP_LIST[colonyLevel];

        if (currentHp > DANGER_HEALTH) return;
        GetFireEffect(); //���G�t�F�N�g���擾

        if (currentHp > 0.0f) return;
        SceneManager.LoadScene("LoseMovie"); //�Q�[���I�[�o�[
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

    private void StartLevelEffect()
    {
        if (!isLevelUp) return;
        levelUpTimeline.SetActive(true);
        levelupText.SetActive(true);

        playableDirector.Play();
        isLevelUp = false; //�t���O��߂�
    }
}
