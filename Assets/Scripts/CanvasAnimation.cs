using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimation : MonoBehaviour {

    [SerializeField] private GameController gameController;
    [SerializeField] private UIController uiController;

    public void TriggerNextDay()
    {
        if ((GameController.currentDay == 9 || GameController.currentDay == 19) && !GameController.showingSummary)
        {
            uiController.ShowSummary();
        }
        else
        {
            gameController.NextDay();
        }
    }
}