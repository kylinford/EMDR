using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMDR;

public class EMDRPanel_Believe_Input : MonoBehaviour {
    Text BelieveText;
    GameObject Button_Save;

	// Use this for initialization
	void Start () {
        BelieveText = FindObjectOfType<EMDRPanel_Main>().transform.FindChild("Button_Goal").GetComponentInChildren<Text>();
        Button_Save = transform.FindChild("Button_Save").gameObject;
        SetSaveButtonAvailability();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSaveButtonAvailability()
    {
        Data data = new Data();
        if (data.Believes.Contains(GetComponent<InputField>().text) || GetComponent<InputField>().text == "")
            Button_Save.SetActive(false);
        else
            Button_Save.SetActive(true);
    }

    public void UpdateBelieveText()
    {
        if (GetComponent<InputField>().text == " ")
            GetComponent<InputField>().text = "";

        PlayerPrefs.SetString("BelieveText", GetComponent<InputField>().text);
        BelieveText.text = PlayerPrefs.GetString("BelieveText");

        if (BelieveText.text == "")
            BelieveText.text = FindObjectOfType<EMDRPanel_Main>().DefaultBelieveText;

    }

    public void SaveBelieveText()
    {
        Data data = new Data();

        string newBelieve = PlayerPrefs.GetString("BelieveText");
        EMDRSettings settings = new EMDRSettings();
        List<string> BelievesFromSettings = new List<string>();
        foreach (EMDRSettings_Believe believe in settings.Believes.Content)
            BelievesFromSettings.Add(believe.Text);

        if (!data.Believes.Contains(newBelieve)
            && !BelievesFromSettings.Contains(newBelieve))
            data.Believes.Add(PlayerPrefs.GetString("BelieveText"));
        data.Save();

        //Destroy(FindObjectOfType<EMDRPanel_Believe>().gameObject);
        FindObjectOfType<EMDRPanel_Believe>().UpdateBelieveList();
        
    }
}
