//RandomEvent containing a title, image, and description
[System.Serializable]
public class RandomEvent
{
    //Contains every image type for each matching event category
    public enum imageType { TYPE_1, TYPE_2, TYPE_3, TYPE_4, TYPE_5, TYPE_6 };

    //Publicly accessible RandomEvent parameters
    public string title;
    public imageType image;
    public string description;
    public int moneyEffect;
    public int moodEffect;

    /*public RandomEvent(string title, imageType image, string description, int moneyEffect, int moodEffect) //Constructor for RandomEvent object, no longer needed
    {
        this.title = title;
        this.image = image;
        this.description = description;
        this.moneyEffect = moneyEffect;
        this.moodEffect = moodEffect;
    }*/
}
