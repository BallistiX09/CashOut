using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] private Text cashAmountText, moodAmountText, savingsAmountText;
    [SerializeField] private Text dateText;
    [SerializeField] private Text eventTitleText;
    [SerializeField] private Image eventImage;
    [SerializeField] private Text eventDescriptionText;
    [SerializeField] private Text leftChoiceButtonText, rightChoiceButtonText;

    private void Start()
    {
        UpdateDateText();
    }

    public void UpdateStatsText()
    {
        cashAmountText.text = "£" + GameController.cash.ToString();
        savingsAmountText.text = "£" + GameController.savings.ToString();
        moodAmountText.text = GameController.mood.ToString(); //To be updated to string descriptions of mood int
    }

    public void UpdateDateText()
    {
        dateText.text = " <b>Decision " + GameController.currentDay.ToString() + "/30</b> | " + GameController.currentDate.ToString("MMMM d, yyyy");
    }
}
