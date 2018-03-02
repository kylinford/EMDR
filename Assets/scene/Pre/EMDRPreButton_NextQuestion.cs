using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMDRPreButton_NextQuestion : MonoBehaviour {
    public GameObject nextQuestion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Clicked()
    {
        nextQuestion.GetComponent<EMDRPrePanel_Question>().StartMoveFront();
    }
}
