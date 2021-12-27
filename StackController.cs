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
      Console.WriteLine(stackMessage);
      while(true) {
        command = Console.ReadLine().ToLower().Trim();

        if(command == "show")
          SqlAccess.ReadTable("Stacks");

        else if(command.StartsWith("add"))
          SqlAccess.AddStack(command.Split()[1]);

        else if(command.StartsWith("remove"))
          SqlAccess.RemoveStack(command.Split()[1]);

        else if(command.StartsWith("edit"))
          SqlAccess.EditStack(command.Split()[1], command.Split()[2]);

        else if(command == "back" || command == "0") break;
        else if(command == "help") Console.WriteLine(stackMessage);
        else if(string.IsNullOrWhiteSpace(command)) continue; // Do nothing if the user presses enter
        else Console.WriteLine("Not a command. Use 'help' if required. ");

      }
    }
  }
}
