using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //References to game UI elements in Unity editor
    [SerializeField] private Text cashAmountText, moodAmountText, savingsAmountText;
    [SerializeField] private Text dateText;
    [SerializeField] private Text eventTitleText;
    [SerializeField] private Image eventImage;
    [SerializeField] private Sprite[] eventImageTypes;
    [SerializeField] private Text eventDescriptionText;
    [SerializeField] private Text leftChoiceButtonText, rightChoiceButtonText;
    [SerializeField] private Animator statCashTitleAnim, statCashAmountAnim;
    [SerializeField] private Animator statMoodTitleAnim, statMoodAmountAnim;
    [SerializeField] private Animator statSavingsTitleAnim, statSavingsAmountAnim;

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

            UpdateCashAndSavingsText();
            yield return null;
        }

        //Manually set the cash value to the original target value, as Lerp doesn't always reach the maximum amount accurately
        GameController.currentCash = targetCash;
        UpdateCashAndSavingsText();
    }

    //Identical to cash animation function above
    public IEnumerator AnimateSavingsText(int targetSavings)
    {
        int startSavings = GameController.currentSavings;

        if (targetSavings > startSavings)
        {
            statSavingsTitleAnim.SetTrigger("Positive");
            statSavingsAmountAnim.SetTrigger("Positive");
        }
        else if (targetSavings < startSavings)
        {
            statSavingsTitleAnim.SetTrigger("Negative");
            statSavingsAmountAnim.SetTrigger("Negative");
        }

        for (float timer = 0; timer < 0.5f; timer += Time.deltaTime)
        {
            float progress = timer / 0.5f;
            GameController.currentSavings = (int)Mathf.Lerp(startSavings, targetSavings, progress);
            UpdateCashAndSavingsText();
            yield return null;
        }
        GameController.currentSavings = targetSavings;
        UpdateCashAndSavingsText();
    }

    //Updates the stats text fields with current game stats information (no animations or delays)
    public void UpdateAllStatsText()
    {
        cashAmountText.text = "£" + GameController.currentCash.ToString();
        savingsAmountText.text = "£" + GameController.currentSavings.ToString();
        moodAmountText.text = GetMoodDescription();
    }

    //Used to update the cash and savings text only without affecting the mood text
    public void UpdateCashAndSavingsText()
    {
        cashAmountText.text = "£" + GameController.currentCash.ToString();
        savingsAmountText.text = "£" + GameController.currentSavings.ToString();
    }

    //Used to update the mood text only without affecting the other animated stats
    public void UpdateMoodText()
    {
        //Animate text colour depending on positive/negative effect
        if (GameController.currentEvent.moodEffect > 0)
        {
            statMoodTitleAnim.SetTrigger("Positive");
            statMoodAmountAnim.SetTrigger("Positive");
        }
        else if (GameController.currentEvent.moodEffect < 0)
        {
            statMoodTitleAnim.SetTrigger("Negative");
            statMoodAmountAnim.SetTrigger("Negative");
        }

        moodAmountText.text = GetMoodDescription();
    }

    //Updates the date text field with current game date information
    public void UpdateDateText()
    {
        dateText.text = " <b>Decision " + GameController.currentDay.ToString() + "/30</b> | " + GameController.currentDate.ToString("MMMM d, yyyy");
    }

    //Updates the event text fields with current event stats information
    public void UpdateEventInformation()
    {
        eventTitleText.text = GameController.currentEvent.title;
        eventDescriptionText.text = GameController.currentEvent.description;

        switch (GameController.currentEvent.image)
        {
            case RandomEvent.imageType.TYPE_1:
                eventImage.sprite = eventImageTypes[0];
                break;
            case RandomEvent.imageType.TYPE_2:
                eventImage.sprite = eventImageTypes[0];
                break;
            case RandomEvent.imageType.TYPE_3:
                eventImage.sprite = eventImageTypes[0];
                break;
            case RandomEvent.imageType.TYPE_4:
                eventImage.sprite = eventImageTypes[0];
                break;
            case RandomEvent.imageType.TYPE_5:
                eventImage.sprite = eventImageTypes[0];
                break;
            case RandomEvent.imageType.TYPE_6:
                eventImage.sprite = eventImageTypes[0];
                break;
        }
    }

    //Generates a user-friendly text description based on the current mood value
    private string GetMoodDescription()
    {
        if (GameController.currentMood >= 0 && GameController.currentMood < 20)
        {
            return "Miserable";
        }
        else if (GameController.currentMood >= 20 && GameController.currentMood < 40)
        {
            return "Unhappy";
        }
        else if (GameController.currentMood >= 40 && GameController.currentMood < 60)
        {
            return "Neutral";
        }
        else if (GameController.currentMood >= 60 && GameController.currentMood < 80)
        {
            return "Happy";
        }
        else if (GameController.currentMood >= 80 && GameController.currentMood < 100)
        {
            return "Cheerful";
        }
        else
        {
            return "Neutral"; //Should not reach this point, only used if mood goes out of scale
        }
    }
}
