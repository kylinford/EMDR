using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class EMDRIntro_ButtonStart : MonoBehaviour {

    public void Clicked()
    {
        StartCoroutine(Coroutine_Clicked());

    }

    IEnumerator Coroutine_Clicked()
    {
        EMDRData ed = FindObjectOfType<EMDRData>();

        if (!FindObjectOfType<InputField>())
            StartGame();
        else
        {
            string fn = FindObjectOfType<InputField>().text;
            yield return StartCoroutine(ed.Coroutine_CreateUser(fn));
            StartGame();
        }
    }

    public void StartGame()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Contains("iPad"))
            SceneManager.LoadScene("Pre_iPad");
        else if (sceneName.Contains("iPhone")) {
        }
    }
}
