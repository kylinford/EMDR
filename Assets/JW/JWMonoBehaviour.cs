using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System;

public class JWMonoBehaviour : MonoBehaviour
{
    static string invalidChars = ",/~*";

    bool canvasMatchWidth = true;


    public static char Validate(char charToValidate)
    {
        //Checks if a dollar sign is entered....

        if (invalidChars.Contains(charToValidate.ToString()))
        {
            // ... if it is change it to an empty character.
            charToValidate = '\0';
        }
        return charToValidate;
    }

    protected GameObject Alert(GameObject panel, string text)
    {
        GameObject newAlert = JWNew_UI(panel, FindObjectOfType<Canvas>().gameObject);
        newAlert.GetComponentInChildren<Text>().text = text;
        return newAlert;
    }

    protected void UpdateXmlAttribute(string xmlFile, string attributePath, string newValue)
    {
        //nodePath format example: "//element/@attribute"
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlFile);

        XmlAttribute attribute = (XmlAttribute)xmlDoc.SelectSingleNode(attributePath);
        if (attribute != null)
            attribute.Value = newValue; // Set to new value.
        xmlDoc.Save(xmlFile);
    }
    protected Sprite LoadSprite(string src)
    {
        return LoadSprite(src, "-1");
    }

    public static bool ShouldLoadSource()
    {
        return Application.platform == RuntimePlatform.WebGLPlayer
            || Application.platform == RuntimePlatform.OSXPlayer;
    }

    protected Sprite LoadSprite(string src, string questID)
    {
        if (src == "")
        {
            print("Loading error at " + src);
            return null;
        }

		if(ShouldLoadSource())
        {
            string srcWithoutExt = src.Split(new char[] { '.' })[0];
            string dir = "";
            if (Single.Parse(questID) > 0)
                dir = "Content/QuestLib/" + questID + "/" + srcWithoutExt;
            else
                dir = "Content/Art/" + srcWithoutExt;

            return Resources.Load<Sprite>(dir);
        }
        else
        {
            string dir = "";
            if (Single.Parse(questID) > 0)
                dir = "QuestLib/" + questID + "/" + src;
            else
                dir = "Art/" + src;

            byte[] pngBytes = File.ReadAllBytes(dir);
            if (pngBytes == null)
                return null;
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(pngBytes);
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
    }

    public static GameObject JWNew(GameObject target, GameObject parent, Vector3 position, Vector3 localscale, Vector3 rotation)
    {
        GameObject newObject = Instantiate(target);
        newObject.transform.SetParent(parent.transform);
        newObject.transform.localScale = localscale;
        newObject.transform.localPosition = position;
        newObject.transform.localEulerAngles = rotation;
        return newObject;
    }

    public static GameObject JWInstantiateUnderParent(GameObject target, GameObject parent, Vector3 position)
    {
        return JWNew(target, parent, position, Vector3.one, Vector3.zero);
    }

    public static GameObject JWNew_UI(GameObject target, GameObject parent, string mode, object content, Vector3 localPosition, Vector3 localScale, Vector3 eulerAngles)
    {
        GameObject newGameObject = JWNew(target, parent, localPosition, localScale, eulerAngles);
        switch (mode)
        {
            case "Image":
                Image newImage = newGameObject.GetComponent<Image>();
                newImage.sprite = (Sprite)content;
                break;
            case "Text":
                Text newText = newGameObject.GetComponent<Text>();
                newText.text = (string)content;
                break;
            case "InputField":
                InputField newInputField = newGameObject.GetComponent<InputField>();
                newInputField.placeholder.GetComponent<Text>().text = (string)content;
                break;
            case "Button":
                Button newButton = newGameObject.GetComponent<Button>();
                newButton.GetComponentInChildren<Text>().text = (string)content;
                break;
            default:
                break;

        }
        return newGameObject;
    }

    public static GameObject JWNew_UI(GameObject target, GameObject parent, string mode, object content)
    {
        return JWNew_UI(target, parent, mode, content, target.transform.localPosition, target.transform.localScale, target.transform.localEulerAngles);
    }

    public static GameObject JWNew_UI(GameObject target, GameObject parent)
    {
        GameObject newGameObject = JWNew_UI(target, parent, "", "");
        return newGameObject;
    }

    public static GameObject JWNew_UI(GameObject target, GameObject parent, Vector3 localposition)
    {
        GameObject newGameObject = JWNew_UI(target, parent, "", "", localposition, target.transform.localScale, target.transform.localEulerAngles);
        return newGameObject;
    }

    protected string ChooseOneFrom(string[] strings)
    {
        int index = UnityEngine.Random.Range(0, strings.Length);
        return strings[index];
    }

    protected bool isIn(int element, List<int> list)
    {
        foreach (int listelement in list)
            if (listelement == element)
                return true;
        return false;
    }

    protected IEnumerator Wait(float timer)
    {
        yield return new WaitForSeconds(timer);
    }

    protected string[] JWParseString(string originStr, char[] delimiterChars)
    {
        //string sampleText = "one\ttwo three:four,five six seven";
        string[] splitedStr = originStr.Split(delimiterChars);
        return splitedStr;
    }

    protected string[] JWParseString(string originStr)
    {
        char[] delimiterChars = { ' ', ',', '.', ':', '\t', '\n' };
        return JWParseString(originStr, delimiterChars);
    }

    IEnumerator JWUIFadeIn_UI(Graphic graphic, float speed)
    {
        graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 0);
        while (graphic && graphic.color.a < 1)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, graphic.color.a + speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    protected Vector2 CalculateLocalLocation(string relative, Vector2 location, GameObject parent)
    {
        Vector2 ret = location;
        Rect contentRect = parent.GetComponent<RectTransform>().rect;
        switch (relative)
        {
            case "x":
                ret = new Vector2(contentRect.width * location.x, location.y);
                break;
            case "y":
                ret = new Vector2(location.x, contentRect.height * location.y);
                break;
            case "xy":
                ret = new Vector2(contentRect.width * location.x, contentRect.height * location.y);
                break;
        }
        return ret;
    }

    protected void AdjustCanvasRatio(float WidthByHeight)
    {
        GameObject Canvas = FindObjectOfType<Canvas>().gameObject;

        if (Camera.main.aspect > WidthByHeight)
        {
            if (canvasMatchWidth)
            {
                Canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1; //0 for width, 1 for height
                canvasMatchWidth = false;
            }
        }
        else
        {
            if (!canvasMatchWidth)
            {
                Canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0; //0 for width, 1 for height
                canvasMatchWidth = true;
            }
        }
    }

    protected IEnumerator GetWWWCoroutine(string url, WWW www)
    {
        WWW ret = new WWW(url);
        yield return ret;
        if (!string.IsNullOrEmpty(ret.error))
            Debug.Log(ret.error + " " + url);
        else
            www = ret;
    }
}
