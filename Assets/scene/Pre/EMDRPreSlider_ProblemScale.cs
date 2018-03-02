using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMDRPreSlider_ProblemScale : MonoBehaviour {
    public GameObject buttonNextQuestion;
    EMDRPre_Controller controller;
    EMDRData ed;
    Text text;
    int min = 0;
    int max = 10;

    // Use this for initialization
    IEnumerator Start() {
        controller = FindObjectOfType<EMDRPre_Controller>();
        text = transform.Find("Text_Score").GetComponent<Text>();
        transform.Find("Text (1)").GetComponent<Text>().text = max.ToString();

        yield return new WaitUntil(() => controller.InitDone);
        ed = FindObjectOfType<EMDRData>();

        //Update UI
        UpdateUI(ed.LoadLocal_Round());
    }

    void UpdateUI(Round round)
    {
        float sliderValue = (float)(round.problemScale_Pre - min) / (float)(max - min);
        GetComponent<Slider>().value = sliderValue;
        int problemScale_Pre = round.problemScale_Pre;
        text.text = problemScale_Pre == -1 ? "Slide to choose" : problemScale_Pre.ToString(); 

        UpdateButtonNextQuestion(round);

    }

    void UpdateButtonNextQuestion(Round round)
    {
        if (transform.parent.Find("EMDRPreButton_NextQuestion(Clone)"))
            return;
        else
        {
            if (round.problemScale_Pre != -1)
            {
                GameObject newButton = JWMonoBehaviour.JWNew_UI(buttonNextQuestion, transform.parent.gameObject);
                GameObject nextQuestion = GameObject.Find("EMDRPrePanel_Question (3)");
                newButton.GetComponent<EMDRPreButton_NextQuestion>().nextQuestion = nextQuestion;
            }
        }
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
            newRound.problemScale_Pre = result;
            ed.SaveLocal_Round(newRound);

            UpdateUI(newRound);
        }
    }
}
