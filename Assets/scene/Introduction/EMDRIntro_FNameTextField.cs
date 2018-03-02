using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EMDRIntro_FNameTextField : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ToggleUI_Prefab_byEmptiness(GameObject uiObject)
    {
        Vector3 position = uiObject.transform.localPosition;
        if (SceneManager.GetActiveScene().name.Contains("iPad"))
            position = new Vector3(0, -500, 0);

        GameObject canvas = FindObjectOfType<Canvas>().gameObject;
        if (GetComponent<InputField>().text != "")
        {
            if (!GameObject.Find("EMDRIntro_ButtonStart(Clone)"))
                JWMonoBehaviour.JWNew_UI(uiObject, canvas, position);
        } else
            Destroy(GameObject.Find("EMDRIntro_ButtonStart(Clone)"));
    }

}
