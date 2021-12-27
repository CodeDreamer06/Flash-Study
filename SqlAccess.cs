using System;
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

    public static void ReadTable(string table) {
      Read($"SELECT * FROM {table}");
    }

    public static void ReadTable(string table, string fieldName, string filterId) {
      Read($"SELECT * FROM {table} WHERE {fieldName} = {filterId}");
    }

    public static List<FlashCard> ReadFlashCards() {
      return ReadFlashCardsAsList($"SELECT * FROM FlashCards");
    }

    public static List<FlashCard> ReadFlashCards(string stackId) {
      return ReadFlashCardsAsList($"SELECT * FROM FlashCards WHERE StackId = {stackId}");
    }

    public static void AddStack(string name) {
      Execute($"INSERT INTO Stacks(StackName) VALUES('{name}');");
      Console.WriteLine("Successfully added " +  name);
    }

    public static void RemoveStack(string name) {
      Execute($"DELETE FROM Stacks WHERE StackName='{name}';");
      Console.WriteLine("Successfully removed " + name);
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
      Execute($"DELETE FROM FlashCards WHERE cardId='{cardId}';");
      Console.WriteLine($"Successfully removed {cardId}");
    }
  }
}
