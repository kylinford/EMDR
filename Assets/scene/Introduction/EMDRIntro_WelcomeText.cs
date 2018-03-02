using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EMDRIntro_WelcomeText : MonoBehaviour {

	// Use this for initialization
	void Start () {

        EMDRData ed = FindObjectOfType<EMDRData>();
        string pdpPlayerInfo = Application.persistentDataPath + ed.Filename_PlayerInfo;

        if (File.Exists(pdpPlayerInfo))
        {
            User user = ed.User;
            GetComponent<Text>().text = "Welcome back, " + user.firstname;
        }
        else
            GetComponent<Text>().text = "Error: Loading user info fail.";
    }

    // Update is called once per frame
    void Update () {
		
	}


}
