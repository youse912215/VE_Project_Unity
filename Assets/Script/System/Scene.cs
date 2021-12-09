using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static WarriorData;

public class Scene : MonoBehaviour
{
    public static int sceneName; //�V�[���ԍ�

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, false); //��ʃT�C�Y
        Application.targetFrameRate = 60; //FPS�Œ�
    }

    // Update is called once per frame
    void Update()
    {
        sceneName = SceneManager.GetActiveScene().buildIndex; //���݂̃V�[���ԍ���ۑ�

        if (deadCount <= 10) return;
        SceneManager.LoadScene("End");
    }

    public void startgame()
    {
        SceneManager.LoadScene("TowerDefence");
    }
}
