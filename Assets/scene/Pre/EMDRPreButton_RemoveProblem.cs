using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreButton_RemoveProblem : MonoBehaviour {
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
        EMDRPreButton_Problem[] buttons = FindObjectsOfType<EMDRPreButton_Problem>();
        foreach (EMDRPreButton_Problem button in buttons)
            button.ShowDeleteButton();
    }
    void HideDeleteButton()
    {
        EMDRPreButton_Problem[] buttons = FindObjectsOfType<EMDRPreButton_Problem>();
        foreach (EMDRPreButton_Problem button in buttons)
            button.HideDeleteButton();
    }
}
