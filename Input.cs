using System;

namespace DotNet_Flash_Study
{
  class Input
  {
    public static string command = "";

    public static void Start() {
      Console.WriteLine(Help.mainMenu);
      while(true) {
        command = Console.ReadLine().ToLower().Trim();

        if(command == "exit" || command == "0") break;
        else if(command == "help") Console.WriteLine(Help.mainMenu);

        else if(command == "1") StudyController.Start();
        else if(command == "2") StackController.Start();
        else if(command == "3") CardController.Start();

        else if(string.IsNullOrWhiteSpace(command)) continue;
        else Console.WriteLine("Not a command. Use 'help' if required. ");
      }
    }
  }
}
