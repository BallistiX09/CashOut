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
        dateText.text = GenerateDateText();
    }

    private string GenerateDateText()
    {
        return " <b>Day " + GameController.currentDay.ToString() + "/30</b> | " + GameController.currentDate.ToString("MMMM d, yyyy");
    }
}
