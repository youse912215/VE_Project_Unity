using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static WarriorData;
using static Call.ConstantValue;

public class Scene : MonoBehaviour
{
    public static int DAY; //0����

    [SerializeField] private AudioSource clickAudio;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution((int)WINDOW_SIZE.x, (int)WINDOW_SIZE.y, true); //��ʃT�C�Y
        Application.targetFrameRate = 60; //FPS�Œ�

        DAY = 0;
        deadCount = 0;
    }

    public void startgame()
    {
        clickAudio.PlayOneShot(clickAudio.clip);
        SceneManager.LoadScene("TowerDefence");
    }
}
