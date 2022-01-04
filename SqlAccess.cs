using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

/*
Creating table FlashCards:
CREATE TABLE FlashCards(
  CardId INT PRIMARY KEY AUTO_INCREMENT,
  Title VARCHAR(255) NOT NULL,
  Answer VARCHAR(64),
  StackId INT NOT NULL,
  FOREIGN KEY (StackId)
    REFERENCES Stacks(StackId)
    ON DELETE CASCADE
);
*/

namespace DotNet_Flash_Study
{
  class SqlAccess
  {
    public static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

    public static void Check() {
      using(MySqlConnection con = new MySqlConnection(connStr)) {
        try {
          con.Open();
        }

        catch (MySqlException ex) {
          Console.WriteLine("An error occurred.");
          if(ex.Number == 1042)
            Console.WriteLine("Unable to connect to MySQL server. Make sure MySQL is running.");
          else
            Console.WriteLine(ex);
          System.Environment.Exit(1);
        }
      }
    }

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

    protected static void Read(string query)
    {
      try {
        using(MySqlConnection con = new MySqlConnection(connStr)){
          con.Open();
          using var cmd = new MySqlCommand(query, con);
          MySqlDataReader reader = cmd.ExecuteReader();
          if(!reader.HasRows)
            Console.WriteLine("No data found");
          while (reader.Read()) {
            for (int i = 0; i < reader.FieldCount; i++)
              Console.Write(reader[i].ToString() + "\t");
            Console.Write("\n");
          }
        }
      }

      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }

    public static List<FlashCard> ReadFlashCardsAsList(string query)
    {
      try {
        using(MySqlConnection con = new MySqlConnection(connStr)){
          con.Open();
          using var cmd = new MySqlCommand(query, con);
          MySqlDataReader reader = cmd.ExecuteReader();
          if(!reader.HasRows)
            Console.WriteLine("No data found");
          var output = new List<FlashCard>();
          while (reader.Read())
            output.Add(new FlashCard(reader["CardId"].ToString(), reader["Title"].ToString(), reader["Answer"].ToString(), reader["StackId"].ToString()));
          return output;
        }
      }

      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return null;
      }
    }

    public static List<Stack> ReadStacksAsList(string query)
    {
      try {
        using(MySqlConnection con = new MySqlConnection(connStr)){
          con.Open();
          using var cmd = new MySqlCommand(query, con);
          MySqlDataReader reader = cmd.ExecuteReader();
          if(!reader.HasRows)
            Console.WriteLine("No data found");
          var output = new List<Stack>();
          while (reader.Read())
            output.Add(new Stack(Convert.ToInt32(reader["StackId"]), reader["StackName"].ToString()));
          return output;
        }
      }

      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return null;
      }
    }

    public static void ReadTable(string table) {
      Read($"SELECT * FROM {table}");
    }

    public static void ReadTable(string table, string fieldName, string filterId) {
      Read($"SELECT * FROM {table} WHERE {fieldName} = {filterId}");
    }

    public static List<FlashCard> ReadFlashCards() {
      return ReadFlashCardsAsList($"SELECT * FROM FlashCards");
    }

    public static List<FlashCard> ReadFlashCards(int stackId) {
      return ReadFlashCardsAsList($"SELECT * FROM FlashCards WHERE StackId = {stackId}");
    }

    public static List<Stack> ReadStacks() {
      return ReadStacksAsList($"SELECT * FROM Stacks");
    }

    public static void AddStack(Stack properties) {
      Execute($"INSERT INTO Stacks(StackName) VALUES('{properties.StackName}');");
      Console.WriteLine("Successfully added " +  properties.StackName);
    }

    public static void RemoveStack(Stack properties) {
      Execute($"DELETE FROM Stacks WHERE StackName='{properties.StackName}';");
      Console.WriteLine("Successfully removed " + properties.StackName);
    }
    public static void EditStack(string oldName, string newName) {
      Execute($"UPDATE Stacks SET StackName = '{newName}' WHERE StackName = '{oldName}';");
      Console.WriteLine($"Successfully changed {oldName} to {newName}");
    }

    public static void AddCard(FlashCard properties) {
      Execute($"INSERT INTO FlashCards(Title, Answer, StackId) VALUES('{properties.Title}', '{properties.Answer}', '{properties.StackId}');");
      Console.WriteLine($"Successfully inserted {properties.Title}");
    }

    public static void RemoveCard(string cardId) {
      var flashCard = SqlAccess.ReadFlashCards()[Convert.ToInt32(cardId) - 1];
      Execute($"DELETE FROM FlashCards WHERE cardId='{flashCard.CardId}';");
      Console.WriteLine($"Successfully removed {flashCard.Title}");
    }
  }
}
