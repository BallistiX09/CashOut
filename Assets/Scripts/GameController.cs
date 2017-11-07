﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //Globally-accessible variables keeping track of current game state
    public static int currentCash = 0;
    public static int currentMood = 0; //Mood based on int value from 0 to 100, with 50 being neutral
    public static int currentSavings = 0;
    public static int currentDay = 1;
    public static DateTime currentDate = System.DateTime.Now;
    public static RandomEvent currentEvent, lastEvent;

    private UIController UIController;
    [SerializeField] private List<RandomEvent> events = new List<RandomEvent>();

    private void Start()
    {
        //Finds a reference to UI Controller for future usage
        UIController = FindObjectOfType<UIController>();

        //Set up variables for first startup
        SetUpStats();

        //Select a random event from the list of events
        currentEvent = events[0];

        //Update game UI with startup information
        UIController.UpdateDateText();
        UIController.UpdateEventInformation();
        UIController.UpdateStatsText();
    }

    public void ButtonNoPressed()
    {
        //Trigger a new game day
        NextDay();
    }

    public void ButtonYesPressed()
    {
        //Trigger a new game day
        NextDay();
    }

    private void NextDay()
    {
        //40 interactive days total, with random non-interactive events mixed throughout

        currentDay++;

        if(currentDay > 8){
            UIController.EndGame();
        }

        //Check if game finished here
        currentDate = currentDate.AddDays(UnityEngine.Random.Range(3, 6));

        currentEvent = events[currentDay - 1];

        /*//Ensures two identical events don't trigger in a row
        lastEvent = currentEvent;
        do
        {
            currentEvent = events[UnityEngine.Random.Range(0, events.Count)];
        } while (currentEvent == lastEvent);*/
        

        UIController.UpdateDateText(); //Maybe switch to delegates for day update
        UIController.UpdateEventInformation();

        //Update income and expenses each time new month entered on specific dates
    }

    private void SetUpStats()
    {
        //Game stats are randomised each time to simulate real life uncertainty
        //TODO Tweak amounts to balance gameplay when events added to game
        currentCash = (int)Mathf.Round((UnityEngine.Random.Range(200, 800)) / 10) * 10;
        currentSavings = (int)Mathf.Round((UnityEngine.Random.Range(0, 1000)) / 10) * 10;
        currentMood = UnityEngine.Random.Range(30, 70);
    }
}
