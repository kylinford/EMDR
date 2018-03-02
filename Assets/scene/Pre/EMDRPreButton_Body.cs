using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreButton_Body : MonoBehaviour {
    Image Image_Body;
    EMDRData ed;

    // Use this for initialization
    IEnumerator Start () {
        yield return new WaitUntil(() => FindObjectOfType<EMDRPre_Controller>().InitDone);
        ed = FindObjectOfType<EMDRData>();
        Image_Body = GameObject.Find("Image_Body").GetComponent<Image>();
        UpdateUI();
    }

    public void Clicked()
    {
        //Update Round locally
        Round newRound = ed.LoadLocal_Round();
        newRound.bodypart = GetComponentInChildren<Text>().text;
        ed.SaveLocal_Round(newRound);

        //Update all UI
        UpdateUI_All();
    }

    void UpdateUI_All()
    {

        //Update all color
        EMDRPreButton_Body[] buttons = FindObjectsOfType<EMDRPreButton_Body>();
        foreach (EMDRPreButton_Body button in buttons)
        {
            button.UpdateUI();
        }

    }

    void UpdateUI()
    {
        Round newRound = ed.LoadLocal_Round();
        if (newRound.bodypart == GetComponentInChildren<Text>().text)
        {
            GetComponent<Image>().color = FindObjectOfType<EMDRPre_Controller>().colorButtonSelected;
            Image_Body.sprite = Resources.Load<Sprite>("body/" + newRound.bodypart);
        }
        else
            GetComponent<Image>().color = Color.white;
    }
}
