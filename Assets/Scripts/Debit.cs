[System.Serializable]
public class Debit
{
    public enum Category { TRANSPORT, ENTERTAINMENT, FINANCIAL, WORK, GAMBLING, SOCIAL, TECHNOLOGY, GAMING, SHOPPING, FOOD, EDUCATION, RENT };

    public string name;
    public Category debitCategory;
    public int monthlyCost;

    public Debit(string name, Category debitCategory, int monthlyCost)
    {
        this.name = name;
        this.debitCategory = debitCategory;
        this.monthlyCost = monthlyCost;
    }
}