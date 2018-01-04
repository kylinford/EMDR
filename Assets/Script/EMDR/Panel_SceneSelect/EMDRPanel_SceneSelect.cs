using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class EMDRPanel_SceneSelect : JWMonoBehaviour {
    GameObject Button_SceneSelect;
    GameObject Content;

    // Use this for initialization
    void Start () {
        Button_SceneSelect = Resources.Load<GameObject>("Prefab/Panel_SceneSelect/Button_SceneSelect");
        Content = GetComponentInChildren<ScrollRect>().content.gameObject;
        UpdateContent();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateContent()
    {
        foreach (Transform child in Content.transform)
        {
            Destroy(child.gameObject);
        }

        EMDRPanel_Main panelMain = FindObjectOfType<EMDRPanel_Main>();
        int i = 0;
        foreach (TextAsset xmlText in panelMain.XmlTexts)
        {
            if (i > PlayerPrefs.GetInt("FinishedLevel", -1) + 1)
                break;
            GameObject newButton = JWInstantiateUnderParent_UI(Button_SceneSelect, Content, false);
            newButton.GetComponent<EMDRButton_SceneSelect>().LevelIndex = i;
            i++;
        }
        if (PlayerPrefs.GetInt("FinishedLevel", -1) < panelMain.XmlTexts.Length - 2)
        {
            Sprite iconNewLevel = Resources.Load<Sprite>("Art/question_mark");
            GameObject newButton = JWInstantiateUnderParent_UI(Button_SceneSelect, Content, false);
            newButton.GetComponent<EMDRButton_SceneSelect>().enabled = false;
            newButton.GetComponentInChildren<Text>().text = "(To be unlocked)";
            newButton.transform.FindChild("Image").GetComponent<Image>().sprite = iconNewLevel;
            newButton.transform.FindChild("Image").GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 0.8f);
            newButton.GetComponent<Button>().interactable = false;

        }

        Content.GetComponent<JWUI>().AutoSize_Height();
    }
}
