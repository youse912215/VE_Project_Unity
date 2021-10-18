using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.TransformVirus;

public class ActVirus : MonoBehaviour
{
    Virus[,] virus = new Virus[CATEGORY, OWNED];

    public static bool isButtonActive;

    GameObject[,] virusObj2D = new GameObject[CATEGORY, OWNED];

    public static bool[] act = new bool[10000];

    Vector3 pos;

        Vector3 mousePos;
        Vector3 worldPos;
        Camera cam;
        public static int iNum; //識別番号

    // Start is called before the first frame update
    void Start()
    {
        InitValue(virus, CODE_CLD);
        InitValue(virus, CODE_INF);
        InitValue(virus, CODE_19);

        isButtonActive = false;
        iNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 4500.0f;

        cam = Camera.main;
        worldPos = cam.ScreenToWorldPoint(mousePos);

        if (virus[(int)CODE_CLD, owned[(int)CODE_CLD]].isActivity)
            virusObj2D[(int)CODE_CLD, owned[(int)CODE_CLD]].transform.position = worldPos;

        if (!isButtonActive) return;
        if (Input.GetMouseButtonDown(1))
        {
            SaveVirusPosition(virus, CODE_CLD, virusObj2D, worldPos);
            isButtonActive = ReverseFlag(isButtonActive);
            iNum++;
        }

        
    }

    public void ButtonPush()
    {
        if (ProcessOutOfRange(CODE_CLD)) return;
        

        isButtonActive = ReverseFlag(isButtonActive);

        if (isButtonActive)
        {
            GetVirus(CODE_CLD);
            //iNum = AllocationNumber(CODE_CLD);

            /* ウイルスが生成される */
            SetVirus(virus, CODE_CLD, virusObj2D);
            virusObj2D[(int)CODE_CLD, owned[(int)CODE_CLD]] = GameObject.Find("Virus3D"); //ウイルスオブジェクトを取得
            Instantiate(
                virusObj2D[(int)CODE_CLD, owned[(int)CODE_CLD]],
                virusObj2D[(int)CODE_CLD, owned[(int)CODE_CLD]].transform.position,
                Quaternion.identity); //ゲームオブジェクトを生成
            virusObj2D[(int)CODE_CLD, owned[(int)CODE_CLD]].SetActive(true); //アクティブ状態にする
            act[iNum] = true;
        }
        else 
        {
            Destroy(virusObj2D[(int)CODE_CLD, owned[(int)CODE_CLD]]); //ゲームオブジェクトを削除
            act[iNum] = false;
            ReleaseVirus(CODE_CLD);
        }
    }

    //flagを反転する
    private bool ReverseFlag(bool flag)
    {
        return !flag ? true : false;
    }
}
