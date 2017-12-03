[System.Serializable]
public class Income
{
    public enum Category { STUDENT_LOAN, WORK, OVERTIME };

    public string name;
    public Category incomeCategory;
    public int monthlyAmount;

    public Income(string name, Category incomeCategory, int monthlyCost)
    {
        this.name = name;
        this.incomeCategory = incomeCategory;
        this.monthlyAmount = monthlyCost;
    }
}