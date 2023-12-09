using ClassLibraryDAL;
using Model.Dto;

namespace DataAccess.Repositories;

public interface IShipmentRepository
{
    public Task<List<ShipmentRequest?>> GetApprovedShipments();
    public Task<List<ShippmentApprovalDto?>> GetShipmentsForApproval();
    public Task ApproveShipment(int id, int managerId, bool approved);

    public Task CreateShipment(ShipmentRequest shipment);
    

}