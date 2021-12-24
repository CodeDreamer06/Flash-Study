using System;

namespace DotNet_Flash_Study
{
  class StackController {

    public static string stackMessage = @"
Manage Stacks
* show: to view stacks
* add [stack name]: to add stacks
* edit [old stack name] [new stack name]: to edit stacks
* remove [stack name]: to remove stacks
* Back or 0: go back to the main menu";

    public static string command = "";

    public static void Start() {
      while(true) {
        Console.WriteLine(stackMessage);
        command = Console.ReadLine().ToLower().Trim();

        if(command == "exit" || command == "0") break;

        else if(command == "help") Console.WriteLine(stackMessage);

        else if(command == "show")
          SqlAccess.Read("SELECT * FROM Stacks");

        else if(command.StartsWith("add"))
          SqlAccess.Execute($"INSERT INTO Stacks(StackName) VALUES('{command.Split()[1]}');");

        else if(command.StartsWith("remove"))
          SqlAccess.Execute($"DELETE FROM Stacks WHERE StackName='{command.Split()[1]}';");

        else if(command.StartsWith("edit"))
          SqlAccess.Execute($"UPDATE Stacks SET StackName = '{command.Split()[2]}' WHERE StackName = '{command.Split()[1]}';");

        else if(command == "back" || command == "0") break;
        else if(string.IsNullOrWhiteSpace(command)) continue; // Do nothing if the user presses enter
        else Console.WriteLine("Not a command. Use 'help' if required. ");

      }
    }
  }
}
