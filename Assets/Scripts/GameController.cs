using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Globally-accessible variables keeping track of current game state
    public static int currentCash = 0;
    public static int currentMood = 0; //Mood based on int value from -100 to 100, with 0 being neutral
    public static int currentStudyHours = 0;
    public static int currentDay = 0;
    public static string currentMonth = "";
    private bool soundsEnabled = true, musicEnabled = true;
    public static DateTime currentDate = System.DateTime.Now;
    public static RandomEvent currentEvent;

    public static bool carOwned = false, tvLicenceCancelled = false, payingCreditCard = false, payingPaydayLoan = false, payingLighthome = false, phoneInsurance = false, 
        betOnSports = false, playingLottery = false, buyingGameLater = false, payingFriendLoan = false, internetCancelled = false;
    public static int monthsLeftOnCC = 0, monthsLeftOnPDL = 0, monthsLeftOnLH = 0, monthsLeftOnFL = 0;

    private UIController UIController;
    private PlayerPerformance playerPerformance;
    private AudioSource buttonClickAudioSource;
    [SerializeField] private Animator animationController;

    [SerializeField] private List<Debit> possibleDebits = new List<Debit>();
    [SerializeField] private List<Income> possibleIncome = new List<Income>();
    [SerializeField] public static List<Debit> currentDebits = new List<Debit>();
    [SerializeField] public static List<Income> currentIncome = new List<Income>();

    [SerializeField] private List<RandomEvent> currentEvents = new List<RandomEvent>(); //Event pool for use in gameplay, uses a mixture of mandatory and random events
    [SerializeField] private List<RandomEvent> mandatoryEvents = new List<RandomEvent>(); //Events which will always happen with every playthrough
    [SerializeField] private List<RandomEvent> randomEvents = new List<RandomEvent>(); //Random events which may or may not be in a particular game
    [SerializeField] private List<UncontrolledEvent> uncontrolledEvents = new List<UncontrolledEvent>(); //Events which the user has no decision over and can only accept

    private void Start()
    {
        //Sets the target game framerate
        Application.targetFrameRate = 45;

        currentCash = 0;
        currentMood = 0;
        currentStudyHours = 0;
        currentDay = 0;
        currentMonth = currentDate.ToString("MMMM");

        //Finds a reference to UI Controller and button click audio source
        UIController = FindObjectOfType<UIController>();
        playerPerformance = FindObjectOfType<PlayerPerformance>();
        buttonClickAudioSource = GetComponent<AudioSource>();

        //Set up variables for first startup
        SetUpStats();
        SetUpIncome();
        SetUpDebits();

        //Sets up the pool of events to be used in-game
        BuildEventsList();

        //Select a random event from the list of events
        currentEvent = currentEvents[UnityEngine.Random.Range(0, currentEvents.Count)];

        //Update game UI with startup information
        UIController.UpdateDateText();
        UIController.UpdateEventInformation();
        UIController.UpdateAllStatsText();
    }

    private void ButtonNoPressed()
    {
        if (animationController.GetCurrentAnimatorStateInfo(0).IsName("Cards Out Idle") || animationController.GetCurrentAnimatorStateInfo(0).IsName("Cards In Idle"))
        {
            PlayButtonClickSound();

            CheckNoFollowUps();

            //Update stats text, using animations and setting the new value after animation is complete
            if (currentEvent.noMoneyInstantEffect != 0)
            {
                switch (currentEvent.category)
                {
                    case RandomEvent.Category.EDUCATION:
                        playerPerformance.spentOnEducation += currentEvent.noMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.ENTERTAINMENT:
                        playerPerformance.spentOnEntertainment += currentEvent.noMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.FINANCIAL:
                        playerPerformance.spentOnFinancial += currentEvent.noMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.FOOD:
                        playerPerformance.spentOnFood += currentEvent.noMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.GAMBLING:
                        playerPerformance.spentOnGambling += currentEvent.noMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.GAMING:
                        playerPerformance.spentOnGaming += currentEvent.noMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.SHOPPING:
                        playerPerformance.spentOnShopping += currentEvent.noMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.SOCIAL:
                        playerPerformance.spentOnSocial += currentEvent.noMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.TECHNOLOGY:
                        playerPerformance.spentOnTechnology += currentEvent.noMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.TRANSPORT:
                        playerPerformance.spentOnTransport += currentEvent.noMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.WORK:
                        playerPerformance.spentOnWork += currentEvent.noMoneyInstantEffect;
                        break;
                    default:
                        Debug.LogWarning("Category not found");
                        playerPerformance.spentOnFinancial += currentEvent.noMoneyInstantEffect;
                        break;
                }

                UIController.StartCoroutine("AnimateCashText", currentCash + currentEvent.noMoneyInstantEffect);
            }

            //Update mood value and mood text animation
            if (currentEvent.noMoodEffect != 0)
            {
                if(currentEvent.noMoodEffect > 0)
                {
                    playerPerformance.moodIncreaseActions++;
                }
                else
                {
                    playerPerformance.moodDecreaseActions++;
                }

                currentMood = Mathf.Clamp(currentMood += currentEvent.noMoodEffect, -100, 100);
                UIController.UpdateMoodText(currentEvent.noMoodEffect);
            }

            if (currentEvent.noStudyEffect != 0)
            {
                UIController.StartCoroutine("AnimateStudyText", currentStudyHours + currentEvent.noStudyEffect);
            }            

            animationController.SetTrigger("CardsOut");
        }
    }

    private void ButtonYesPressed()
    {
        if (animationController.GetCurrentAnimatorStateInfo(0).IsName("Cards Out Idle") || animationController.GetCurrentAnimatorStateInfo(0).IsName("Cards In Idle"))
        {
            PlayButtonClickSound();

            CheckYesFollowUps();

            //Update stats text, using animations and setting the new value after animation is complete
            if (currentEvent.yesMoneyInstantEffect != 0)
            {
                switch (currentEvent.category)
                {
                    case RandomEvent.Category.EDUCATION:
                        playerPerformance.spentOnEducation += currentEvent.yesMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.ENTERTAINMENT:
                        playerPerformance.spentOnEntertainment += currentEvent.yesMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.FINANCIAL:
                        playerPerformance.spentOnFinancial += currentEvent.yesMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.FOOD:
                        playerPerformance.spentOnFood += currentEvent.yesMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.GAMBLING:
                        playerPerformance.spentOnGambling += currentEvent.yesMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.GAMING:
                        playerPerformance.spentOnGaming += currentEvent.yesMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.SHOPPING:
                        playerPerformance.spentOnShopping += currentEvent.yesMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.SOCIAL:
                        playerPerformance.spentOnSocial += currentEvent.yesMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.TECHNOLOGY:
                        playerPerformance.spentOnTechnology += currentEvent.yesMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.TRANSPORT:
                        playerPerformance.spentOnTransport += currentEvent.yesMoneyInstantEffect;
                        break;
                    case RandomEvent.Category.WORK:
                        playerPerformance.spentOnWork += currentEvent.yesMoneyInstantEffect;
                        break;
                    default:
                        Debug.LogWarning("Category not found");
                        playerPerformance.spentOnFinancial += currentEvent.yesMoneyInstantEffect;
                        break;
                }

                UIController.StartCoroutine("AnimateCashText", currentCash + currentEvent.yesMoneyInstantEffect);
            }

            //Update mood value and mood text animation
            if (currentEvent.yesMoodEffect != 0)
            {
                if (currentEvent.yesMoodEffect > 0)
                {
                    playerPerformance.moodIncreaseActions++;
                }
                else
                {
                    playerPerformance.moodDecreaseActions++;
                }

                currentMood = Mathf.Clamp(currentMood += currentEvent.yesMoodEffect, -100, 100);
                UIController.UpdateMoodText(currentEvent.yesMoodEffect);
            }

            if (currentEvent.yesStudyEffect != 0)
            {
                UIController.StartCoroutine("AnimateStudyText", currentStudyHours + currentEvent.yesStudyEffect);
            }

            animationController.SetTrigger("CardsOut");
        }
    }

    public void NextDay()
    {
        //30 interactive days total, with random non-interactive events mixed throughout and 5 trivia questions
        currentDay++;

        if(currentDay == 30)
        {
            UIController.EndGame();
        }

        int daysToIncrementBy = UnityEngine.Random.Range(3, 6);

        if(currentCash < 0)
        {
            playerPerformance.daysSpentInOverdraft += daysToIncrementBy;
        }

        if(currentMood < 0)
        {
            playerPerformance.daysSpentUnhappy += daysToIncrementBy;
        }

        if(currentStudyHours < 30)
        {
            playerPerformance.daysSpentUnder30StudyHours += daysToIncrementBy;
        }

        currentDate = currentDate.AddDays(daysToIncrementBy);
        currentEvent = currentEvents[currentDay];

        UIController.UpdateDateText();
        UIController.UpdateEventInformation();

        if(currentMonth != currentDate.ToString("MMMM"))
        {
            currentMonth = currentDate.ToString("MMMM");
            NewMonth();
        }

        animationController.SetTrigger("CardsIn");
    }

    private void NewMonth()
    {
        int incomeExpensesDifference = 0;

        foreach (Income income in currentIncome)
        {
            incomeExpensesDifference += income.monthlyAmount;

            switch (income.incomeCategory)
            {
                case Income.Category.STUDENT_LOAN:
                    playerPerformance.earnedFromStudentLoan += income.monthlyAmount;
                    break;
                case Income.Category.WORK:
                    playerPerformance.earnedFromWork += income.monthlyAmount;
                    break;
                case Income.Category.OVERTIME:
                    playerPerformance.earnedFromOvertime += income.monthlyAmount;
                    break;
                default:
                    Debug.LogWarning("Category not found");
                    playerPerformance.earnedFromStudentLoan += income.monthlyAmount;
                    break;
            }
        }

        foreach (Debit debit in currentDebits)
        {
            incomeExpensesDifference += debit.monthlyCost;

            switch (debit.debitCategory)
            {
                case Debit.Category.EDUCATION:
                    playerPerformance.spentOnEducation += debit.monthlyCost;
                    break;
                case Debit.Category.ENTERTAINMENT:
                    playerPerformance.spentOnEntertainment += debit.monthlyCost;
                    break;
                case Debit.Category.FINANCIAL:
                    playerPerformance.spentOnFinancial += debit.monthlyCost;
                    break;
                case Debit.Category.FOOD:
                    playerPerformance.spentOnFood += debit.monthlyCost;
                    break;
                case Debit.Category.GAMBLING:
                    playerPerformance.spentOnGambling += debit.monthlyCost;
                    break;
                case Debit.Category.GAMING:
                    playerPerformance.spentOnGaming += debit.monthlyCost;
                    break;
                case Debit.Category.SHOPPING:
                    playerPerformance.spentOnShopping += debit.monthlyCost;
                    break;
                case Debit.Category.SOCIAL:
                    playerPerformance.spentOnSocial += debit.monthlyCost;
                    break;
                case Debit.Category.TECHNOLOGY:
                    playerPerformance.spentOnTechnology += debit.monthlyCost;
                    break;
                case Debit.Category.TRANSPORT:
                    playerPerformance.spentOnTransport += debit.monthlyCost;
                    break;
                case Debit.Category.WORK:
                    playerPerformance.spentOnWork += debit.monthlyCost;
                    break;
                default:
                    Debug.LogWarning("Category not found");
                    playerPerformance.spentOnFinancial += debit.monthlyCost;
                    break;
            }
        }

        if (payingCreditCard)
        {
            monthsLeftOnCC--;

            if(monthsLeftOnCC <= 0)
            {
                payingCreditCard = false;
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Credit Card")
                    {
                        currentDebits.Remove(currentDebits[i]);
                    }
                }
            }
        }

        if (payingPaydayLoan)
        {
            monthsLeftOnPDL--;

            if(monthsLeftOnPDL <= 0)
            {
                payingPaydayLoan = false;
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Payday Loan")
                    {
                        currentDebits.Remove(currentDebits[i]);
                    }
                }
            }
        }

        if (payingLighthome)
        {
            monthsLeftOnLH--;

            if(monthsLeftOnLH <= 0)
            {
                payingLighthome = false;
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Lighthome")
                    {
                        currentDebits.Remove(currentDebits[i]);
                    }
                }
            }
        }

        if (payingFriendLoan)
        {
            monthsLeftOnFL--;

            if(monthsLeftOnFL <= 0)
            {
                payingFriendLoan = false;
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Loan From Friend")
                    {
                        currentDebits.Remove(currentDebits[i]);
                    }
                }
            }
        }

        if (buyingGameLater)
        {
            incomeExpensesDifference -= 25;
            buyingGameLater = false;
        }

        UIController.StopCoroutine("AnimateCashText");
        UIController.StartCoroutine("AnimateCashText", currentCash + incomeExpensesDifference);
    }

    private void CheckNoFollowUps()
    {
        switch (currentEvent.ID)
        {
            case 28: //Switch to lower paying job
                for (int i = 0; i < currentIncome.Count; i++)
                {
                    if (currentIncome[i].name == "Work")
                    {
                        currentIncome[i].monthlyAmount -= 50;
                    }
                }
                break;
        }
    }

    private void CheckYesFollowUps()
    {
        switch (currentEvent.ID)
        {
            case 3: //Car bought
                carOwned = true;
                currentDebits.Add(possibleDebits[0]);
                for(int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Public Transport")
                    {
                        currentDebits.Remove(currentDebits[i]);
                    }
                }
                break;
            case 4: //TV licence cancelled
                tvLicenceCancelled = true;
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "TV Licence")
                    {
                        currentDebits.Remove(currentDebits[i]);
                    }
                }
                break;
            case 5: //Paid with credit card
                payingCreditCard = true;
                monthsLeftOnCC = 4;
                currentDebits.Add(possibleDebits[2]);
                break;
            case 6: //Overtime
                currentIncome.Add(possibleIncome[2]);
                break;
            case 7: //Phone insurance
                phoneInsurance = true;
                currentDebits.Add(possibleDebits[3]);
                break;
            case 8: //Payday loan
                payingPaydayLoan = true;
                monthsLeftOnPDL = 3;
                currentDebits.Add(possibleDebits[4]);
                break;
            case 9: //Sports bet
                betOnSports = true;
                break;
            case 13: //Walking to uni
                if (!carOwned)
                {
                    for (int i = 0; i < currentDebits.Count; i++)
                    {
                        if (currentDebits[i].name == "Public Transport")
                        {
                            currentDebits.Remove(currentDebits[i]);
                        }
                    }
                }
                break;
            case 14: //Phone upgrade
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Phone Contract")
                    {
                        currentDebits[i].monthlyCost -= 15;
                    }
                }
                break;
            case 18: //Cheaper shopping
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Shopping")
                    {
                        currentDebits[i].monthlyCost += 60;
                    }
                }
                break;
            case 19: //Lottery
                playingLottery = true;
                currentDebits.Add(possibleDebits[8]);
                break;
            case 20: //Buying game later
                buyingGameLater = true;
                break;
            case 27: //Charity
                currentDebits.Add(possibleDebits[9]);
                break;
            case 28: //Change job
                for (int i = 0; i < currentIncome.Count; i++)
                {
                    if (currentIncome[i].name == "Work")
                    {
                        currentIncome[i].monthlyAmount += 80;
                    }
                }
                break;
            case 29: //Cancel internet
                internetCancelled = true;
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Internet")
                    {
                        currentDebits.Remove(currentDebits[i]);
                    }
                }
                break;
            case 31: //Game addons cancelled
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Game Addons")
                    {
                        currentDebits.Remove(currentDebits[i]);
                    }
                }
                break;
            case 35: //Upgrade student flat
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Rent")
                    {
                        currentDebits[i].monthlyCost -= 35;
                    }
                }
                break;
            case 36: //Friend loan
                payingFriendLoan = true;
                monthsLeftOnFL = 2;
                currentDebits.Add(possibleDebits[15]);
                break;
            case 37: //Shopping daily
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Shopping")
                    {
                        currentDebits[i].monthlyCost += 20;
                    }
                }
                break;
            case 38: //Lighthome
                payingLighthome = true;
                monthsLeftOnLH = 18;
                currentDebits.Add(possibleDebits[13]);
                break;
            case 40: //Bring lunch to work
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Lunch In Work")
                    {
                        currentDebits[i].monthlyCost += 20;
                    }
                }
                break;
            case 41: //Energy savings
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Rent")
                    {
                        currentDebits[i].monthlyCost += 15;
                    }
                }
                break;
            case 42: //Shop at night
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "Shopping")
                    {
                        currentDebits[i].monthlyCost += 10;
                    }
                }
                break;
            case 43: //Upgrade internet
                if (!internetCancelled)
                {
                    for (int i = 0; i < currentDebits.Count; i++)
                    {
                        if (currentDebits[i].name == "Internet")
                        {
                            currentDebits[i].monthlyCost -= 15;
                        }
                    }
                }
                break;
            case 44: //Cancel TV and landline
                for (int i = 0; i < currentDebits.Count; i++)
                {
                    if (currentDebits[i].name == "TV & Landline Phone")
                    {
                        currentDebits.Remove(currentDebits[i]);
                    }
                }
                break;
        }
    }

    private void SetUpStats()
    {
        //Game stats are randomised each time to simulate real life uncertainty
        //TODO Tweak amounts to balance gameplay when events added to game
        currentCash = (int)Mathf.Round((UnityEngine.Random.Range(150, 600)) / 10) * 10;
        currentMood = UnityEngine.Random.Range(-50, 50);
        currentStudyHours = UnityEngine.Random.Range(20, 28);
    }

    private void SetUpIncome()
    {
        currentIncome.Add(possibleIncome[0]); //Student loan
        currentIncome.Add(possibleIncome[1]); //Work
    }

    private void SetUpDebits()
    {
        currentDebits.Add(possibleDebits[1]); //TV Licence
        currentDebits.Add(possibleDebits[5]); //Public transport
        currentDebits.Add(possibleDebits[6]); //Phone contract
        currentDebits.Add(possibleDebits[7]); //Shopping
        currentDebits.Add(possibleDebits[10]); //Internet
        currentDebits.Add(possibleDebits[11]); //Game addons
        currentDebits.Add(possibleDebits[12]); //Rent
        currentDebits.Add(possibleDebits[14]); //TV and landline
        currentDebits.Add(possibleDebits[16]); //Lunch in work
    }

    private void BuildEventsList()
    {
        //Randomly removes 12 items from the random events list
        //Will bring list down to 23 items, which combined with the 7 mandatory events will bring the list to 30
        for (int i = 0; i < 12; i++)
        {
            randomEvents.RemoveAt(UnityEngine.Random.Range(0, randomEvents.Count));
        }

        //Copies the mandatory events into a temporary list
        List<RandomEvent> tempEventsList = new List<RandomEvent>();

        for (int i = 0; i < mandatoryEvents.Count; i++)
        {
            tempEventsList.Add(mandatoryEvents[i]);
        }

        //Copies the remaining random events into the temporary list
        for (int i = 0; i < randomEvents.Count; i++)
        {
            tempEventsList.Add(randomEvents[i]);
        }

        //Creates new random number generator
        System.Random rnd = new System.Random();

        //Randomises the order of the temporary list with mandatory and random events combined, then copies the events into the current events list
        currentEvents = tempEventsList.OrderBy(item => rnd.Next()).ToList();
    }

    private void PlayButtonClickSound()
    {
        //Only plays sound if enabled in player options
        if (soundsEnabled)
        {
            buttonClickAudioSource.Play();
        }
    }

    public void MenuButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}