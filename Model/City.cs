namespace ClassLibraryDAL;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Location Location { get; set; } = new Location();


}