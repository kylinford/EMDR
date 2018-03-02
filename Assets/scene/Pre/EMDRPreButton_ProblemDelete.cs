using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreButton_ProblemDelete : MonoBehaviour {
    public void Clicked()
    {
        StartCoroutine(RemoveProblem());

        //Update UI
    }

    IEnumerator RemoveProblem()
    {
        //Delete current problem from server side
        string problemtoremove = transform.parent.Find("Text_Problem").GetComponent<Text>().text;
        EMDRData ed = FindObjectOfType<EMDRData>();

        //Update user
        User newUser = ed.User;
        newUser.RemoveProblem(problemtoremove);
        yield return StartCoroutine(ed.Coroutine_UpdateUser(newUser));

        //Update round
        Round currRound = ed.LoadLocal_Round();
        if (currRound.problem == problemtoremove)
        {
            currRound.problem = "";
            ed.SaveLocal_Round(currRound);
        }

        //Update UI
        FindObjectOfType<EMDRPreScroll_Problems>().UpdateUI();
        //Update later question text
        Round newRound = ed.LoadLocal_Round();
        string problem = newRound.problem == "" ? "your problem" : newRound.problem;
        Text text_ProblemScale = FindObjectOfType<EMDRPreText_ProblemScaleQuestion>().GetComponent<Text>();
        text_ProblemScale.text = "How much does " + problem + " bother you?";
        Text text_ProblemDetail = FindObjectOfType<EMDRPreText_ProblemDetailQuestion>().GetComponent<Text>();
        text_ProblemDetail.text = "What do you feel about " + problem + "?";

    }
}
