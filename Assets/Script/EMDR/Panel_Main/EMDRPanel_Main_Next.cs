using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class EMDRPanel_Main_Next : MonoBehaviour {
    EMDRPanel_Main panelMain;

	// Use this for initialization
	void Start () {
        panelMain = FindObjectOfType<EMDRPanel_Main>();
        UpdateFinishedLevel();
        UpdateAvailability();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateFinishedLevel()
    {
        if (PlayerPrefs.GetInt("CurrLevel") > PlayerPrefs.GetInt("FinishedLevel", -1))
            PlayerPrefs.SetInt("FinishedLevel", PlayerPrefs.GetInt("CurrLevel"));
        if (FindObjectOfType<EMDRPanel_SceneSelect>())
            FindObjectOfType<EMDRPanel_SceneSelect>().UpdateContent();
    }

    public void UpdateAvailability()
    {
        EMDRLevel level = new EMDRLevel(panelMain.XmlTexts[PlayerPrefs.GetInt("CurrLevel")]);

        int index = PlayerPrefs.GetInt("CurrLevel"); 
        if (index + 1 < panelMain.XmlTexts.Length)
        {
            EMDRLevel nextLevel = new EMDRLevel(panelMain.XmlTexts[index + 1]);
            GetComponentInChildren<Text>().text = "Next: " + nextLevel.Property.Name;
        }
        else
        {
            GetComponentInChildren<Text>().text = "All levels finished";
            GetComponentInChildren<Text>().color = level.Property.TextColor;
            GetComponent<Button>().enabled = false;
            GetComponent<Image>().enabled = false;
        }
    }

    public void LoadNextLevel()
    {
        int index = PlayerPrefs.GetInt("CurrLevel");
        if (index + 1 >= panelMain.XmlTexts.Length)//All levels finished
            return;
        
        //Load Next level
        FindObjectOfType<EMDRPanel_Main>().LoadLevel(index + 1);
    }
}
