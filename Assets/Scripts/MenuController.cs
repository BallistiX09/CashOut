using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        //Sets the target game framerate
        Application.targetFrameRate = 45;
    }

    public void ButtonPlayPressed()
    {
        //Loads the game scene when the play button is pressed
        SceneManager.LoadScene("Game");
    }
}
