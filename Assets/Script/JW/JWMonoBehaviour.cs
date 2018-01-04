using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System;

public class JWMonoBehaviour : MonoBehaviour
{
    bool canvasMatchWidth = true;

    protected GameObject Alert(GameObject panel, string text, bool gradually)
    {
        GameObject newAlert = JWInstantiateUnderParent_UI(panel, FindObjectOfType<Canvas>().gameObject, gradually);
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

    protected GameObject JWInstantiateUnderParent(GameObject target, GameObject parent, Vector3 position, Vector3 localscale, Vector3 rotation)
    {
        GameObject newObject = Instantiate(target);
        newObject.transform.SetParent(parent.transform);
        newObject.transform.localScale = localscale;
        newObject.transform.localPosition = position;
        newObject.transform.localEulerAngles = rotation;
        return newObject;
    }

    protected GameObject JWInstantiateUnderParent(GameObject target, GameObject parent, Vector3 position)
    {
        return JWInstantiateUnderParent(target, parent, position, Vector3.one, Vector3.zero);
    }

    protected GameObject JWInstantiateUnderParent_UI(GameObject target, GameObject parent, string mode, object content, bool gradually, Vector3 localPosition, Vector3 localScale, Vector3 eulerAngles)
    {
        GameObject newGameObject = JWInstantiateUnderParent(target, parent, localPosition, localScale, eulerAngles);
        float appearSpeed = 1;
        switch (mode)
        {
            case "Image":
                Image newImage = newGameObject.GetComponent<Image>();
                if (gradually)
                    StartCoroutine(JWUIFadeIn_UI(newImage, appearSpeed));
                newImage.sprite = (Sprite)content;
                break;
            case "Text":
                Text newText = newGameObject.GetComponent<Text>();
                if (gradually)
                    StartCoroutine(JWUIFadeIn_UI(newText, appearSpeed));
                newText.text = (string)content;
                break;
            case "InputField":
                InputField newInputField = newGameObject.GetComponent<InputField>();
                if (gradually)
                    StartCoroutine(JWUIFadeIn_UI(newInputField.GetComponent<Image>(), appearSpeed));
                newInputField.placeholder.GetComponent<Text>().text = (string)content;
                break;
            case "Button":
                Button newButton = newGameObject.GetComponent<Button>();
                if (gradually)
                {
                    StartCoroutine(JWUIFadeIn_UI(newButton.GetComponent<Image>(), appearSpeed));
                    StartCoroutine(JWUIFadeIn_UI(newButton.GetComponentInChildren<Text>(), appearSpeed));
                }
                newButton.GetComponentInChildren<Text>().text = (string)content;
                break;
            default:
                if (gradually)
                    foreach (Graphic graphic in newGameObject.GetComponentsInChildren<Graphic>())
                        StartCoroutine(JWUIFadeIn_UI(graphic, 1));
                break;

        }
        return newGameObject;
    }

    protected GameObject JWInstantiateUnderParent_UI(GameObject target, GameObject parent, string mode, object content, bool gradually)
    {
        return JWInstantiateUnderParent_UI(target, parent, mode, content, gradually, target.transform.localPosition, target.transform.localScale, target.transform.localEulerAngles);
    }

    protected GameObject JWInstantiateUnderParent_UI(GameObject target, GameObject parent, bool gradually)
    {
        GameObject newGameObject = JWInstantiateUnderParent_UI(target, parent, "", "", gradually);
        return newGameObject;
    }
    protected GameObject JWInstantiateUnderParent_UI(GameObject target, GameObject parent, bool gradually, Vector3 localposition)
    {
        GameObject newGameObject = JWInstantiateUnderParent_UI(target, parent, "", "", gradually, localposition, target.transform.localScale, target.transform.localEulerAngles);
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
