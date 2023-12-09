namespace ClassLibraryDAL;

public class ShipmentRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RequestedBy { get; set; }
    public int? AcceptedBy { get; set; }
    public Location LiveLocation { get; set; } = new Location();
    public City ShippedFromCity { get; set; }
    
}