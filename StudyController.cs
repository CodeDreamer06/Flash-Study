using System;
using System.Collections.Generic;

namespace DotNet_Flash_Study
{
  class StudyController {
    public static List<Stack> ShowStackList() {
      Console.WriteLine("Which Stack would you like to study?");
      List<Stack> stacks = SqlAccess.ReadStacks();
      var stackViews = new List<StackToViewDTO>();
      for(int i = 0; i < stacks.Count; i++)
        stackViews.Add(new StackToViewDTO(stacks[i]));
      for (int i = 0; i < stackViews.Count; i++)
        Console.WriteLine($"{i + 1}   {stackViews[i].StackName}");
      return stacks;
    }

    public static void Start() {
      var stacks = ShowStackList();
      string command = Console.ReadLine().ToLower().Trim();
      while(string.IsNullOrWhiteSpace(command)) {
        Console.WriteLine("Please enter a valid number");
        command = Console.ReadLine().ToLower().Trim();
      }
      int stackId = Convert.ToInt32(command);
      int absoluteStackId = 0;
      try {
        absoluteStackId = stacks[stackId - 1].StackId;
      }

      catch {
        Console.WriteLine("Please choose a valid option from the list.");
        StudyController.Start();
      }

      var flashCards = SqlAccess.ReadFlashCards(absoluteStackId);

      int correctCount = 0;
      for (int i = 0; i < flashCards.Count; i++) {
          Console.WriteLine(flashCards[i].Title);
          string userAnswer = Console.ReadLine().ToLower().Trim();
          string correctAnswer = flashCards[i].Answer.ToLower();
          if(userAnswer == correctAnswer) correctCount += 1;
        }
      decimal score = Decimal.Divide(correctCount, flashCards.Count) * 100;
      Console.WriteLine("Today's Score: " + score + "%");
      Console.WriteLine(Help.mainMenu);
    }
  }
}
