using System;

namespace DotNet_Flash_Study
{
  class StudyController {
    public static void Start() {
      Console.WriteLine("Which Stack would you like to study?");
      SqlAccess.ReadTable("Stacks");
      string stackId = Console.ReadLine().ToLower().Trim();
      var flashCards = SqlAccess.ReadFlashCards(stackId);
      int correctCount = 0;
      for (int i = 0; i < flashCards.Count; i++) {
          Console.WriteLine(flashCards[i].Title);
          string userAnswer = Console.ReadLine().ToLower().Trim();
          string correctAnswer = flashCards[i].Answer.ToLower();
          if(userAnswer == correctAnswer) correctCount += 1;
        }
      decimal score = Decimal.Divide(correctCount, flashCards.Count) * 100;
      Console.WriteLine("Today's Score: " + score + "%");
    }
  }
}
