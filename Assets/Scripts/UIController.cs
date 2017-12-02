using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //References to game UI elements in Unity editor
    [SerializeField] private Text cashAmountText, moodAmountText, studyAmountText;
    [SerializeField] private Text dateText;
    [SerializeField] private Text eventTitleText;
    [SerializeField] private Image eventImage;
    [SerializeField] private Sprite[] eventImageTypes;
    [SerializeField] private Text eventDescriptionText;
    [SerializeField] private Text leftChoiceButtonText, rightChoiceButtonText;
    [SerializeField] private Text endMoneyText, endMoodText, endStudyText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Animator statCashTitleAnim, statCashAmountAnim;
    [SerializeField] private Animator statMoodTitleAnim, statMoodAmountAnim;
    [SerializeField] private Animator statStudyTitleAnim, statStudyAmountAnim;

    //Animate the text field incrementing or decrementing to the new value
    //Using an IEnumerator/Coroutine to allow the game to run without waiting on the loop to finish
    public IEnumerator AnimateCashText(int targetCash)
    {
        //Set the starting text amount before animation begins
        int startCash = GameController.currentCash;

        //Set text colour animation depending on if it's a positive/negative effect
        if (targetCash > startCash)
        {
            statCashTitleAnim.SetTrigger("Positive");
            statCashAmountAnim.SetTrigger("Positive");
        }
        else if (targetCash < startCash)
        {
            statCashTitleAnim.SetTrigger("Negative");
            statCashAmountAnim.SetTrigger("Negative");
        }

        //Loop to gradually increment/decrement the current cash value, interpolating between starting and new target value
        for (float timer = 0; timer < 0.5f; timer += Time.deltaTime)
        {
            float progress = timer / 0.5f;
            GameController.currentCash = (int)Mathf.Lerp(startCash, targetCash, progress);

            UpdateCashText();
            yield return null;
        }

        //Manually set the cash value to the original target value, as Lerp doesn't always reach the maximum amount accurately
        GameController.currentCash = targetCash;
        UpdateCashText();
    }

    public IEnumerator AnimateStudyText(int targetHours)
    {
        //Set the starting text amount before animation begins
        int startHours = GameController.currentStudyHours;

        //Set text colour animation depending on if it's a positive/negative effect
        if (targetHours > startHours)
        {
            statStudyTitleAnim.SetTrigger("Positive");
            statStudyAmountAnim.SetTrigger("Positive");
        }
        else if (targetHours < startHours)
        {
            statStudyTitleAnim.SetTrigger("Negative");
            statStudyAmountAnim.SetTrigger("Negative");
        }

        //Loop to gradually increment/decrement the current study hours value, interpolating between starting and new target value
        for (float timer = 0; timer < 0.5f; timer += Time.deltaTime)
        {
            float progress = timer / 0.5f;
            GameController.currentStudyHours = (int)Mathf.Lerp(startHours, targetHours, progress);

            UpdateStudyText();
            yield return null;
        }

        //Manually set the cash value to the original target value, as Lerp doesn't always reach the maximum amount accurately
        GameController.currentStudyHours = targetHours;
        UpdateStudyText();
    }

    //Updates the stats text fields with current game stats information (no animations or delays)
    public void UpdateAllStatsText()
    {
        cashAmountText.text = "£" + GameController.currentCash.ToString();
        moodAmountText.text = GetMoodDescription();
        studyAmountText.text = GameController.currentStudyHours.ToString();
    }

    //Used to update the cash text only without affecting the mood text
    public void UpdateCashText()
    {
        cashAmountText.text = "£" + GameController.currentCash.ToString();
    }

    //Used to update the mood text only without affecting the other animated stats
    public void UpdateMoodText(int moodEffect)
    {
        //Animate text colour depending on positive/negative effect
        if (moodEffect > 0)
        {
            statMoodTitleAnim.SetTrigger("Positive");
            statMoodAmountAnim.SetTrigger("Positive");
        }
        else if (moodEffect < 0)
        {
            statMoodTitleAnim.SetTrigger("Negative");
            statMoodAmountAnim.SetTrigger("Negative");
        }

        moodAmountText.text = GetMoodDescription();
    }

    public void UpdateStudyText()
    {
        studyAmountText.text = GameController.currentStudyHours.ToString();
    }

    //Updates the date text field with current game date information
    public void UpdateDateText()
    {
        dateText.text = " <b>Decision " + (GameController.currentDay + 1).ToString() + "/30</b> | " + GameController.currentDate.ToString("MMMM d, yyyy");
    }

    //Updates the event text fields with current event stats information
    public void UpdateEventInformation()
    {
        eventTitleText.text = GameController.currentEvent.title;
        eventDescriptionText.text = GameController.currentEvent.description;
        
        switch (GameController.currentEvent.image)
        {
            case RandomEvent.imageType.TRANSPORT:
                eventImage.sprite = eventImageTypes[0];
                break;
            case RandomEvent.imageType.ENTERTAINMENT:
                eventImage.sprite = eventImageTypes[1];
                break;
            case RandomEvent.imageType.FINANCIAL:
                eventImage.sprite = eventImageTypes[2];
                break;
            case RandomEvent.imageType.WORK:
                eventImage.sprite = eventImageTypes[3];
                break;
            case RandomEvent.imageType.GAMBLING:
                eventImage.sprite = eventImageTypes[4];
                break;
            case RandomEvent.imageType.SOCIAL:
                eventImage.sprite = eventImageTypes[5];
                break;
            case RandomEvent.imageType.TECHNOLOGY:
                eventImage.sprite = eventImageTypes[6];
                break;
            case RandomEvent.imageType.GAMING:
                eventImage.sprite = eventImageTypes[7];
                break;
            case RandomEvent.imageType.SHOPPING:
                eventImage.sprite = eventImageTypes[8];
                break;
            case RandomEvent.imageType.FOOD:
                eventImage.sprite = eventImageTypes[9];
                break;
            case RandomEvent.imageType.EDUCATION:
                eventImage.sprite = eventImageTypes[10];
                break;
        }
    }

    //Generates a user-friendly text description based on the current mood value
    private string GetMoodDescription()
    {
        if (GameController.currentMood >= -100 && GameController.currentMood <= -75)
        {
            return "Miserable";
        }
        else if (GameController.currentMood > -75 && GameController.currentMood <= -40)
        {
            return "Unhappy";
        }
        else if (GameController.currentMood > -40 && GameController.currentMood < 40)
        {
            return "Neutral";
        }
        else if (GameController.currentMood >= 40 && GameController.currentMood < 75)
        {
            return "Happy";
        }
        else if (GameController.currentMood >= 75 && GameController.currentMood <= 100)
        {
            return "Cheerful";
        }
        else
        {
            return "Neutral"; //Should not reach this point, only used if mood goes out of scale
        }
    }

    public void EndGame()
    {
        gameOverPanel.SetActive(true);
        endMoneyText.text = "Money: " + GameController.currentCash.ToString();
        endMoodText.text = "Mood: " + GetMoodDescription();
        endStudyText.text = "Study Hours: " + GameController.currentStudyHours.ToString();
    }
}
