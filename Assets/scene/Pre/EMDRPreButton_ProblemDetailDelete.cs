using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreButton_ProblemDetailDelete : MonoBehaviour {
    public void Clicked()
    {
        StartCoroutine(RemoveProblem());

        //Update UI
    }

    IEnumerator RemoveProblem()
    {
        //Delete current problem from server side
        string pdtoremove = transform.parent.Find("Text_ProblemDetail").GetComponent<Text>().text;
        EMDRData ed = FindObjectOfType<EMDRData>();

        //Update user
        User newUser = ed.User;
        newUser.RemoveProblemDetail(pdtoremove);
        yield return StartCoroutine(ed.Coroutine_UpdateUser(newUser));

        //Update round
        Round currRound = ed.LoadLocal_Round();
        if (currRound.problemdetail == pdtoremove)
        {
            currRound.problemdetail = "";
            ed.SaveLocal_Round(currRound);
        }

        //Update UI
        FindObjectOfType<EMDRPreScroll_ProblemDetail>().UpdateUI();
    }
}
