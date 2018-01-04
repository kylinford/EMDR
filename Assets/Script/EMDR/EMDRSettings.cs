using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.IO;

public class EMDRSettings {
    EMDRSettings_Believes believes;

    public EMDRSettings_Believes Believes { get { return believes; } }

    public EMDRSettings()
    {
        string xmlData = (Resources.Load("settings") as TextAsset).text;
        if (xmlData == null)
            return;

        XmlReader reader = XmlReader.Create(new StringReader(xmlData));
        bool readerValid = true;
        if (reader == null)
        {
            Debug.Log("no data");
            readerValid = false;
        }

        if (readerValid)
        {
            reader.ReadStartElement();

            //Content
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "believes":
                                believes = new EMDRSettings_Believes(reader);
                                break;
                        }
                        break;
                    case XmlNodeType.Text:
                    case XmlNodeType.EndElement:
                    default:
                        break;
                }
            }
        }
        //print();
    }

    void print()
    {
        believes.print();
    }
}

public class EMDRSettings_Believes
{
    string currText = "";
    List<EMDRSettings_Believe> content = new List<EMDRSettings_Believe>();

    public string CurrText { get { return currText; } }
    public List<EMDRSettings_Believe> Content { get { return content; } }

    public EMDRSettings_Believes(XmlReader reader)
    {
        XmlReader readerSubtree = reader.ReadSubtree();
        //Attribute
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case "currText":
                    currText = reader.Value;
                    break;
                default:
                    break;
            }
        }

        //Customize object
        while (readerSubtree.Read())
        {
            switch (readerSubtree.NodeType)
            {
                case XmlNodeType.Element:
                    switch (reader.Name)
                    {
                        case "believe":
                            content.Add(new EMDRSettings_Believe(reader));
                            break;
                    }
                    break;
            }
        }
    }

    public void print()
    {
        Debug.Log("Believes: CurrText=" + currText);
        foreach (EMDRSettings_Believe believe in content)
            believe.print();
    }

}

public class EMDRSettings_Believe
{
    string text;

    public string Text { get { return text; } }

    public EMDRSettings_Believe(XmlReader reader)
    {
        //Attribute
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                case "text":
                    text = reader.Value;
                    break;
                default:
                    break;
            }
        }
    }

    public void print()
    {
        Debug.Log("Believe: text=" + text);
    }
}

