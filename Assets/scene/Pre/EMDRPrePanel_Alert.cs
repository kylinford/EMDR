using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMDRPrePanel_Alert : MonoBehaviour {
    public GameObject Panel_NextQuestion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Clicked_Yes()
    {
        EMDRPrePanel_Question question = Panel_NextQuestion.GetComponent<EMDRPrePanel_Question>();
        question.StartMoveFront();
        Destroy(gameObject);
    }

    public void Clicked_No()
    {
        Destroy(gameObject);
    }

}
