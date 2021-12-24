using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DotNet_Flash_Study
{
  class SqlAccess
  {
    public static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

    public static void Execute(string query)
    {
      try {
        using(MySqlConnection con = new MySqlConnection(connStr)){
          con.Open();
          using var cmd = new MySqlCommand(query, con);
          cmd.ExecuteNonQuery();
        }
      }

      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }

    public static void Read(string query)
    {
      try {
        using(MySqlConnection con = new MySqlConnection(connStr)){
          con.Open();
          using var cmd = new MySqlCommand(query, con);
          MySqlDataReader reader = cmd.ExecuteReader();
          while (reader.Read())
            Console.WriteLine(reader[0]+": "+reader[1]);
        }
      }

      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
  }
}
