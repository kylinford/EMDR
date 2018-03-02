using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EMDRPrePanel_AddProblemDetail : MonoBehaviour {
    EMDRData ed;
    Transform buttonAdd;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitUntil(() => FindObjectOfType<EMDRPre_Controller>().InitDone);
        ed = FindObjectOfType<EMDRData>();
        buttonAdd = transform.Find("Button_Add");
    }

    public void Clicked_Add()
    {
        StartCoroutine(Clicked_Add_Corountine());
    }


    IEnumerator Clicked_Add_Corountine()
    {
        string newProblem = GetComponentInChildren<InputField>().text;
        User updatedUser = ed.User;
        updatedUser.AddProblemDetail(newProblem);
        yield return StartCoroutine(ed.Coroutine_UpdateUser(updatedUser));
        FindObjectOfType<EMDRPreScroll_ProblemDetail>().UpdateUI();
        Destroy(gameObject);

    }
    public void Clicked_Cancel()
    {
        Destroy(gameObject);
    }

    public void UpdateAddButton()
    {
        //Check if newProblem exists in problems
        string newProblemDetail = GetComponentInChildren<InputField>().text;
        string[] currProblemDetails = ed.User.GetProblemDetails();

        if (currProblemDetails.Contains(newProblemDetail))
        {
            buttonAdd.GetComponentInChildren<Text>().text = newProblemDetail + " Exits";
            buttonAdd.GetComponent<Button>().interactable = false;
        }
        else
        {
            buttonAdd.GetComponentInChildren<Text>().text = "Add";
            buttonAdd.GetComponent<Button>().interactable = true;
        }
    }
}
