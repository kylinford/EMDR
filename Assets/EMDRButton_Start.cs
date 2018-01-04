using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRButton_Start : JWMonoBehaviour {
    EMDRPanel_Main panelMain;

    // Use this for initialization
    void Start () {
        panelMain = FindObjectOfType<EMDRPanel_Main>();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponentInChildren<Text>().text = panelMain.Timer + "s";
    }

    public void AddTimer(int seconds)
    {
        panelMain.Timer += seconds;
        panelMain.Timer = panelMain.Timer >= 10 ? panelMain.Timer : 10;
        panelMain.Timer = panelMain.Timer <= 100 ? panelMain.Timer : 100;
    }

    public void StartGame()
    {
        EMDRLevel level = new EMDRLevel(panelMain.XmlTexts[PlayerPrefs.GetInt("CurrLevel")]);
        panelMain.StartCoroutinePointsMove();
        GameObject textInstruction = JWInstantiateUnderParent_UI(Resources.Load<GameObject>("Prefab/Panel_Main/Text_Instruction"), transform.parent.gameObject, false);
        textInstruction.transform.position = transform.position;
        textInstruction.GetComponent<Text>().color = level.Property.TextColor * 0.7f;
        Destroy(gameObject);
    }
}
