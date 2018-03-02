using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreButton_WanttoAchieve : MonoBehaviour {
    //public EMDRPrePanel_Question nextQuestion;
    //EMDRData ed;

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitUntil(() => FindObjectOfType<EMDRPre_Controller>().InitDone);
        UpdateColor();
    }

    public void Clicked()
    {
        //Update local round record
        EMDRData ed = FindObjectOfType<EMDRData>();
        Round newRound = ed.LoadLocal_Round();
        newRound.wanttoAchieve = GetComponentInChildren<Text>().text;
        ed.SaveLocal_Round(newRound);
        EMDRPrePanel_Question nextQuestion = GameObject.Find("EMDRPrePanel_Question (1)").GetComponent<EMDRPrePanel_Question>();
        nextQuestion.Clicked_SideButton();
        //Update buttons' color

        EMDRPreButton_WanttoAchieve[] buttons = FindObjectsOfType<EMDRPreButton_WanttoAchieve>();
        foreach (EMDRPreButton_WanttoAchieve button in buttons)
            button.UpdateColor();
    }

    void UpdateColor()
    {
        EMDRData ed = FindObjectOfType<EMDRData>();
        Round round = ed.LoadLocal_Round();

        if (round.wanttoAchieve == GetComponentInChildren<Text>().text)
        {
            GetComponent<Image>().color = FindObjectOfType<EMDRPre_Controller>().colorButtonSelected;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }
}
