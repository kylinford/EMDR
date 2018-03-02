using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EMDRPreButton_Problem : MonoBehaviour {
    EMDRPreButton_ProblemDelete deleteButton;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => FindObjectOfType<EMDRPre_Controller>().InitDone);
        deleteButton = GetComponentInChildren<EMDRPreButton_ProblemDelete>();

        HideDeleteButton();
        UpdateColor();
    }

    public void Clicked()
    {
        //UpdateRoundProblem
        EMDRData ed = FindObjectOfType<EMDRData>();
        Round newRound = ed.LoadLocal_Round();
        newRound.problem = GetComponentInChildren<Text>().text;
        ed.SaveLocal_Round(newRound);

        //Update later question text
        Text text_ProblemScale = FindObjectOfType<EMDRPreText_ProblemScaleQuestion>().GetComponent<Text>();
        text_ProblemScale.text = "How much does " + newRound.problem + " bother you?";
        Text text_ProblemDetail = FindObjectOfType<EMDRPreText_ProblemDetailQuestion>().GetComponent<Text>();
        text_ProblemDetail.text = "What do you feel about " + newRound.problem + "?"; 

        //Next question
        EMDRPrePanel_Question nextQuestion = GameObject.Find("EMDRPrePanel_Question (2)").GetComponent<EMDRPrePanel_Question>();
        nextQuestion.Clicked_SideButton();

        //Update Color
        EMDRPreButton_Problem[] buttons = FindObjectsOfType<EMDRPreButton_Problem>();
        foreach (EMDRPreButton_Problem button in buttons)
            button.UpdateColor();
    }

    void UpdateColor()
    {
        EMDRData ed = FindObjectOfType<EMDRData>();
        Round round = ed.LoadLocal_Round();

        if (round.problem == GetComponentInChildren<Text>().text)
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
