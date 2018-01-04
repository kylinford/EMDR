using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EMDRPanel_Main : JWMonoBehaviour {
    //public string levelID;
    List<Image> dots =  new List<Image>();
    float interval;
    float timer;
    string defaultBelieveText = "I want to believe...";
    public TextAsset[] xmlTexts;

    public float Interval { get { return interval; } set { interval = value; } }
    public float Timer { get { return timer; } set { timer = value; } }
    public string DefaultBelieveText { get { return defaultBelieveText; } }
    public TextAsset[] XmlTexts { get { return xmlTexts; } }

    // Use this for initialization
    void Start () {
        xmlTexts = Resources.LoadAll<TextAsset>("Levels");
        LoadLevel();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void LoadLevel()
    {
        LoadLevel(PlayerPrefs.GetInt("CurrLevel"));
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= xmlTexts.Length)
            return;
        StopAllCoroutines();
        PlayerPrefs.SetInt("CurrLevel", levelIndex);
        EMDRLevel level = new EMDRLevel(xmlTexts[PlayerPrefs.GetInt("CurrLevel", levelIndex)]);
        if (level == null)
            return;

        EMDRPanel_Main_Dot[] panelDots = FindObjectsOfType<EMDRPanel_Main_Dot>();
        foreach (EMDRPanel_Main_Dot dot in panelDots)
            Destroy(dot.gameObject);
        interval = level.Property.Interval;
        timer = level.Property.Timer;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/" + level.Property.Bgsrc);
        GameObject Image_Dot = Resources.Load<GameObject>("Prefab/Panel_Main/Image_Dot");
        dots.Clear();

        if (GameObject.Find("Text_Instruction(Clone)"))
            Destroy(GameObject.Find("Text_Instruction(Clone)"));
        if (FindObjectOfType<EMDRPanel_Main_Next>())
            Destroy(FindObjectOfType<EMDRPanel_Main_Next>().gameObject);
        if (!FindObjectOfType<EMDRButton_Start>())
        {
            GameObject Button_Start = Resources.Load<GameObject>("Prefab/Panel_Main/Button_Start");
            GameObject newButtonStart = JWInstantiateUnderParent_UI(Button_Start, transform.parent.gameObject, false, Button_Start.transform.localPosition);
            newButtonStart.GetComponent<RectTransform>().anchoredPosition = Button_Start.transform.localPosition;
        
        }

        Text TextBelieve = transform.FindChild("Button_Goal").GetComponentInChildren<Text>();
        TextBelieve.text = PlayerPrefs.GetString("BelieveText", defaultBelieveText);
        TextBelieve.color = level.Property.TextColor;

        foreach (EMDRLevel_Dot dot in level.Dots.Content)
        {
            GameObject newDot = JWInstantiateUnderParent_UI(Image_Dot, gameObject, false);
            newDot.transform.localPosition = new Vector3(dot.X, dot.Y, 0);
            newDot.transform.localScale = newDot.transform.localScale * level.Property.DotScale;
            newDot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/" + level.Property.Dotsrc);
            dots.Add(newDot.GetComponent<Image>());
        }

    }

    public void StartCoroutinePointsMove()
    {
        StartCoroutine(PointsMove());
    }

    IEnumerator PointsMove()
    {
        //Set all dots to be transparent
        foreach (Image point in dots)
            point.color = new Color(1, 1, 1, 0);

        //Init speed, direction and i
        float speed = 1.0f / (interval / 2.0f);
        int direction = 1;
        int i = 0;

        //Movement animation
        while (timer > 0)
        {
            Image point = dots[i];
            while (point.color.a < 1)
            {
                yield return new WaitForEndOfFrame();
                timer -= Time.deltaTime;
                point.color = new Color(1, 1, 1, point.color.a + speed * Time.deltaTime);
            }
            while (point.color.a > 0)
            {
                yield return new WaitForEndOfFrame();
                timer -= Time.deltaTime;
                point.color = new Color(1, 1, 1, point.color.a - speed * Time.deltaTime);
            }
            if (i + 1 == dots.Count)
                direction = -1;
            if (i == 0)
                direction = 1;
            i += direction;
        }

        //Finished level 

        //Show next level button
        GameObject Text_Instruction = GameObject.Find("Text_Instruction(Clone)");
        GameObject Button_Next = Resources.Load<GameObject>("Prefab/Panel_Main/Button_Next");
        GameObject newButtonNext = JWInstantiateUnderParent_UI(Button_Next, gameObject, false);
        newButtonNext.transform.position = Text_Instruction.transform.position;
        Destroy(Text_Instruction);
    }
}
