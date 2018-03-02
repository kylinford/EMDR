using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class EMDRGame_Controller : MonoBehaviour {
    public GameObject data;
    public Color colorButtonSelected = new Color(237.0f / 255.0f, 255.0f / 255.0f, 195.0f / 255.0f, 139.0f / 255.0f);

    bool isSmall = true;
    EMDRData ed;
    bool initDone = false; public bool InitDone { get { return initDone; } }

    // Use this for initialization
    IEnumerator Start () {
        //Advertisement.Show();

        //Init ed;
        bool edInited = false;
        if (!FindObjectOfType<EMDRData>())
        {
            Instantiate(data);
        }
        ed = FindObjectOfType<EMDRData>();
        yield return StartCoroutine(ed.Init());
        edInited = true;

        //All init done
        initDone = edInited && true; //For later use (other inits)
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ZoomCamera()
    {
        if (!isSmall)
        {
            Camera.main.transform.position = new Vector3(0, 0, -5);
            Camera.main.orthographicSize = 5;
            isSmall = true;
        }
        else
        {
            GameObject panelBack = GameObject.Find("Panel_Back");
            Vector3 camerPosition = panelBack.transform.position + new Vector3(0, 0, -5);
            Camera.main.transform.position = camerPosition;
            Camera.main.orthographicSize = 5 * 1400.0f / 1024.0f;
            isSmall = false;
        }

    }

    IEnumerator CameraMoveTo(Vector3 nePostion)
    {
        yield return new WaitForFixedUpdate();
    }
}
