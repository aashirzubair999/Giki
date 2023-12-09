using ClassLibraryDAL;

namespace Model;

public class Vendor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public City LocatedAtCity { get; set; }
}