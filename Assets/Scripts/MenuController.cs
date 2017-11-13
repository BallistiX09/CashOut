using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject currentPanel;
    private AudioSource buttonClickAudioSource;
    private bool soundsEnabled = true, musicEnabled = true;
    private const string PREFS_SOUND_KEY = "Sound_Enabled", PREFS_MUSIC_KEY = "Music_Enabled";
    [SerializeField] private GameObject optionsPanel, aboutPanel;
    [SerializeField] private Toggle soundToggle, musicToggle;

    private void Start()
    {
        //Sets the target game framerate
        Application.targetFrameRate = 45;

        //Links the button click sound audio source
        buttonClickAudioSource = GetComponent<AudioSource>();

        //Loads saved user options
        LoadSavedOptions();
    }

    public void ButtonPlayPressed()
    {
        PlayButtonClickSound();

        //Loads the game scene when the play button is pressed
        SceneManager.LoadScene("Game");
    }

    public void ButtonOptionsPressed()
    {
        //Stops function triggering if another panel animation is in progress
        if (currentPanel == null)
        {
            PlayButtonClickSound();

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
            PlayButtonClickSound();

            currentPanel = aboutPanel;
            aboutPanel.SetActive(true);
            aboutPanel.GetComponent<Animator>().SetTrigger("PanelIn");
        }
    }

    public void OptionsClosePressed()
    {
        //Triggers the panel slide out animation
        optionsPanel.GetComponent<Animator>().SetTrigger("PanelOut");

        PlayButtonClickSound();
    }

    public void AboutClosePressed()
    {
        aboutPanel.GetComponent<Animator>().SetTrigger("PanelOut");

        PlayButtonClickSound();
    }

    public void DisableCurrentPanel()
    {
        if (currentPanel == optionsPanel)
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

    private void LoadSavedOptions()
    {
        //Checks that this option has been saved previously before attempting to load
        if (PlayerPrefs.HasKey(PREFS_SOUND_KEY))
        {
            //Checks the int value of the key using a constant variable key name to prevent mistakes
            //PlayerPrefs doesn't support bool values, so an int set to 1 or 0 is used instead
            if (PlayerPrefs.GetInt(PREFS_SOUND_KEY) == 1)
            {
                //Sets the local sound enabled variable, and updates the options toggle state in the UI
                soundsEnabled = true;
                soundToggle.isOn = true;
            }
            else if (PlayerPrefs.GetInt(PREFS_SOUND_KEY) == 0)
            {
                soundsEnabled = false;
                soundToggle.isOn = false;
            }
        }

        //Works exactly the same was as above
        if (PlayerPrefs.HasKey(PREFS_MUSIC_KEY))
        {
            if (PlayerPrefs.GetInt(PREFS_MUSIC_KEY) == 1)
            {
                musicEnabled = true;
                musicToggle.isOn = true;
            }
            else if (PlayerPrefs.GetInt(PREFS_MUSIC_KEY) == 0)
            {
                musicEnabled = false;
                musicToggle.isOn = false;
            }
        }
    }

    //Runs AFTER the value has changed, not beforehand in the way buttons do
    public void OptionsSoundTogglePressed()
    {
        //Prevents the function from running if the user isn't in the options panel
        //When the toggle state is updated, this function will run automatically when the value is changed
        //This prevents the state from being wrongly updated when options are loaded on game startup
        if (currentPanel == optionsPanel)
        {
            if (!soundsEnabled)
            {
                buttonClickAudioSource.Play();
            }

            if (soundToggle.isOn)
            {
                EnableSounds();
            }
            else
            {
                DisableSounds();
            }
        }
    }

    //Runs AFTER the value has changed, not beforehand in the way buttons do
    public void OptionsMusicTogglePressed()
    {
        if (currentPanel == optionsPanel)
        {
            PlayButtonClickSound();

            if (musicToggle.isOn)
            {
                EnableMusic();
            }
            else
            {
                DisableMusic();
            }
        }
    }

    private void EnableSounds()
    {
        //Sets the local sound enabled variable to true, and saves this option into PlayerPrefs storage
        soundsEnabled = true;
        PlayerPrefs.SetInt(PREFS_SOUND_KEY, 1);
    }

    private void DisableSounds()
    {
        soundsEnabled = false;
        PlayerPrefs.SetInt(PREFS_SOUND_KEY, 0);
    }

    private void EnableMusic()
    {
        musicEnabled = true;
        PlayerPrefs.SetInt(PREFS_MUSIC_KEY, 1);
    }

    private void DisableMusic()
    {
        musicEnabled = false;
        PlayerPrefs.SetInt(PREFS_MUSIC_KEY, 0);
    }

    private void PlayButtonClickSound()
    {
        //Only plays sound if enabled in player options
        if (soundsEnabled)
        {
            buttonClickAudioSource.Play();
        }
    }
}