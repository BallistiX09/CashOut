using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject currentPanel;
    private AudioSource buttonClickAudioSource;
    [SerializeField] private GameObject optionsPanel, aboutPanel;

    private void Start()
    {
        //Sets the target game framerate
        Application.targetFrameRate = 45;

        buttonClickAudioSource = GetComponent<AudioSource>();
    }

    public void ButtonPlayPressed()
    {
        buttonClickAudioSource.Play();

        //Loads the game scene when the play button is pressed
        SceneManager.LoadScene("Game");
    }

    public void ButtonOptionsPressed()
    {
        //Stops function triggering if another panel animation is in progress
        if(currentPanel == null)
        {
            buttonClickAudioSource.Play();

            //Keeps track of currently selected panel
            currentPanel = optionsPanel;
            optionsPanel.SetActive(true);

            //Starts panel slide in animation
            optionsPanel.GetComponent<Animator>().SetTrigger("PanelIn");

            //TODO Set up toggles in options panel
        }
    }

    public void ButtonAboutPressed()
    {
        if (currentPanel == null)
        {
            buttonClickAudioSource.Play();

            currentPanel = aboutPanel;
            aboutPanel.SetActive(true);
            aboutPanel.GetComponent<Animator>().SetTrigger("PanelIn");
        }
    }

    public void OptionsClosePressed()
    {
        //Triggers the panel slide out animation
        optionsPanel.GetComponent<Animator>().SetTrigger("PanelOut");
        buttonClickAudioSource.Play();
    }

    public void AboutClosePressed()
    {
        aboutPanel.GetComponent<Animator>().SetTrigger("PanelOut");
        buttonClickAudioSource.Play();
    }

    public void DisableCurrentPanel()
    {
        if(currentPanel == optionsPanel)
        {
            //Resets the panel position, disables the panel, and clears the current panel information
            optionsPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            optionsPanel.SetActive(false);
            currentPanel = null;
        }

        if (currentPanel == aboutPanel)
        {
            aboutPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            aboutPanel.SetActive(false);
            currentPanel = null;
        }
    }
}