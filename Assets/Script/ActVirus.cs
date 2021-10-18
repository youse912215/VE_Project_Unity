using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.TransformVirus;

public class ActVirus : MonoBehaviour
{
    private Virus[,] virus = new Virus[CATEGORY, OWNED];
    private GameObject[,] virusObj = new GameObject[CATEGORY, OWNED]; 
    public GameObject prefab;
    private bool isButtonActive;

    private Vector3 mousePos; //マウス座標
    private Vector3 worldPos; //ワールド座標
    private Camera cam; //カメラオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        InitValue(virus, CODE_CLD);
        InitValue(virus, CODE_INF);
        InitValue(virus, CODE_19);

        isButtonActive = false;
        cam = Camera.main;
    }

    // Pushing any buttons
    private void ButtonPush()
    {
        if (ProcessOutOfRange(CODE_CLD)) return;
        isButtonActive = ReverseFlag(isButtonActive);

        if (isButtonActive)
        {         
            /* ウイルスが生成される */
            GenerationVirus(virus, CODE_CLD, virusObj); //ウイルス生成
            virusObj[(int)CODE_CLD, vNum[(int)CODE_CLD]] = Instantiate(prefab); //ゲームオブジェクトを生成
            virusObj[(int)CODE_CLD, vNum[(int)CODE_CLD]].SetActive(true); //アクティブ状態にする 
        }
        else 
        {
            /* ウイルスを削除する */
            virus[(int)CODE_CLD, vNum[(int)CODE_CLD]].isActivity = false; //生存状態をfalse
            Destroy(virusObj[(int)CODE_CLD, vNum[(int)CODE_CLD]]); //ゲームオブジェクトを削除
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveVirus(); //ウイルスを移動

        if (virus[(int)CODE_CLD, vNum[(int)CODE_CLD]].isActivity)
            virusObj[(int)CODE_CLD, vNum[(int)CODE_CLD]].transform.position = worldPos;

        if (!isButtonActive) return;
        SetVirus(); //ウイルスを設置
    }

    /// <summary>
    /// flagを反転する
    /// </summary>
    private bool ReverseFlag(bool flag)
    {
        return !flag ? true : false;
    }

    /// <summary>
    /// ウイルスを設置
    /// </summary>
    private void SetVirus()
    {
        //クリック
        if (Input.GetMouseButtonDown(1))
        {
            SaveVirusPosition(virus, CODE_CLD, virusObj, worldPos);
            GetVirus(CODE_CLD);
            isButtonActive = false;
        }
    }

    /// <summary>
    /// ウイルスを移動
    /// </summary>
    private void MoveVirus()
    {
        mousePos = Input.mousePosition;
        mousePos.z = CAM_DISTANCE;  
        worldPos = cam.ScreenToWorldPoint(mousePos);
    }
}
