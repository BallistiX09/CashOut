using System;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static int cash = 0;
    public static int mood = 0;
    public static int savings = 0;
    public static int currentDay = 1;
    public static DateTime currentDate = System.DateTime.Now;
    private UIController UIController;

    private void Start()
    {
        UIController = FindObjectOfType<UIController>();
    }

    public void ButtonNoPressed()
    {
        NextDay();
    }

    public void ButtonYesPressed()
    {
        NextDay();
    }

    private void NextDay()
    {
        //40 interactive days total, with random non-interactive events mixed throughout

        currentDay++;
        //Check if game finished here
        currentDate = currentDate.AddDays(UnityEngine.Random.Range(3, 6));

        UIController.UpdateDateText(); //Maybe switch to delegates for day update

        //Update income and expenses each time new month entered on specific dates
    }
}
