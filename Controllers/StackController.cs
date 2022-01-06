using System;
using System.Collections.Generic;
using FlashStudy.Utilities;
using FlashStudy.Models;
using FlashStudy.DTOs;

namespace FlashStudy.Controllers
{
  class StackController {
    public static string command = "";

    public static void Start() {
      Console.WriteLine(Help.stackMessage);
      while(true) {
        command = Console.ReadLine().ToLower().Trim();

        if(command == "show") {
          var stacks = SqlAccess.ReadStacks();
          var stackViews = new List<StackToViewDTO>();
          for(int i = 0; i < stacks.Count; i++)
            stackViews.Add(new StackToViewDTO(stacks[i]));
          for (int i = 0; i < stackViews.Count; i++)
            Console.WriteLine($"{i + 1}   {stackViews[i].StackName}");
        }

        else if(command.StartsWith("add"))
          SqlAccess.AddStack(new Stack(command.Split()[1]));

        else if(command.StartsWith("remove"))
          SqlAccess.RemoveStack(new Stack(command.Split()[1]));

        else if(command.StartsWith("edit"))
          SqlAccess.EditStack(command.Split()[1], command.Split()[2]);

        else if(command == "back" || command == "0") break;
        else if(command == "help") Console.WriteLine(Help.stackMessage);
        else if(string.IsNullOrWhiteSpace(command)) continue; // Do nothing if the user presses enter
        else Console.WriteLine("Not a command. Use 'help' if required. ");
      }
      Console.WriteLine(Help.mainMenu);
    }
  }
}
