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
        else if ((Random.Range(1, 9) == 8) && !GameController.showingSummary && !GameController.showingUncontrolledEvent)
        {
            GameController.showingUncontrolledEvent = true;
            gameController.UncontrolledEvent();
        }
        else
        {
            gameController.NextDay();
        }
    }
}