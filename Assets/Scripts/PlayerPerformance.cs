using UnityEngine;
[System.Serializable]
public class PlayerPerformance : MonoBehaviour
{
    public int earnedFromStudentLoan, earnedFromWork, earnedFromOvertime;
    public int spentOnTransport, spentOnEntertainment, spentOnFinancial, spentOnFood, spentOnGambling, spentOnShopping, spentOnSocial, spentOnTechnology, spentOnGaming, spentOnEducation, spentOnRent;
    public int moodIncreaseActions, moodDecreaseActions;
    public int daysSpentInOverdraft, daysSpentUnhappy, daysSpentUnder30StudyHours;

    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public int debtLeftOnCC()
    {
        int debt = 0;

        if (GameController.payingCreditCard)
        {
            for (int i = 0; i < GameController.monthsLeftOnCC; i++)
            {
                GameController.monthsLeftOnCC--;

                for (int j = 0; j < gameController.currentDebits.Count; j++)
                {
                    if (gameController.currentDebits[j].name == "Credit Card")
                    {
                        debt += gameController.currentDebits[j].monthlyCost;
                    }
                }
            }
        }

        return debt;
    }

    public int debtLeftOnPDL()
    {
        int debt = 0;

        if (GameController.payingPaydayLoan)
        {
            for (int i = 0; i < GameController.monthsLeftOnPDL; i++)
            {
                GameController.monthsLeftOnPDL--;

                for (int j = 0; j < gameController.currentDebits.Count; j++)
                {
                    if (gameController.currentDebits[j].name == "Payday Loan")
                    {
                        debt += gameController.currentDebits[j].monthlyCost;
                    }
                }
            }
        }

        return debt;
    }

    public int debtLeftOnLH()
    {
        int debt = 0;

        if (GameController.payingLighthome)
        {
            for (int i = 0; i < GameController.monthsLeftOnLH; i++)
            {
                GameController.monthsLeftOnLH--;

                for (int j = 0; j < gameController.currentDebits.Count; j++)
                {
                    if (gameController.currentDebits[j].name == "Lighthome")
                    {
                        debt += gameController.currentDebits[j].monthlyCost;
                    }
                }
            }
        }

        return debt;
    }

    public int debtLeftOnFL()
    {
        int debt = 0;

        if (GameController.payingFriendLoan)
        {
            for (int i = 0; i < GameController.monthsLeftOnFL; i++)
            {
                GameController.monthsLeftOnFL--;

                for (int j = 0; j < gameController.currentDebits.Count; j++)
                {
                    if (gameController.currentDebits[j].name == "Loan From Friend")
                    {
                        debt += gameController.currentDebits[j].monthlyCost;
                    }
                }
            }
        }

        return debt;
    }
}
