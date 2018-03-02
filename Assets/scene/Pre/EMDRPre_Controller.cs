using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class EMDRPre_Controller : MonoBehaviour {
    public GameObject data;
    public GameObject panelAlert;
    public Color colorButtonSelected = new Color(237.0f / 255.0f, 255.0f / 255.0f, 195.0f / 255.0f, 139.0f / 255.0f);

    EMDRData ed;
    //bool currQuestionAnswered = false; public bool CurrQuestionAnswered { get { return CurrQuestionAnswered(); }}
    bool initDone = false; public bool InitDone { get { return initDone; } }

    // Use this for initialization
    IEnumerator Start () {
        //Init ed;
        bool edInited = false;
        if (!FindObjectOfType<EMDRData>())
        {
            Instantiate(data);
        }
        ed = FindObjectOfType<EMDRData>();
        yield return StartCoroutine(ed.Init());
        edInited = true;
        
        //All init done
        initDone = edInited && true; //For later use (other inits)

        //local round
        if (File.Exists(Application.persistentDataPath + ed.Filename_Round)){
            //Continue from where the user left
            ed.LoadLocal_Round().Print();
        }
        else
        {
            //Start with a new round
            ed.SaveLocal_Round(new Round());
            ed.LoadLocal_Round().Print();
        }
    }

    public bool QuestionAnswered(int i)
    {
        EMDRData ed = FindObjectOfType<EMDRData>();
        Round round = ed.LoadLocal_Round();
        switch (i)
        {
            case -1:
                return true;
            case 0:
                return round.wanttoAchieve != "";
            case 1:
                return round.problem != "";
            case 2:
                return round.problemScale_Pre != -1;
            case 3:
                return round.problemdetail != "";
            case 4:
                return round.bodypart != "";
            case 5:
                return round.believe != "";
            case 6:
                return round.believeScale_Pre != -1;
        }
        print("wrong input: question id");
        return false;
    }
}
