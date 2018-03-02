using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EMDRPrePanel_Question : MonoBehaviour {
    EMDRPre_Controller controller;

    IEnumerator Start()
    {
        controller = FindObjectOfType<EMDRPre_Controller>();
        yield return new WaitUntil(()=>controller.InitDone);
    }

    int GetIndex()
    {
        int ret = -1;
        char index_Char = transform.name[transform.name.Length - 2];
        Int32.TryParse(index_Char.ToString(), out ret);
        return ret;
    }


    string GetPosition()
    {
        if (transform.position.x < -1)
            return "left";
        else if (transform.position.x > 1)
            return "right";
        return "middle";
    }

    public void Clicked_SideButton() {
        if (GetPosition() == "right")
        {
            if (!controller.QuestionAnswered(GetIndex() - 1))
            {
                //Alert
                GameObject newAlert = JWMonoBehaviour.JWNew_UI(controller.panelAlert, FindObjectOfType<Canvas>().gameObject);
                newAlert.GetComponent<EMDRPrePanel_Alert>().Panel_NextQuestion = gameObject;
                return;
            }
        }

        //Move
        StartCoroutine(MoveFront());
    }

    public void StartMoveFront()
    {
        StartCoroutine(MoveFront());
    }

    IEnumerator MoveFront()
    {
        //Move
        float timer = 0.3f;
        Vector3 dest = transform.parent.position - transform.position;
        float speed = (transform.parent.position - dest).magnitude / timer;
        while (timer > 0)
        {
            yield return new WaitForFixedUpdate();
            transform.parent.position = Vector3.MoveTowards
                (transform.parent.position, dest, speed * Time.deltaTime);
            timer -= Time.deltaTime;
        }

        //Set other pages' onScreen to false
        EMDRPrePanel_Question[] questions = FindObjectsOfType<EMDRPrePanel_Question>();
            
    }
}
