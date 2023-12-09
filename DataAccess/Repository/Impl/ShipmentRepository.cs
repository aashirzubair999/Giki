using System.Data;
using ClassLibraryDAL;
using DataAccess.Repositories;
using Microsoft.Data.SqlClient;
using Model.Dto;

namespace DataAccess.Repository.Impl;

public class ShipmentRepository: IShipmentRepository
{
    public async Task<List<ShipmentRequest?>> GetApprovedShipments()
    {
        SqlConnection con = DbContext.GetConnection();

        await con.OpenAsync();
        SqlCommand cmd = new SqlCommand("usp_ShipmentSelectApproved", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader reader = await cmd.ExecuteReaderAsync();
        List<ShipmentRequest> Shipments = new List<ShipmentRequest>();
        while (await reader.ReadAsync())
        {
            Shipments.Add(
                new ShipmentRequest()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = Convert.ToString(reader["shipName"])!,
                    LiveLocation =
                    {
                        Lat = Convert.ToDouble(reader["liveLocationLat"])!,
                        Long = Convert.ToDouble(reader["liveLocationLong"])!
                    },
                    ShipmentToVendor =
                    {
                        Name = Convert.ToString(reader["vendor"])!
                    }
                }
            );
        }

        await con.CloseAsync();
        return Shipments;
    }

    public async Task<List<ShippmentApprovalDto?>> GetShipmentsForApproval()
    {
        SqlConnection con = DbContext.GetConnection();

        await con.OpenAsync();
        SqlCommand cmd = new SqlCommand("usp_ShipmentSelectForApproval", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataReader reader = await cmd.ExecuteReaderAsync();
        List<ShippmentApprovalDto> shipments = new List<ShippmentApprovalDto>();
        while (await reader.ReadAsync())
        {
            shipments.Add(
                new ShippmentApprovalDto()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = Convert.ToString(reader["shipName"])!,
                    RequestedBy = Convert.ToString(reader["name"])!,
                    vendorName = Convert.ToString(reader["vendor"])!
                }
            );
        }

        await con.CloseAsync();
        return shipments;
    }

    public async Task ApproveShipment(int id, int managerId, bool approved)
    {
        SqlConnection con = DbContext.GetConnection();
        SqlCommand cmd = new SqlCommand();
        await con.OpenAsync();
        if (approved)
        {
            cmd = new SqlCommand("usp_ShipmentApprove", con);
            cmd.Parameters.AddWithValue("@ApprovedBy", managerId);

        }
        else
        {
            cmd = new SqlCommand("usp_ShipmentDelete", con);

        }
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ID", id);
        await cmd.ExecuteNonQueryAsync();
        
        await con.CloseAsync();
    }
    
    public async Task CreateShipment(ShipmentRequest shipment)
    {
        SqlConnection con = DbContext.GetConnection();
        
        await con.OpenAsync();

        SqlCommand cmd = new SqlCommand("usp_ShipmentInsert", con);

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@name", shipment.Name);
        cmd.Parameters.AddWithValue("@requestedBy", shipment.RequestedBy);
        cmd.Parameters.AddWithValue("@liveLocationLat", shipment.LiveLocation.Lat);
        cmd.Parameters.AddWithValue("@liveLocationLong", shipment.LiveLocation.Long);
        cmd.Parameters.AddWithValue("@shippedFromCity", shipment.ShippedFromCity.Id);
        cmd.Parameters.AddWithValue("@shippedFromCity", shipment.ShipmentToVendor.Id);
        
        
        await con.CloseAsync();
    }

   
}