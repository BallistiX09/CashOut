using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private int cash = 0;
    private int mood = 0;
    private int savings = 0;
    private int currentDay = 1;
    private DateTime currentDate = System.DateTime.Now;

    [SerializeField] private Text cashAmountText, moodAmountText, savingsAmountText;
    [SerializeField] private Text dateText;
    [SerializeField] private Text eventTitleText;
    [SerializeField] private Image eventImage;
    [SerializeField] private Text eventDescriptionText;
    [SerializeField] private Text leftChoiceButtonText, rightChoiceButtonText;

    private void Start()
    {
        dateText.text = GenerateDateText();
    }

    private string GenerateDateText()
    {
        return " <b>Day " + currentDay.ToString() + "/30</b> | " + currentDate.ToString("MMMM d, yyyy");
    }
}
