using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using ClassLibraryDAL;
using DataAccess.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Abstractions;
using Model.Models;

namespace DataAccess.RepositoriesImpl;

public class UserRepository : IUserRepository
{
   

    public async Task<User?> GetUser(string email)
    {
        SqlConnection con = DbContext.GetConnection();
        
        await con.OpenAsync();
        SqlCommand cmd = new SqlCommand("usp_UserSelectByEmail", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@email", email);
        //Console.WriteLine("parawhat: " + cmd.Parameters[0].ToString());
        User? user = null;
        try
        {
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            reader.Read();
            user = new User
            {
                Id = Convert.ToInt32(reader["id"]),
                PasswordHash = Convert.ToString(reader["password_hash"])!,
                Email = Convert.ToString(reader["email"])!,
                Name = Convert.ToString(reader["name"])!,
                Role = Convert.ToString(reader["role"])!
            };
        }
        catch (InvalidOperationException e)
        {
            return null;
        }
        catch (Exception e)
        {
            Debug.Print("Exception occured when logging in User: " + e);
        }
        await con.CloseAsync();
        return user;
    }

}