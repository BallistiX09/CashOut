[System.Serializable]
public class UncontrolledEvent {

    //Contains image types for good and bad uncontrolled events
    public enum imageType { UNCONTROLLED_GOOD, UNCONTROLLED_BAD };

    //Publicly accessible UncontrolledEvent parameters
    public int ID;
    public string title;
    public imageType image;
    public string description;
    public int moneyInstantEffect;
    public int moneyMonthlyEffect;
    public int moodEffect;
    public int studyEffect;
}
