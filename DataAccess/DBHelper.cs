using Microsoft.Data.SqlClient;

namespace DataAccess;

public class DbContext
{

    public static SqlConnection GetConnection()
    {
        SqlConnection con = new SqlConnection("Server=tcp:hnc-giki.database.windows.net,1433;Initial Catalog=hackathon;Persist Security Info=False;User ID=hnc-giki;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        return con;


    }
}