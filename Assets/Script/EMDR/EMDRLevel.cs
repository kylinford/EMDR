using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class EMDRLevel
{
    //string id;
    EMDRLevel_Property property;
    EMDRLevel_Dots dots;

    public EMDRLevel_Property Property { get { return property; } set { property = value; } }
    public EMDRLevel_Dots Dots { get { return dots; } }
    //public string ID { get { return id; } }

    public EMDRLevel(TextAsset xmlText)
    {
        /*string xmlData = Resources.Load<TextAsset>("Levels/" + levelID).text;     
        if (xmlData == null)
            return;
        id = levelID;
        */
        string xmlData = xmlText.text;
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
                            case "dots":
                                dots = new EMDRLevel_Dots(reader);
                                break;
                            case "property":
                                property = new EMDRLevel_Property(reader);
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

    IEnumerator CreateXmlReader(string url, string XmlData)
    {
        WWW www = new WWW(url);
        //yield return new WaitForSeconds(2);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
            Debug.Log(www.error + " " + url);
        //StreamReader streamReader = new StreamReader(url);
        //reader = XmlReader.Create(streamReader);
        XmlData = www.text;// DataCollection.DeSerialize(result);
    }

    void print()
    {
        property.print();
        dots.print();
    }
}

public class EMDRLevel_Property
{
    string name;
    float timer;
    float interval;
    string bgsrc;
    string dotsrc;

    float dotscale=1;
    string bgsrc_16x9;//1920x1080
    string bgsrc_4x3;//1024x768;
    string bgsrc_5x4;
    string bgsrc_3x2;
    string bgsrc_16x10;

    float textcolor_r;
    float textcolor_g;
    float textcolor_b;
    Color textcolor;

    public string Name { get { return name; } }
    public float Timer { get { return timer; } set { timer = value; } }
    public float Interval { get { return interval; } }
    public string Bgsrc { get {
            string ret = bgsrc;
            float cameraAspect = Camera.main.aspect;
            if (cameraAspect == 16.0f / 9.0f)
                ret = bgsrc_16x9 != null ? bgsrc_16x9 : bgsrc;
            else if(cameraAspect == 4.0f / 3.0f)
                ret = bgsrc_4x3 != null ? bgsrc_4x3 : bgsrc;
            else if (cameraAspect == 5.0f / 4.0f)
                ret = bgsrc_5x4 != null ? bgsrc_5x4 : bgsrc;
            else if (cameraAspect == 3.0f / 2.0f)
                ret = bgsrc_3x2 != null ? bgsrc_3x2 : bgsrc;
            else if (cameraAspect == 16.0f / 10.0f)
                ret = bgsrc_16x10 != null ? bgsrc_16x10 : bgsrc;
            return ret;
        }
    }
    public string Dotsrc { get { return dotsrc; } }
    public float DotScale { get { return dotscale; } }
    public Color TextColor { get { return new Color(textcolor_r, textcolor_g, textcolor_b); } }

    public EMDRLevel_Property(XmlReader reader)
    {
        //Attribute
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {

                case "name":
                    name = reader.Value;
                    break;
                case "timer":
                    Single.TryParse(reader.Value, out timer);
                    break;
                case "interval":
                    Single.TryParse(reader.Value, out interval);
                    break;
                case "bgsrc":
                    bgsrc = reader.Value;
                    break;
                case "dotsrc":
                    dotsrc = reader.Value;
                    break;
                case "dotscale":
                    Single.TryParse(reader.Value, out dotscale);
                    break;
                case "bgsrc_16x9":
                    bgsrc_16x9 = reader.Value;
                    break;
                case "bgsrc_4x3":
                    bgsrc_4x3 = reader.Value;
                    break;
                case "bgsrc_5x4":
                    bgsrc_5x4 = reader.Value;
                    break;
                case "bgsrc_3x2":
                    bgsrc_3x2 = reader.Value;
                    break;
                case "bgsrc_16x10":
                    bgsrc_16x10 = reader.Value;
                    break;
                case "textcolor-r":
                    Single.TryParse(reader.Value, out textcolor_r);
                    break;
                case "textcolor-g":
                    Single.TryParse(reader.Value, out textcolor_g);
                    break;
                case "textcolor-b":
                    Single.TryParse(reader.Value, out textcolor_b);
                    break;

                default:
                    break;
            }
        }
    }
    public void print()
    {
        Debug.Log("Property: name=" + name + "; timer=" + timer 
            + "; interval=" + interval + "; bgsrc=" + bgsrc 
            + "; dotsrc=" + dotsrc + ";textcolor_r=" + textcolor_r
            + ";textcolor_g=" + textcolor_g + ";textcolor_b=" + textcolor_b);
    }
}

public class EMDRLevel_Dot
{
    float x;
    float y;

    public float X { get { return x; } }
    public float Y { get { return y; } }

    public EMDRLevel_Dot(XmlReader reader)
    {
        //Attribute
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {

                case "x":
                    Single.TryParse(reader.Value, out x);
                    break;
                case "y":
                    Single.TryParse(reader.Value, out y);
                    break;
                default:
                    break;
            }
        }
    }

    public void print()
    {
        Debug.Log("Dot: x=" + x + "; y=" + y);
    }
}

public class EMDRLevel_Dots 
{
    ArrayList content;

    public ArrayList Content { get { return content; } set { content = value; } }

    public EMDRLevel_Dots(XmlReader reader)
    {
        XmlReader readerSubtree = reader.ReadSubtree();
        content = new ArrayList();
        //Attribute
        while (reader.MoveToNextAttribute())
        {
            switch (reader.Name)
            {
                default:
                    break;
            }
        }

        //Customize object
        while (readerSubtree.Read())
        {
            switch (readerSubtree.NodeType)
            {
                /*case XmlNodeType.Text:
                    content.Add(reader.Value);
                    break;*/
                case XmlNodeType.Element:
                    switch (reader.Name)
                    {
                        case "dot":
                            content.Add(new EMDRLevel_Dot(reader));
                            break;
                    }
                    break;
            }
        }
    }

    public void print()
    {
        foreach (EMDRLevel_Dot dot in content)
            dot.print();
    }
}
