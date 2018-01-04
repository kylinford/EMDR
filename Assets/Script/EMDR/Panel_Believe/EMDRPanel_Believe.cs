using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EMDR;

public class EMDRPanel_Believe : JWMonoBehaviour {
    //EMDRData data;

	// Use this for initialization
	void Start () {
        //data = Resources.Load<EMDRData>("Prefab/Data");
        UpdateBelieveList();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateBelieveList()
    {
        StartCoroutine(UpdateBelieveListCoroutine());
    }

    IEnumerator UpdateBelieveListCoroutine()
    {
        EMDRPanel_Main panelMain = FindObjectOfType<EMDRPanel_Main>();
        EMDRLevel level = new EMDRLevel(panelMain.XmlTexts[PlayerPrefs.GetInt("CurrLevel")]);

        GameObject ContentBelieveSentence = transform.FindChild("Scroll View").FindChild("Viewport").FindChild("Content").gameObject;
        foreach (Transform child in ContentBelieveSentence.transform)
        {
            Destroy(child.gameObject);
        }
        transform.FindChild("InputField").FindChild("Text_Title").GetComponent<Text>().color = level.Property.TextColor;
        GameObject Button_BelieveText = Resources.Load<GameObject>("Prefab/Panel_Believe/Button_BelieveText");

        //believe text from local data
        Data data = new Data();
        for (int i=data.Believes.Count-1; i >= 0; i--)
        {
            GameObject newButton_BelieveText = JWInstantiateUnderParent_UI(Button_BelieveText, ContentBelieveSentence, false);
            newButton_BelieveText.GetComponentInChildren<Text>().text = data.Believes[i];
        }

        yield return new WaitForEndOfFrame();
        ContentBelieveSentence.GetComponent<JWUI>().AutoSize_Height();
    }
}
