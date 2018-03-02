using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreButton_AddProblem : MonoBehaviour {
    public GameObject Panel_NewProblem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
        //Instantiate add new problem ui
        if (FindObjectOfType<EMDRPrePanel_AddProblem>())
            return;
        GameObject panel_Question = transform.GetComponentInParent<EMDRPrePanel_Question>().gameObject;
        JWMonoBehaviour.JWNew_UI(Panel_NewProblem, panel_Question);
    }
}
