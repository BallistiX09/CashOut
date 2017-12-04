[System.Serializable]
public class UncontrolledEvent
{
    //Contains image types for good and bad uncontrolled events
    public enum Category { UNCONTROLLED_GOOD, UNCONTROLLED_BAD };

    //Publicly accessible UncontrolledEvent parameters
    public int ID;
    public string title;
    public Category category;
    public string description;
    public int moneyInstantEffect;
    public int moodEffect;
    public int studyEffect;
}