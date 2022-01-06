using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FlashStudy.Models;

namespace FlashStudy
{
  class SqlAccess
  {
    public static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
    public static string defaultConnStr = ConfigurationManager.ConnectionStrings["defaultConnStr"].ConnectionString;

    public static void Check() {
      using(MySqlConnection con = new MySqlConnection(defaultConnStr)) {
        try {
          con.Open();
          string query = "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'Study'";
          using var cmd = new MySqlCommand(query, con);
          MySqlDataReader reader = cmd.ExecuteReader();
          if(reader.HasRows) return;
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
      using(MySqlConnection con = new MySqlConnection(defaultConnStr)) {
        Console.WriteLine("Welcome to Flash Study! \nCreating the database..");
        con.Open();
        var initialQueries = new string[] {
          @"CREATE DATABASE Study;",
          @"USE Study;",
          @"CREATE TABLE Stacks(
            StackId INT PRIMARY KEY AUTO_INCREMENT,
            StackName VARCHAR(64) NOT NULL
          );",
          @"CREATE TABLE FlashCards(
            CardId INT PRIMARY KEY AUTO_INCREMENT,
            Title VARCHAR(255) NOT NULL,
            Answer VARCHAR(64),
            StackId INT NOT NULL,
            FOREIGN KEY (StackId)
              REFERENCES Stacks(StackId)
              ON DELETE CASCADE
          );",
          @"CREATE TABLE Sessions(
            SessionId INT PRIMARY KEY AUTO_INCREMENT,
            CreatedOn TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
            Score INT NOT NULL,
            StackId INT NOT NULL,
            FOREIGN KEY (StackId)
              REFERENCES Stacks(StackId)
              ON DELETE CASCADE
          );"
        };
        foreach (string line in initialQueries) {
          using var lineQuery = new MySqlCommand(line, con);
          lineQuery.ExecuteNonQuery();
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


    public static List<Session> ReadSessionsAsList(string query)
    {
      try {
        using(MySqlConnection con = new MySqlConnection(connStr)){
          con.Open();
          using var cmd = new MySqlCommand(query, con);
          MySqlDataReader reader = cmd.ExecuteReader();
          if(!reader.HasRows)
            Console.WriteLine("No data found");
          var output = new List<Session>();
          while (reader.Read())
            output.Add(new Session(Convert.ToInt32(reader["SessionId"]), reader["CreatedOn"].ToString(), Convert.ToInt32(reader["Score"]), Convert.ToInt32(reader["StackId"])));
          return output;
        }
      }

      catch (Exception ex)
      {
        Console.WriteLine(ex);
        return null;
      }
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

    public static void AddSession(Session properties) {
      Execute($"INSERT INTO Sessions(score, StackId) VALUES('{properties.Score}', '{properties.StackId}');");
    }

    public static List<Session> ReadSessions() {
      return ReadSessionsAsList($"SELECT * FROM Sessions");
    }
  }
}
