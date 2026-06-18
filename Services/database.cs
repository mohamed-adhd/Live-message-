using System.IO;
using Microsoft.Data.Sqlite;
namespace live_message_app.Services;

public class database
{
    private string path = "/databses/data.db";

    public bool check_login(string username, string passwd)
    {
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText="SELECT * FROM users WHERE username=$usr AND passwd=$psd;";
        cmd.Parameters.AddWithValue("$usr", username);
        cmd.Parameters.AddWithValue("$psd", passwd);

        long res = (long)cmd.ExecuteScalar();
        return res > 0;
    }
    
}