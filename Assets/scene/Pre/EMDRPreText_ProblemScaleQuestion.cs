using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreText_ProblemScaleQuestion : MonoBehaviour {

    EMDRPre_Controller controller;
    EMDRData ed;

    // Use this for initialization
    IEnumerator Start()
    {
        controller = FindObjectOfType<EMDRPre_Controller>();
        yield return new WaitUntil(() => controller.InitDone);
        ed = FindObjectOfType<EMDRData>();

        string roundProblem = ed.LoadLocal_Round().problem;
        string problem = roundProblem == "" ? "your problem" : roundProblem;
        GetComponent<Text>().text =  "How much does " + problem + " bother you?";

    }
}
