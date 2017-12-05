using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class DatabaseController : MonoBehaviour {

    DatabaseReference reference;
    private GameController gameController;
    private UIController uiController;
    private PlayerPerformance playerPerformance;

	void Start ()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://cashout-6e5cc.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        gameController = FindObjectOfType<GameController>();
        uiController = FindObjectOfType<UIController>();
        playerPerformance = FindObjectOfType<PlayerPerformance>();
    }

    public void SubmitResultsToDatabase()
    {
        DatabaseEntry databaseEntry = new DatabaseEntry(GameController.currentCash, GameController.currentMood, GameController.currentStudyHours, uiController.GetHighestSpendingCategory(), 
            uiController.GetRemainingDebt(), playerPerformance.earnedFromStudentLoan, playerPerformance.earnedFromWork, playerPerformance.earnedFromOvertime, playerPerformance.spentOnTransport,
            playerPerformance.spentOnEntertainment, playerPerformance.spentOnFinancial, playerPerformance.spentOnFood, playerPerformance.spentOnGambling, playerPerformance.spentOnShopping,
            playerPerformance.spentOnSocial, playerPerformance.spentOnTechnology, playerPerformance.spentOnGaming, playerPerformance.spentOnEducation, playerPerformance.spentOnRent,
            playerPerformance.moodIncreaseActions, playerPerformance.moodDecreaseActions, playerPerformance.daysSpentInOverdraft, playerPerformance.daysSpentUnhappy, playerPerformance.daysSpentUnder30StudyHours);

        string jsonData = JsonUtility.ToJson(databaseEntry);

        reference.Push().SetRawJsonValueAsync(jsonData).ContinueWith(
            (task) => {
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogWarning("Could not push to database. Exception: " + task.Exception);
                }
                else
                {
                    Debug.Log("Pushed to database sucessfully");
                }
            }
        );
    }
}

public class DatabaseEntry
{
    public double finalCash, finalMood, finalStudyHours;
    public string highestSpendingCategory;
    public double totalDebtRemaining;
    public double earnedFromStudentLoan, earnedFromWork, earnedFromOvertime;
    public double spentOnTransport, spentOnEntertainment, spentOnFinancial, spentOnFood, spentOnGambling, spentOnShopping, spentOnSocial, spentOnTechnology, spentOnGaming, spentOnEducation, spentOnRent;
    public double moodIncreaseActions, moodDecreaseActions;
    public double daysSpentInOverdraft, daysSpentUnhappy, daysSpentUnder30StudyHours;

    public DatabaseEntry(double finalCash, double finalMood, double finalStudyHours, string highestSpendingCategory, double totalDebtRemaining, double earnedFromStudentLoan, double earnedFromWork, double earnedFromOvertime, double spentOnTransport, double spentOnEntertainment, double spentOnFinancial, double spentOnFood, double spentOnGambling, double spentOnShopping, double spentOnSocial, double spentOnTechnology, double spentOnGaming, double spentOnEducation, double spentOnRent, double moodIncreaseActions, double moodDecreaseActions, double daysSpentInOverdraft, double daysSpentUnhappy, double daysSpentUnder30StudyHours)
    {
        this.finalCash = finalCash;
        this.finalMood = finalMood;
        this.finalStudyHours = finalStudyHours;
        this.highestSpendingCategory = highestSpendingCategory;
        this.totalDebtRemaining = totalDebtRemaining;
        this.earnedFromStudentLoan = earnedFromStudentLoan;
        this.earnedFromWork = earnedFromWork;
        this.earnedFromOvertime = earnedFromOvertime;
        this.spentOnTransport = spentOnTransport;
        this.spentOnEntertainment = spentOnEntertainment;
        this.spentOnFinancial = spentOnFinancial;
        this.spentOnFood = spentOnFood;
        this.spentOnGambling = spentOnGambling;
        this.spentOnShopping = spentOnShopping;
        this.spentOnSocial = spentOnSocial;
        this.spentOnTechnology = spentOnTechnology;
        this.spentOnGaming = spentOnGaming;
        this.spentOnEducation = spentOnEducation;
        this.spentOnRent = spentOnRent;
        this.moodIncreaseActions = moodIncreaseActions;
        this.moodDecreaseActions = moodDecreaseActions;
        this.daysSpentInOverdraft = daysSpentInOverdraft;
        this.daysSpentUnhappy = daysSpentUnhappy;
        this.daysSpentUnder30StudyHours = daysSpentUnder30StudyHours;
    }
}
