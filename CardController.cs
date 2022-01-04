using System;
using System.Collections.Generic;

namespace DotNet_Flash_Study
{
  class CardController {
    public static string command = "";

    public static void Start()
    {
      Console.WriteLine(Help.cardMessage);
      while(true)
      {
        command = Console.ReadLine().ToLower().Trim();

        if(command == "show") {
          var flashCards = SqlAccess.ReadFlashCards();
          var flashCardsViews = new List<FlashCardToViewDTO>();
          for(int i = 0; i < flashCards.Count; i++)
              flashCardsViews.Add(new FlashCardToViewDTO(flashCards[i]));
          for(int i = 0; i < flashCardsViews.Count; i++) {
            var view = flashCardsViews[i];
            Console.WriteLine($"{i + 1}   {view.Title}     {view.Answer}");
          }
        }

        else if(command == "add")
        {
          var cardProperties = new string[] { "Title: ", "Answer: ", "StackId: " };
          var cardData = new string[3];
          for (int i = 0; i < cardData.Length; i++) {
            Console.Write(cardProperties[i]);
            cardData[i] = Console.ReadLine().Trim();
          }

          SqlAccess.AddCard(new FlashCard(cardData));
        }

        else if(command.StartsWith("remove"))
          SqlAccess.RemoveCard(command.Split()[1]);

        else if(command == "back" || command == "0") break;
        else if(command == "help") Console.WriteLine(Help.cardMessage);
        else if(string.IsNullOrWhiteSpace(command)) continue;
        else Console.WriteLine("Not a command. Use 'help' if required. ");

      }
      Console.WriteLine(Help.mainMenu);
    }
  }
}
