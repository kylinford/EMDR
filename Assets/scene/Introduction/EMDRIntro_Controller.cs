using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class EMDRIntro_Controller : MonoBehaviour {
    public GameObject UINewuser;
    public GameObject UIExistinguser;

    // Use this for initialization
    IEnumerator Start () {
        EMDRData ed = FindObjectOfType<EMDRData>();
        yield return StartCoroutine(ed.Init());

        string pdpUserInfo = Application.persistentDataPath + ed.Filename_PlayerInfo;

        if (File.Exists(pdpUserInfo))
        {
            JWMonoBehaviour.JWNew_UI(UIExistinguser, FindObjectOfType<Canvas>().gameObject);
            //FindObjectOfType<EMDRIntro_WelcomeText>().GetComponent<Text>().text = "Welcome back, " + ed.User.firstname;
        }
        else
        {
            JWMonoBehaviour.JWNew_UI(UINewuser, FindObjectOfType<Canvas>().gameObject);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

}
