using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimation : MonoBehaviour {

    [SerializeField] private GameController gameController;

    public void TriggerNextDay()
    {
        gameController.NextDay();
    }
}
