using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static WarriorData;
using static Call.ConstantValue;

public class Scene : MonoBehaviour
{
    public static int sceneName; //シーン番号

    public static int DAY = 1; //1日目
    private const int WIN_COUNT = 10000;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution((int)WINDOW_SIZE.x, (int)WINDOW_SIZE.y, false); //画面サイズ
        Application.targetFrameRate = 60; //FPS固定
    }

    // Update is called once per frame
    void Update()
    {
        sceneName = SceneManager.GetActiveScene().buildIndex; //現在のシーン番号を保存

        if (deadCount <= WIN_COUNT) return;
        SceneManager.LoadScene("End");
    }

    public void startgame()
    {
        SceneManager.LoadScene("TowerDefence");
    }
}
