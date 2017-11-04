//RandomEvent containing a title, image, and description
public class RandomEvent
{
    //Contains every image type for each matching event category
    public enum imageType { TYPE_1, TYPE_2, TYPE_3, TYPE_4, TYPE_5, TYPE_6 };
     
    //Publicly accessible RandomEvent parameters
    public string title;
    public imageType image;
    public string description;

    public RandomEvent(string title, imageType image, string description) //Constructor for RandomEvent object
    {
        this.title = title;
        this.image = image;
        this.description = description;
    }
}
