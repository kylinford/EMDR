using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreButton_RemoveProblemDetail : MonoBehaviour {
    public void Clicked()
    {
        if (GetComponentInChildren<Text>().text == "-")
        {
            ShowDeleteButton();
            GetComponentInChildren<Text>().text = "done";
        }
        else if (GetComponentInChildren<Text>().text == "done")
        {
            HideDeleteButton();
            GetComponentInChildren<Text>().text = "-";
        }

    }

    void ShowDeleteButton()
    {
        EMDRPreButton_ProblemDetail[] buttons = FindObjectsOfType<EMDRPreButton_ProblemDetail>();
        foreach (EMDRPreButton_ProblemDetail button in buttons)
            button.ShowDeleteButton();
    }
    void HideDeleteButton()
    {
        EMDRPreButton_ProblemDetail[] buttons = FindObjectsOfType<EMDRPreButton_ProblemDetail>();
        foreach (EMDRPreButton_ProblemDetail button in buttons)
            button.HideDeleteButton();
    }
}
