using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreButton_Believe : MonoBehaviour {

    IEnumerator Start()
    {
        yield return new WaitUntil(() => FindObjectOfType<EMDRPre_Controller>().InitDone);
        UpdateColor();
    }
    

    public void Clicked()
    {
        //Update local round record
        EMDRData ed = FindObjectOfType<EMDRData>();
        Round newRound = ed.LoadLocal_Round();
        newRound.believe = GetComponentInChildren<Text>().text;
        ed.SaveLocal_Round(newRound);

        //Next Question
        EMDRPrePanel_Question nextQuestion = GameObject.Find("EMDRPrePanel_Question (6)").GetComponent<EMDRPrePanel_Question>();
        nextQuestion.Clicked_SideButton();

        //Update all buttons' color
        EMDRPreButton_Believe[] buttons = FindObjectsOfType<EMDRPreButton_Believe>();
        foreach (EMDRPreButton_Believe button in buttons)
            button.UpdateColor();
    }

    void UpdateColor()
    {
        EMDRData ed = FindObjectOfType<EMDRData>();
        Round round = ed.LoadLocal_Round();

        if (round.believe == GetComponentInChildren<Text>().text)
            GetComponent<Image>().color = FindObjectOfType<EMDRPre_Controller>().colorButtonSelected;
        else
            GetComponent<Image>().color = Color.white;
    }
}
