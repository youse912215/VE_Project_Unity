using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Call.VirusData;
using static Call.VirusData.VIRUS_NUM;
using static Call.TransformVirus;

public class ActVirus : MonoBehaviour
{
    Virus[,] virus = new Virus[CATEGORY, OWNED];

    public static bool isActive;

    GameObject[] virusObj2D;

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

        isActive = false;
        iNum = 0;
    }

    // Update is called once per frame
    void Update()
    {

        SetVirusActive();

        mousePos = Input.mousePosition;
        mousePos.z = 4500.0f;

        cam = Camera.main;
        worldPos = cam.ScreenToWorldPoint(mousePos);

        virusObj2D[iNum].transform.position = worldPos;

        if (!isActive) return;
        if (Input.GetMouseButtonDown(1))
        {

            virusObj2D[iNum] = GameObject.Find("Virus3D"); //ウイルスオブジェクトを取得
            virusObj2D[iNum].SetActive(true);
            SetVirus(virus, CODE_CLD, virusObj2D[iNum]);
            Instantiate(virusObj2D[iNum], virusObj2D[iNum].transform.position, Quaternion.identity);
            act[iNum] = true;
            isActive = ReverseFlag(isActive);
            iNum++;
        }

        
    }

    public void ButtonPush()
    {
        if (ProcessOutOfRange(CODE_CLD)) return;
        

        isActive = ReverseFlag(isActive); 
        if (isActive)
        {
            GetVirus(CODE_CLD);
            //iNum = AllocationNumber(CODE_CLD);
        }
        else 
        {
            ReleaseVirus(CODE_CLD);
        }
    }

    //flagを反転する
    private bool ReverseFlag(bool flag)
    {
        return !flag ? true : false;
    }

    private void SetVirusActive()
    {
       
            
    }
}
