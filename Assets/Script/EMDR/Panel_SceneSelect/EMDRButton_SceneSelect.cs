using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRButton_SceneSelect : MonoBehaviour {
    int levelIndex;
    EMDRLevel level;
    EMDRPanel_Main panelMain;

    public int LevelIndex { get { return levelIndex; } set { levelIndex = value; } }

    // Use this for initialization
    void Start () {
        panelMain = FindObjectOfType<EMDRPanel_Main>();
        UpdateContent();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateContent()
    {
        level = new EMDRLevel(panelMain.XmlTexts[levelIndex]);
        transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/" + level.Property.Bgsrc);
        transform.FindChild("Text").GetComponent<Text>().text = level.Property.Name;
    }

    public void LoadLevel()
    {
        panelMain.LoadLevel(levelIndex);
        Destroy(FindObjectOfType<EMDRPanel_SceneSelect>().gameObject);
    }
}
