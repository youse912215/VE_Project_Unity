using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.CommonFunction;

public class ActVirus : MonoBehaviour
{
    private Virus[,] virus = new Virus[CATEGORY, OWNED];
    private GameObject[,] virusObj = new GameObject[CATEGORY, OWNED]; 
    private GameObject prefab;
    private bool isButtonActive;

    private Vector3 mousePos; //マウス座標
    private Vector3 worldPos; //ワールド座標
    private Camera cam; //カメラオブジェクト

    private int buttonMode;
    private int oldMode;

    // Start is called before the first frame update
    void Start()
    {
        InitValue(virus, CODE_CLD);
        InitValue(virus, CODE_INF);
        InitValue(virus, CODE_19);
        
        isButtonActive = false;
        cam = Camera.main;

        buttonMode = 0;
        oldMode = buttonMode;
    }

    // Pushing any buttons
    public void ButtonPush(int n)
    {
        if (isButtonActive && buttonMode != n) return;

        if (ProcessOutOfRange(n)) return;
        isButtonActive = ReverseFlag(isButtonActive);

        if (isButtonActive)
        {         
            /* ウイルスが生成される */
            GenerationVirus(virus, (VIRUS_NUM)n); //ウイルス生成
            prefab = GameObject.Find(VirusName + n.ToString()); //ウイルス番号に合致するPrefabを取得
            virusObj[n, vNum[n]] = Instantiate(prefab); //ゲームオブジェクトを生成
            virusObj[n, vNum[n]].SetActive(true); //アクティブ状態にする
            buttonMode = n;
        }
        else 
        {
            /* ウイルスを削除する */
            virus[n, vNum[n]].isActivity = false; //生存状態をfalse
            Destroy(virusObj[n, vNum[n]]); //ゲームオブジェクトを削除
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveVirus(); //ウイルスを移動

        //ウイルスオブジェクトの位置をワールド座標で更新する
        if (virus[buttonMode, vNum[buttonMode]].isActivity)
            virusObj[buttonMode, vNum[buttonMode]].transform.position = worldPos;

        if (!isButtonActive) return;
        SetVirus(); //ウイルスを設置
    }

    

    /// <summary>
    /// ウイルスを設置
    /// </summary>
    private void SetVirus()
    {
        //クリック操作
        if (Input.GetMouseButtonDown(1))
        {
            SaveVirusPosition(virus, (VIRUS_NUM)buttonMode, virusObj, worldPos); //設置したウイルス座標を保存
            GetVirus((VIRUS_NUM)buttonMode); //ウイルスをアクティブにする
            isButtonActive = false; //ボタン操作をfalse
        }
    }

    /// <summary>
    /// ウイルスを移動
    /// </summary>
    private void MoveVirus()
    {
        mousePos = Input.mousePosition; //マウスの座標を取得
        mousePos.z = CAM_DISTANCE; //マウスz座標にカメラとの距離を代入
        worldPos = cam.ScreenToWorldPoint(mousePos); //スクリーン→ワールド変換
    }
}
