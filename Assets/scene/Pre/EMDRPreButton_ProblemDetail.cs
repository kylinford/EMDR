using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreButton_ProblemDetail : MonoBehaviour {
    EMDRPreButton_ProblemDetailDelete deleteButton;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => FindObjectOfType<EMDRPre_Controller>().InitDone);
        deleteButton = GetComponentInChildren<EMDRPreButton_ProblemDetailDelete>();

        HideDeleteButton();
        UpdateColor();
    }
    
    public void Clicked()
    {
        //UpdateRoundProblem
        EMDRData ed = FindObjectOfType<EMDRData>();
        Round newRound = ed.LoadLocal_Round();
        newRound.problemdetail = GetComponentInChildren<Text>().text;
        ed.SaveLocal_Round(newRound);

        //Next question
        EMDRPrePanel_Question nextQuestion = GameObject.Find("EMDRPrePanel_Question (4)").GetComponent<EMDRPrePanel_Question>();
        nextQuestion.Clicked_SideButton();

        //Update Color
        EMDRPreButton_ProblemDetail[] buttons = FindObjectsOfType<EMDRPreButton_ProblemDetail>();
        foreach (EMDRPreButton_ProblemDetail button in buttons)
            button.UpdateColor();
    }

    void UpdateColor()
    {
        EMDRData ed = FindObjectOfType<EMDRData>();
        Round round = ed.LoadLocal_Round();

        if (round.problemdetail == GetComponentInChildren<Text>().text)
        {
            GetComponent<Image>().color = FindObjectOfType<EMDRPre_Controller>().colorButtonSelected;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }
    
    public void ShowDeleteButton()
    {
        GetComponent<Button>().interactable = false;
        deleteButton.gameObject.SetActive(true);
    }

    public void HideDeleteButton()
    {
        GetComponent<Button>().interactable = true;
        deleteButton.gameObject.SetActive(false);
    }
    
}
