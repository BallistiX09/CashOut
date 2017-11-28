using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Globally-accessible variables keeping track of current game state
    public static int currentCash = 0;
    public static int currentMood = 0; //Mood based on int value from -100 to 100, with 0 being neutral
    public static int currentStudyHours = 0;
    public static int currentDay = 0;
    private bool soundsEnabled = true, musicEnabled = true;
    public static DateTime currentDate = System.DateTime.Now;
    public static RandomEvent currentEvent;
    private UIController UIController;
    private AudioSource buttonClickAudioSource;
    [SerializeField] private Animator animationController;

    private List<RandomEvent> currentEvents = new List<RandomEvent>(); //Event pool for use in gameplay, uses a mixture of mandatory and random events
    [SerializeField] private List<RandomEvent> mandatoryEvents = new List<RandomEvent>(); //Events which will always happen with every playthrough
    [SerializeField] private List<RandomEvent> randomEvents = new List<RandomEvent>(); //Random events which may or may not be in a particular game
    [SerializeField] private List<UncontrolledEvent> uncontrolledEvents = new List<UncontrolledEvent>(); //Events which the user has no decision over and can only accept

    private void Start()
    {
        //Sets the target game framerate
        Application.targetFrameRate = 45;

        currentCash = 0;
        currentMood = 0;
        currentStudyHours = 0;
        currentDay = 0;

        //Finds a reference to UI Controller and button click audio source
        UIController = FindObjectOfType<UIController>();
        buttonClickAudioSource = GetComponent<AudioSource>();

        //Set up variables for first startup
        SetUpStats();

        //Sets up the pool of events to be used in-game
        BuildEventsList();

        //Select a random event from the list of events
        currentEvent = currentEvents[UnityEngine.Random.Range(0, currentEvents.Count)];

        //Update game UI with startup information
        UIController.UpdateDateText();
        UIController.UpdateEventInformation();
        UIController.UpdateAllStatsText();
    }

    private void BuildEventsList()
    {
        //Randomly removes 12 items from the random events list
        //Will bring list down to 23 items, which combined with the 7 mandatory events will bring the list to 30
        for(int i = 0; i < 12; i++)
        {
            randomEvents.RemoveAt(UnityEngine.Random.Range(0, randomEvents.Count));
        }

        //Copies the mandatory events into a temporary list
        List<RandomEvent> tempEventsList = mandatoryEvents;

        //Copies the remaining random events into the temporary list
        for (int i = 0; i < randomEvents.Count; i++)
        {
            tempEventsList.Add(randomEvents[i]);
        }

        //Creates new random number generator
        System.Random rnd = new System.Random();

        //Randomises the order of the temporary list with mandatory and random events combined, then copies the events into the current events list
        currentEvents = tempEventsList.OrderBy(item => rnd.Next()).ToList();
    }

    private void ButtonNoPressed()
    {
        if (animationController.GetCurrentAnimatorStateInfo(0).IsName("Cards Out Idle") || animationController.GetCurrentAnimatorStateInfo(0).IsName("Cards In Idle"))
        {
            PlayButtonClickSound();

            //Update stats text, using animations and setting the new value after animation is complete
            if (currentEvent.noMoneyInstantEffect != 0)
            {
                UIController.StartCoroutine("AnimateCashText", currentCash + currentEvent.noMoneyInstantEffect);
            }

            //Update mood value and mood text animation
            if (currentEvent.noMoodEffect != 0)
            {
                currentMood = Mathf.Clamp(currentMood += currentEvent.noMoodEffect, -100, 100);
                UIController.UpdateMoodText(currentEvent.noMoodEffect);
            }

            if (currentEvent.noStudyEffect != 0)
            {
                UIController.StartCoroutine("AnimateStudyText", currentStudyHours + currentEvent.noStudyEffect);
            }            

            animationController.SetTrigger("CardsOut");
        }
    }

    private void ButtonYesPressed()
    {
        if (animationController.GetCurrentAnimatorStateInfo(0).IsName("Cards Out Idle") || animationController.GetCurrentAnimatorStateInfo(0).IsName("Cards In Idle"))
        {
            PlayButtonClickSound();

            //Update stats text, using animations and setting the new value after animation is complete
            if (currentEvent.yesMoneyInstantEffect != 0)
            {
                UIController.StartCoroutine("AnimateCashText", currentCash + currentEvent.yesMoneyInstantEffect);
            }

            //Update mood value and mood text animation
            if (currentEvent.yesMoodEffect != 0)
            {
                currentMood = Mathf.Clamp(currentMood += currentEvent.yesMoodEffect, -100, 100);
                UIController.UpdateMoodText(currentEvent.yesMoodEffect);
            }

            if (currentEvent.yesStudyEffect != 0)
            {
                UIController.StartCoroutine("AnimateStudyText", currentStudyHours + currentEvent.yesStudyEffect);
            }

            animationController.SetTrigger("CardsOut");
        }
    }

    public void NextDay()
    {
        //30 interactive days total, with random non-interactive events mixed throughout and 5 trivia questions

        currentDay++;

        //TODO Check if game finished here
        if(currentDay == 30)
        {
            UIController.EndGame();
        }

        currentDate = currentDate.AddDays(UnityEngine.Random.Range(3, 6));

        currentEvent = currentEvents[currentDay];

        /*
        //Ensures two identical events don't trigger in a row
        //Currently unused due to new events system
        lastEvent = currentEvent;
        do
        {
            currentEvent = randomEvents[UnityEngine.Random.Range(0, randomEvents.Count)];
        } while (currentEvent == lastEvent);*/

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
        currentMood = UnityEngine.Random.Range(-50, 50);
        currentStudyHours = UnityEngine.Random.Range(20, 28);
    }

    private void PlayButtonClickSound()
    {
        //Only plays sound if enabled in player options
        if (soundsEnabled)
        {
            buttonClickAudioSource.Play();
        }
    }

    public void MenuButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
