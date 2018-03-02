using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreScroll_Problems : MonoBehaviour {
    public GameObject button;
    public GameObject buttonRemove;
    public GameObject buttonAdd;

    int maxButtonCount = 16;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitUntil(() => FindObjectOfType<EMDRPre_Controller>().InitDone);
        UpdateUI();
    }

    public void UpdateUI()
    {
        StartCoroutine(UpdateUI_Coroutine());
    }


    IEnumerator UpdateUI_Coroutine()
    {
        //Instantiate problem buttons
        EMDRData ed = FindObjectOfType<EMDRData>();
        string[] problems = ed.User.GetProblems();
        GameObject contentPanel = transform.Find("Viewport").Find("Content").gameObject;

        foreach (Transform child in contentPanel.transform)
        {
            Destroy(child.gameObject);
        }
        yield return new WaitUntil(() => contentPanel.transform.childCount == 0);

        if (problems != null)
        {
            foreach (string problem in problems)
            {
                if (contentPanel.transform.childCount >= maxButtonCount - 1)
                    break;
                GameObject newButton = JWMonoBehaviour.JWNew_UI(button, contentPanel);
                newButton.GetComponentInChildren<Text>().text = problem;
            }
        }

        //Instanntiate add and remove buttons
        if (contentPanel.transform.childCount > 0)
            JWMonoBehaviour.JWNew_UI(buttonRemove, contentPanel);
        if (contentPanel.transform.childCount < maxButtonCount)
            JWMonoBehaviour.JWNew_UI(buttonAdd, contentPanel);
    }
}
