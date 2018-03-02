using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreSlider_BelieveScale : MonoBehaviour {
    EMDRPre_Controller controller;
    EMDRData ed;
    Text text;
    int min = 0;
    int max = 7;

    // Use this for initialization
    IEnumerator Start()
    {
        controller = FindObjectOfType<EMDRPre_Controller>();
        text = transform.Find("Text_Score").GetComponent<Text>();
        transform.Find("Text (1)").GetComponent<Text>().text = max.ToString();
        yield return new WaitUntil(() => controller.InitDone);
        ed = FindObjectOfType<EMDRData>();

        //Update UI
        UpdateUI();
    }

    void UpdateUI()
    {
        Round round = ed.LoadLocal_Round();
        float sliderValue = (float)(round.believeScale_Pre - min) / (float)(max - min);
        GetComponent<Slider>().value = sliderValue;
        int believeScale_Pre = round.believeScale_Pre;
        text.text = believeScale_Pre == -1 ? "Slide to choose" : believeScale_Pre.ToString(); ;
    }

    public void ValueChanged()
    {

        float range = (float)max - (float)min;
        float interval = 1.0f / range;
        float value_Slider = GetComponent<Slider>().value;
        int result = (int)((value_Slider + interval / 2.0f) / interval);

        if (transform.Find("Text_Score").GetComponent<Text>().text != result.ToString())
        {
            transform.Find("Text_Score").GetComponent<Text>().text = result.ToString();

            //Save Round
            Round newRound = ed.LoadLocal_Round();
            newRound.believeScale_Pre = result;
            ed.SaveLocal_Round(newRound);
        }
    }
}
