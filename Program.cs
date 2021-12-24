using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DotNet_Flash_Study
{
    class Program
    {
        public static string help = @"
Type 1 to Study
Type 2 to Manage your flash cards
Type 0 to Exit
        ";

        public static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        static void Main(string[] args)
        {
          MySqlConnection conn = new MySqlConnection(connStr);
          try
          {
              Console.WriteLine("Hi there! What would you like to do?");
              Console.WriteLine(help);
              conn.Open();

              string sql = "SELECT * FROM FlashCards";
              MySqlCommand cmd = new MySqlCommand(sql, conn);
              MySqlDataReader reader = cmd.ExecuteReader();

              while (reader.Read())
              {
                  Console.WriteLine(reader[0]+" - "+reader[1]);
              }
              reader.Close();
          }
          catch (Exception ex)
          {
              Console.WriteLine(ex.ToString());
          }

          conn.Close();
          Console.WriteLine("Done.");
        }
    }
}
