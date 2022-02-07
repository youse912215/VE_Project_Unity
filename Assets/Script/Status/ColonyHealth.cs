using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

using static Call.ConstantValue;

public class ColonyHealth : MonoBehaviour
{
    private float maxHp = 5000.0f; //�ő�HP
    private const int COLONY_LEVEL_MAX = 19;
    public static float currentHp; //���݂�HP
    public static int colonyLevel = 0; //�R���j�[���x��
    public static float exp = 0.0f; //���݂̌o���l

    private PlayableDirector playableDirector;
    [SerializeField] private GameObject time;
    [SerializeField] private GameObject levelUpTimeline;
    [SerializeField] private GameObject levelupText;

    [SerializeField] private Slider slider; //Slider�i�[
    [SerializeField] private ParticleSystem fire; //���p�[�e�B�N��
    private ParticleSystem fireEffect; //���G�t�F�N�g
    private bool isFire; //���G�t�F�N�g�t���O
    private bool isLevelUp = false;

    //���x�����Ƃ̌o���l���X�g�i�K�v�o���l�j
    public static readonly List<float> EXP_LIST = new List<float>{
        100, 250, 400, 600, 800,
        1100, 1450, 1800, 2300, 3000,
        4000, 5000, 6500, 8000, 10000,
        12500, 15000, 20000, 25000, 35000,
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
        slider.value = 1; //Slider�𖞃^��
        currentHp = maxHp; //���݂�HP�ɍő�HP����
        isFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        LevelUpdate(); //���x���X�V
        StartLevelEffect(); //���x���A�b�v�G�t�F�N�g�J�n

        /* Health�Ǘ� */
        slider.value = currentHp / maxHp; //HP�X���C�_�[���X�V

        if (currentHp > 1000.0f) return;
        GetFireEffect(); //���G�t�F�N�g���擾

        if (currentHp > 0.0f) return;
        SceneManager.LoadScene("LOSE"); //�Q�[���I�[�o�[
    }

    /// <summary>
    /// ���x���X�V
    /// </summary>
    private void LevelUpdate()
    {
        if (colonyLevel == 0) return; //���x��0�̂Ƃ��A�������X�L�b�v
        if (exp < EXP_LIST[colonyLevel - 1]) return; //�K�v�o���l�ɒB���Ă��Ȃ��Ƃ��A�������X�L�b�v
        if (colonyLevel == COLONY_LEVEL_MAX) return; //�ő僌�x���ɒB���Ă��邫�A�������X�L�b�v
        exp = 0.0f; //�o���l�����Z�b�g
        isLevelUp = true; //���x���A�b�v
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
