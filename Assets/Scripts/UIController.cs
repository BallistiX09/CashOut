using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //References to game UI elements in Unity editor
    private GameController gameController;
    private PlayerPerformance playerPerformance;
    private DatabaseController databaseController;

    [SerializeField] private Text cashAmountText, moodAmountText, studyAmountText;

    [SerializeField] private Text dateText;
    [SerializeField] private Text eventTitleText;
    [SerializeField] private Image eventImage;
    [SerializeField] private Sprite[] eventImageTypes;
    [SerializeField] private Text eventDescriptionText;
    [SerializeField] private Text leftChoiceButtonText, rightChoiceButtonText;

    [SerializeField] private Text endMoneyText, endMoodText, endStudyText;
    [SerializeField] private Text endMoneySpentText, endDebtRemainingText, endDaysInOverdraftText;

    [SerializeField] private Text incomesText, debitsText;
    [SerializeField] private Text summaryDetailsText, endSummaryDetailsText;
    [SerializeField] private GameObject summaryTitle, summaryDetails, endSummaryDetails;

    [SerializeField] private GameObject gameOverPanel, debitsPanel, backgroundPanel;
    [SerializeField] private GameObject endMoneySpent, endDebtRemaining, endDaysInOverdraft;
    [SerializeField] private GameObject buttonNo, buttonYes, endButtonDetails;

    [SerializeField] private Animator statCashTitleAnim, statCashAmountAnim;
    [SerializeField] private Animator statMoodTitleAnim, statMoodAmountAnim;
    [SerializeField] private Animator statStudyTitleAnim, statStudyAmountAnim;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        databaseController = FindObjectOfType<DatabaseController>();
        playerPerformance = FindObjectOfType<PlayerPerformance>();
    }

    public string GenerateSummaryText()
    {
        return "Earned from student loan: £" + playerPerformance.earnedFromStudentLoan.ToString() + "\n" +
            "Earned from work: £" + playerPerformance.earnedFromWork.ToString() + "\n" +
            "Earned from overtime: £" + playerPerformance.earnedFromOvertime.ToString() + "\n\n" +
            "Spent on transport: £" + playerPerformance.spentOnTransport.ToString() + "\n" +
            "Spent on entertainment: £" + playerPerformance.spentOnEntertainment.ToString() + "\n" +
            "Spent on financial: £" + playerPerformance.spentOnFinancial.ToString() + "\n" +
            "Spent on food: £" + playerPerformance.spentOnFood.ToString() + "\n" +
            "Spent on gambling: £" + playerPerformance.spentOnGambling.ToString() + "\n" +
            "Spent on shopping: £" + playerPerformance.spentOnShopping.ToString() + "\n" +
            "Spent on social: £" + playerPerformance.spentOnSocial.ToString() + "\n" +
            "Spent on technology: £" + playerPerformance.spentOnTechnology.ToString() + "\n" +
            "Spent on gaming: £" + playerPerformance.spentOnGaming.ToString() + "\n" +
            "Spent on education: £" + playerPerformance.spentOnEducation.ToString() + "\n" +
            "Spent on rent: £" + playerPerformance.spentOnRent.ToString() + "\n\n" +
            "Debt left on credit card: £" + playerPerformance.debtLeftOnCC().ToString() + "\n" +
            "Debt left on payday loan: £" + playerPerformance.debtLeftOnPDL().ToString() + "\n" +
            "Debt left on LightHome purchase: £" + playerPerformance.debtLeftOnLH().ToString() + "\n" +
            "Debt left on loan from friend: £" + playerPerformance.debtLeftOnFL().ToString() + "\n\n" +
            "Days spent in overdraft: " + playerPerformance.daysSpentInOverdraft.ToString() + "\n" +
            "Days spent unhappy: " + playerPerformance.daysSpentUnhappy.ToString() + "\n" +
            "Days spent <30 study hours: " + playerPerformance.daysSpentUnder30StudyHours.ToString();
    }

    public void ShowSummary()
    {
        GameController.showingSummary = true;

        dateText.gameObject.SetActive(false);
        eventTitleText.gameObject.SetActive(false);
        eventImage.transform.parent.gameObject.SetActive(false);
        eventDescriptionText.gameObject.SetActive(false);

        summaryTitle.gameObject.SetActive(true);
        summaryDetails.gameObject.SetActive(true);

        buttonNo.gameObject.SetActive(false);
        buttonYes.GetComponentInChildren<Text>().text = "DONE";
        summaryDetailsText.text = GenerateSummaryText();

        gameController.animationController.SetTrigger("CardsIn");
    }

    public void HideSummary()
    {
        GameController.showingSummary = false;

        dateText.gameObject.SetActive(true);
        eventTitleText.gameObject.SetActive(true);
        eventImage.transform.parent.gameObject.SetActive(true);
        eventDescriptionText.gameObject.SetActive(true);

        summaryTitle.gameObject.SetActive(false);
        summaryDetails.gameObject.SetActive(false);

        buttonNo.gameObject.SetActive(true);
        buttonYes.GetComponentInChildren<Text>().text = "YES";
    }

    public void ShowUncontrolledEvent()
    {
        buttonNo.gameObject.SetActive(false);
        buttonYes.GetComponentInChildren<Text>().text = "DONE";
        gameController.animationController.SetTrigger("CardsIn");
    }

    public void HideUncontrolledEvent()
    {
        buttonNo.gameObject.SetActive(true);
        buttonYes.GetComponentInChildren<Text>().text = "YES";
    }

    public void ShowFinalSummaryDetails()
    {
        endMoneyText.gameObject.SetActive(false);
        endMoodText.gameObject.SetActive(false);
        endStudyText.gameObject.SetActive(false);
        endMoneySpent.gameObject.SetActive(false);
        endMoneySpentText.gameObject.SetActive(false);
        endDebtRemaining.gameObject.SetActive(false);
        endDebtRemainingText.gameObject.SetActive(false);
        endDaysInOverdraft.gameObject.SetActive(false);
        endDaysInOverdraftText.gameObject.SetActive(false);
        endButtonDetails.gameObject.SetActive(false);
        endSummaryDetails.gameObject.SetActive(true);
        endSummaryDetailsText.text = GenerateSummaryText();
    }

    //Animate the text field incrementing or decrementing to the new value, using an IEnumerator/Coroutine to allow the game to run without waiting on the loop to finish
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

    public void UpdateUncontrolledEventInformation()
    {
        eventTitleText.text = GameController.currentUncontrolledEvent.title;
        eventDescriptionText.text = GameController.currentUncontrolledEvent.description;

        switch (GameController.currentUncontrolledEvent.category)
        {
            case UncontrolledEvent.Category.UNCONTROLLED_GOOD:
                eventImage.sprite = eventImageTypes[12];
                break;
            case UncontrolledEvent.Category.UNCONTROLLED_BAD:
                eventImage.sprite = eventImageTypes[13];
                break;
        }

        ShowUncontrolledEvent();
    }

    //Updates the event text fields with current event stats information
    public void UpdateEventInformation()
    {
        eventTitleText.text = GameController.currentEvent.title;
        eventDescriptionText.text = GameController.currentEvent.description;
        
        switch (GameController.currentEvent.category)
        {
            case RandomEvent.Category.TRANSPORT:
                eventImage.sprite = eventImageTypes[0];
                break;
            case RandomEvent.Category.ENTERTAINMENT:
                eventImage.sprite = eventImageTypes[1];
                break;
            case RandomEvent.Category.FINANCIAL:
                eventImage.sprite = eventImageTypes[2];
                break;
            case RandomEvent.Category.WORK:
                eventImage.sprite = eventImageTypes[3];
                break;
            case RandomEvent.Category.GAMBLING:
                eventImage.sprite = eventImageTypes[4];
                break;
            case RandomEvent.Category.SOCIAL:
                eventImage.sprite = eventImageTypes[5];
                break;
            case RandomEvent.Category.TECHNOLOGY:
                eventImage.sprite = eventImageTypes[6];
                break;
            case RandomEvent.Category.GAMING:
                eventImage.sprite = eventImageTypes[7];
                break;
            case RandomEvent.Category.SHOPPING:
                eventImage.sprite = eventImageTypes[8];
                break;
            case RandomEvent.Category.FOOD:
                eventImage.sprite = eventImageTypes[9];
                break;
            case RandomEvent.Category.EDUCATION:
                eventImage.sprite = eventImageTypes[10];
                break;
            case RandomEvent.Category.RENT:
                eventImage.sprite = eventImageTypes[11];
                break;
            default:
                eventImage.sprite = eventImageTypes[2];
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

    public void ShowDebits()
    {
        debitsPanel.SetActive(true);

        incomesText.text = "";
        debitsText.text = "";
        
        foreach (Income income in gameController.currentIncome)
        {
            incomesText.text += income.name + ": £" + income.monthlyAmount + "\n\n";
        }

        foreach (Debit debit in gameController.currentDebits)
        {
            debitsText.text += debit.name + ": -£" + debit.monthlyCost.ToString().Substring(1, debit.monthlyCost.ToString().Length-1) + "\n\n";
        }

        incomesText.text = incomesText.text.Substring(0, incomesText.text.Length - 2);
        debitsText.text = debitsText.text.Substring(0, debitsText.text.Length - 2);
    }

    public void HideDebits()
    {
        debitsPanel.SetActive(false);
    }

    public string GetHighestSpendingCategory()
    {
        int highestCategory = -1;
        int highestAmount = 0;
        int[] categories = new int[] {playerPerformance.spentOnEducation, playerPerformance.spentOnEntertainment, playerPerformance.spentOnFinancial, playerPerformance.spentOnFood, playerPerformance.spentOnGambling,
            playerPerformance.spentOnGaming, playerPerformance.spentOnRent, playerPerformance.spentOnShopping, playerPerformance.spentOnSocial, playerPerformance.spentOnTechnology, playerPerformance.spentOnTransport };

        for (int i = 0; i < categories.Length; i++)
        {
            if (categories[i] > highestAmount)
            {
                highestCategory = i;
            }
        }

        switch (highestCategory)
        {
            case 0:
                return "Education";
            case 1:
                return "Entertainment";
            case 2:
                return "Financial";
            case 3:
                return "Food";
            case 4:
                return "Gambling";
            case 5:
                return "Gaming";
            case 6:
                return "Rent";
            case 7:
                return "Shopping";
            case 8:
                return "Social";
            case 9:
                return "Technology";
            case 10:
                return "Transport";
            default:
                Debug.LogWarning("Category not found");
                return "Financial";
        }
    }

    public int GetRemainingDebt()
    {
        return playerPerformance.debtLeftOnCC() + playerPerformance.debtLeftOnFL() + playerPerformance.debtLeftOnLH() + playerPerformance.debtLeftOnPDL();
    }

    public void EndGame()
    {
        gameOverPanel.SetActive(true);

        if (GameController.currentCash < 0)
        {
            endMoneyText.text = "<b><color=#4eff95ff>Remaining Money: </color></b>-£" + GameController.currentCash.ToString().Substring(1, GameController.currentCash.ToString().Length - 1);
        }
        else
        {
            endMoneyText.text = "<b><color=#4eff95ff>Remaining Money: </color></b>£" + GameController.currentCash.ToString();
        }
        endMoodText.text = "<b><color=#4eff95ff>Final Mood: </color></b>" + GetMoodDescription();
        endStudyText.text = "<b><color=#4eff95ff>Final Study Hours: </color></b>" + GameController.currentStudyHours.ToString();

        endMoneySpentText.text = GetHighestSpendingCategory();
        endDebtRemainingText.text = "£" + Math.Abs(GetRemainingDebt()).ToString();
        endDaysInOverdraftText.text = playerPerformance.daysSpentInOverdraft.ToString() + " days";

        databaseController.SubmitResultsToDatabase();
    }
}