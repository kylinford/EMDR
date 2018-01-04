using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMDRButton_SceneList : JWMonoBehaviour {
    GameObject Panel_SceneSelect;

    // Use this for initialization
    void Start () {
        Panel_SceneSelect = Resources.Load<GameObject>("Prefab/Panel_SceneSelect/Panel_SceneSelect");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleSceneListPanel()
    {
        if (!FindObjectOfType<EMDRPanel_SceneSelect>())
            JWInstantiateUnderParent_UI(Panel_SceneSelect, gameObject, false);
        else
            Destroy(FindObjectOfType<EMDRPanel_SceneSelect>().gameObject);
    }
}
