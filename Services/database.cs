using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics.X86;
using Microsoft.Data.Sqlite;
namespace live_message_app.Services;
using static Console;

public struct Messagestruct
{
    public string Text;
    public int from_id;
    public int to_id;
    public int order;

    
}

public struct user
{
    public int id;
    public string name;
    public string username;
}
public class database
{
    private string path = "Data Source=/home/bro/my-creations/live-message-app/databases/admin.db ";
    public int check_login(string username, string passwd)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(passwd))
            return -1;
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText="SELECT id FROM users WHERE username=$usr AND password=$psd;";
        cmd.Parameters.AddWithValue("$usr", username);
        cmd.Parameters.AddWithValue("$psd", passwd);

        using var res = cmd.ExecuteReader()!;
        Console.WriteLine($"DEBUG: user='{username}' pass='{passwd}' count={res}");
        return res.GetInt32(0);
    }

    public bool add(string username, string name, string password, string gmail)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) ||
            string.IsNullOrEmpty(gmail))
        {
            return false;
        }

        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = "INSERT INTO users(username,name,password,gmail) VALUES ($user,$name,$passwd,$gmail);";
        cmd.Parameters.AddWithValue("$user", username);
        cmd.Parameters.AddWithValue("$name", name);
        cmd.Parameters.AddWithValue("$passwd", password);
        cmd.Parameters.AddWithValue("$gmail", gmail);

        long res = (long)cmd.ExecuteNonQuery()!;
        return res > 0;
        
    }

    public user search_by_id(int id)
    {
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText="SELECT * FROM users WHERE id=$d;";
        cmd.Parameters.AddWithValue("$d", id);
        using var res = cmd.ExecuteReader()!;
        //!Console.WriteLine($"DEBUG: user='{username}' pass='{passwd}' count={res}");-->
        user temp=new();
        while (res.Read())
        {
            temp.id = id;
            temp.name=res.GetString(1);
            temp.username=res.GetString(1);
        }

        return temp;

    }
    public List<Messagestruct> Fetchmessages(int id)
    {
        List<Messagestruct> tempo=new();
        using var con = new SqliteConnection(path);
        con.Open();
        var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT * FROM chats WHERE from_di=$f OR to_id=$t;";
        cmd.Parameters.AddWithValue("$f", id);  
        cmd.Parameters.AddWithValue("$t", id);  
        using var ls =cmd.ExecuteReader()!;
        while (ls.Read())
        {
            Messagestruct temp;
            temp.from_id = ls.GetInt32(0);
            temp.to_id = ls.GetInt32(1);
            temp.Text = ls.GetString(2);
            temp.order = ls.GetInt32(3);
            tempo.Add(temp);
        }

        return tempo;
    }

    public List<user> Fetchfriends(List<Messagestruct> mlist,int id)
    {
        List<user> friends = new();
        List<int> temp = new();
        foreach (Messagestruct m in mlist)
        {
            if (m.from_id == id)
            {
                if (!temp.Contains(m.to_id))
                {
                    friends.Add(search_by_id(m.to_id));
                    temp.Add(m.to_id);
                }
                
            }else if (m.to_id == id)
            {
                if (!temp.Contains(m.from_id))
                {
                    friends.Add(search_by_id(m.from_id));
                    temp.Add(m.from_id);
                }
            }
        }

        return friends;
    }
    
}