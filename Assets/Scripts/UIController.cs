using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    //References to game UI elements in Unity editor
    [SerializeField] private Text cashAmountText, moodAmountText, savingsAmountText;
    [SerializeField] private Text dateText;
    [SerializeField] private Text eventTitleText;
    [SerializeField] private Image eventImage;
    [SerializeField] private Sprite[] eventImageTypes;
    [SerializeField] private Text eventDescriptionText;
    [SerializeField] private Text leftChoiceButtonText, rightChoiceButtonText;

    public void UpdateStatsText()
    {
        //Updates the stats text fields with current game stats information
        cashAmountText.text = "£" + GameController.currentCash.ToString();
        savingsAmountText.text = "£" + GameController.currentSavings.ToString();
        moodAmountText.text = GetMoodDescription();
    }

    public void UpdateDateText()
    {
        //Updates the date text field with current game date information
        //TODO Switch to 30 days instead of 12
        dateText.text = " <b>Decision " + GameController.currentDay.ToString() + "/12</b> | " + GameController.currentDate.ToString("MMMM d, yyyy");
    }

    public void UpdateEventInformation()
    {
        //Updates the event text fields with current event stats information
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

    private string GetMoodDescription()
    {
        //Generates a user-friendly text description based on the current mood value
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
