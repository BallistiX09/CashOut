using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Globally-accessible variables keeping track of current game state
    public static int currentCash = 0;
    public static int currentMood = 0; //Mood based on int value from 0 to 100, with 50 being neutral
    public static int currentSavings = 0;
    public static int currentDay = 1;
    public static DateTime currentDate = System.DateTime.Now;
    public static RandomEvent currentEvent, lastEvent;
    private UIController UIController;
    [SerializeField] private Animator animationController;

    //TODO Add linked events and non-interactive events
    [SerializeField] private List<RandomEvent> randomEvents = new List<RandomEvent>();

    private void Start()
    {
        //Sets the target game framerate
        Application.targetFrameRate = 45;

        //Finds a reference to UI Controller for future usage
        UIController = FindObjectOfType<UIController>();

        //Set up variables for first startup
        SetUpStats();

        //Select a random event from the list of events
        currentEvent = randomEvents[UnityEngine.Random.Range(0, randomEvents.Count)];

        //Update game UI with startup information
        UIController.UpdateDateText();
        UIController.UpdateEventInformation();
        UIController.UpdateAllStatsText();
    }

    public void ButtonNoPressed()
    {
        if (animationController.GetCurrentAnimatorStateInfo(0).IsName("Cards Out Idle") || animationController.GetCurrentAnimatorStateInfo(0).IsName("Cards In Idle"))
        {
            //TODO Update to relate to yes or no button press, currently used as debug only
            //Update stats text, using animations and setting the new value after animation is complete
            UIController.StartCoroutine("AnimateCashText", currentCash + currentEvent.moneyEffect / 2);
            UIController.StartCoroutine("AnimateSavingsText", Mathf.Clamp(currentSavings + currentEvent.moneyEffect / 2, 0, 100000));

            //Update mood value and mood text animation
            currentMood = Mathf.Clamp(currentMood += currentEvent.moodEffect, 0, 100);
            UIController.UpdateMoodText();

            animationController.SetTrigger("CardsOut");
        }
    }

    public void ButtonYesPressed()
    {
        if (animationController.GetCurrentAnimatorStateInfo(0).IsName("Cards Out Idle") || animationController.GetCurrentAnimatorStateInfo(0).IsName("Cards In Idle"))
        {
            //TODO Updated to relate to yes or no button press, currently used as debug only
            //Update stats text, using animations and setting the new value after animation is complete
            UIController.StartCoroutine("AnimateCashText", currentCash + currentEvent.moneyEffect / 2);
            UIController.StartCoroutine("AnimateSavingsText", Mathf.Clamp(currentSavings + currentEvent.moneyEffect / 2, 0, 100000));

            //Update mood value and mood text animation
            currentMood = Mathf.Clamp(currentMood += currentEvent.moodEffect, 0, 100);
            UIController.UpdateMoodText();

            animationController.SetTrigger("CardsOut");
        }
    }

    public void NextDay()
    {
        //40 interactive days total, with random non-interactive events mixed throughout

        currentDay++;

        //TODO Check if game finished here

        currentDate = currentDate.AddDays(UnityEngine.Random.Range(3, 6));

        //Ensures two identical events don't trigger in a row
        lastEvent = currentEvent;
        do
        {
            currentEvent = randomEvents[UnityEngine.Random.Range(0, randomEvents.Count)];
        } while (currentEvent == lastEvent);

        //TODO Maybe switch to delegates for day update
        UIController.UpdateDateText();
        UIController.UpdateEventInformation();

        //TODO Update income and expenses each time new month entered on specific dates

        animationController.SetTrigger("CardsIn");
    }

    private void SetUpStats()
    {
        //Game stats are randomised each time to simulate real life uncertainty
        //TODO Tweak amounts to balance gameplay when events added to game
        currentCash = (int)Mathf.Round((UnityEngine.Random.Range(150, 400)) / 10) * 10;
        currentSavings = (int)Mathf.Round((UnityEngine.Random.Range(0, 250)) / 10) * 10;
        currentMood = UnityEngine.Random.Range(30, 70);
    }
}
