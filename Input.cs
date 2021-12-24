using System;

namespace DotNet_Flash_Study
{
  class Input
  {
    public static string helpMessage = @"
Main Menu
Type 1 to Study
Type 2 to Manage Stacks
Type 3 to Manage FlashCards
Type 0 to Exit";

    // TODO: Implement the usage of this enum
    public enum MainCommands
    {
      Study = 1,
      ManageStacks = 2,
      ManageFlashCards = 3
    };

    public static string command = "";

    public static void Start() {
      while(true) {
        Console.WriteLine(helpMessage);
        command = Console.ReadLine().ToLower().Trim();

        if(command == "exit" || command == "0") break;

        else if(command == "help") Console.WriteLine(helpMessage);

        else if(command == "2") StackController.Start();

        else if(string.IsNullOrWhiteSpace(command)) continue; // Do nothing if the user presses enter
        else Console.WriteLine("Not a command. Use 'help' if required. ");
      }
    }
  }
}
