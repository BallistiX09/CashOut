//RandomEvent containing a title, image, and description
[System.Serializable]
public class RandomEvent
{
    //Contains every image type for each matching event category
    public enum Category { TRANSPORT, ENTERTAINMENT, FINANCIAL, WORK, GAMBLING, SOCIAL, TECHNOLOGY, GAMING, SHOPPING, FOOD, EDUCATION, RENT };

    //Publicly accessible RandomEvent parameters
    public int ID;
    public string title;
    public Category category;
    public string description;
    public int yesMoneyInstantEffect;
    public int yesMoneyMonthlyEffect;
    public int yesMoodEffect;
    public int yesStudyEffect;
    public int noMoneyInstantEffect;
    public int noMoneyMonthlyEffect;
    public int noMoodEffect;
    public int noStudyEffect;

    /*public RandomEvent(string title, imageType image, string description, int moneyEffect, int moodEffect) //Constructor for RandomEvent object, no longer needed
    {
        this.title = title;
        this.image = image;
        this.description = description;
        this.moneyEffect = moneyEffect;
        this.moodEffect = moodEffect;
    }*/
}