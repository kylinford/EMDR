using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMDRPreButton_AddProblemDetail : MonoBehaviour {
    public GameObject Panel_NewProblemDetail;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Clicked()
    {
        //Instantiate add new problem ui
        if (FindObjectOfType<EMDRPrePanel_AddProblemDetail>())
            return;
        GameObject panel_Question = transform.GetComponentInParent<EMDRPrePanel_Question>().gameObject;
        JWMonoBehaviour.JWNew_UI(Panel_NewProblemDetail, panel_Question);
    }
}
