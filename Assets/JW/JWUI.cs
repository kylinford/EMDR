using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JWUI : JWMonoBehaviour {
    GameObject uiBeingToggled;

    // Use this for initialization
    void Start () {
        // Sets the MyValidate method to invoke after the input field's default input validation invoke (default validation happens every time a character is entered into the text field.)
        if (GetComponent<InputField>())
            GetComponent<InputField>().onValidateInput += delegate (string input, int charIndex, char addedChar)
                { return JWMonoBehaviour.Validate(addedChar); };

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void Hide(int direction)
    {
        Vector3 offset = Vector3.zero;
        switch (direction)
        {
            case 0://Up
            case 1:
                break;
            case 2://left
                float distance = 512.0f + transform.localPosition.x + GetComponent<RectTransform>().rect.width / 2.0f;
                print(distance);
                offset = new Vector3(-distance, 0, 0);
                break;
            case 3://right
                break;
        }
        transform.parent.localPosition = transform.parent.localPosition + offset;
    }

    public void Exit()
    {
        Destroy(transform.parent.gameObject);
    }

    //AutoSize still has some problem
    public void AutoSize(float minWidth, float minHeight, float widthOffset, float heightOffset)
    {
        float y_top = 0, y_bottom = 0, x_left = 0, x_right = 0;
        foreach(Transform child in transform)
        {
            Vector2 childPosition = child.transform.localPosition;
            Rect childRect = child.GetComponent<RectTransform>().rect;
            float childY_top = childPosition.y + childRect.height / 2.0f;
            float childY_bottom = childPosition.y - childRect.height / 2.0f;
            float childX_Left = childPosition.x - childRect.width / 2.0f;
            float childX_Right = childPosition.x + childRect.width / 2.0f;

            y_top = y_top > childY_top ? y_top : childY_top;
            y_bottom = y_bottom < childY_bottom ? y_bottom : childY_bottom;
            x_left = x_left < childX_Left ? x_left : childX_Left;
            x_right = x_right > childX_Right ? x_right : childX_Right;
        }
        float rectWidth = x_right - x_left + widthOffset > minWidth ? x_right - x_left + widthOffset : minWidth;
        float rectHeight = y_top - y_bottom + heightOffset > minHeight ? y_top - y_bottom + heightOffset : minHeight;
        GetComponent<RectTransform>().sizeDelta = new Vector2(rectWidth, rectHeight);

    }

    //Game 
    public void PauseGame(GameObject pausePanel)
    {
        SetAllButtonsActive(false);
        Time.timeScale = 0;
        InstantiateUI(pausePanel);
    }
    public void ResumeGame(GameObject pausePanel)
    {
        SetAllButtonsActive(true);
        Time.timeScale = 1;
        Destroy(pausePanel.gameObject);
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Scene
    public virtual void LoadScene(string scenename)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scenename);
    }
    public void LoadNextScene()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            print("Scene index out of range");            
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Show and hide other UI
    void SetAllButtonsActive(bool b)
    {
        Button[] allbuttons = FindObjectsOfType<Button>();
        foreach (Button button in allbuttons)
            button.enabled = b;
    }
    public void InstantiateUI(GameObject uiPrefab)
    {
        InstantiateUI(uiPrefab, uiPrefab.transform.position);
    }
    void InstantiateUI(GameObject uiPrefab, Vector3 position)
    {
        JWInstantiateUnderParent(uiPrefab, GameObject.Find("Canvas"), position);
    }
    public void ToggleUI(GameObject uiObject) {
        if (!uiObject.activeSelf)
            uiObject.SetActive(true);
        else
            uiObject.SetActive(false);
    }

    public void ToggleUI_Prefab(GameObject uiObject)
    {
        
        GameObject canvas = FindObjectOfType<Canvas>().gameObject;
        if (!uiBeingToggled)
        {
            GameObject newUI = JWNew_UI(uiObject, canvas);
            uiBeingToggled = newUI;
        }
        else
        {
            Destroy(uiBeingToggled);
        }
    }


    public void ShowText()
    {
        GetComponentInChildren<Text>().enabled = true;
    }
    public void HideText()
    {
        GetComponentInChildren<Text>().enabled = false;
    }
}
