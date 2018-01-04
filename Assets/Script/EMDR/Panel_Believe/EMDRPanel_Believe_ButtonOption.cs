using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMDR;

public class EMDRPanel_Believe_ButtonOption : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateInputText()
    {
        FindObjectOfType<EMDRPanel_Believe_Input>().GetComponent<InputField>().text = GetComponentInChildren<Text>().text;
    }

    public void DeleteCurrBelieveText()
    {
        Data data = new Data();
        data.Believes.Remove(GetComponentInChildren<Text>().text);
        data.Save();
        FindObjectOfType<EMDRPanel_Believe>().UpdateBelieveList();
    }
}
