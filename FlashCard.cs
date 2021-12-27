using System;

namespace DotNet_Flash_Study
{
    public class FlashCard
    {
      public FlashCard(string[] properties) {
        this.Title = properties[0];
        this.Answer = properties[1];
        this.StackId = Convert.ToInt32(properties[2]);
      }

      public FlashCard(string title, string answer, string stackId) {
        this.Title = title;
        this.Answer = answer;
        this.StackId = Convert.ToInt32(stackId);
      }

      public FlashCard(string cardId, string title, string answer, string stackId) {
        this.CardId = Convert.ToInt32(cardId);
        this.Title = title;
        this.Answer = answer;
        this.StackId = Convert.ToInt32(stackId);
      }

      public int CardId { get; set; }
      public string Title { get; set; }
      public string Answer { get; set; }

      // Foreign Key
      public int StackId { get; set; }

      // Navigation Property
      // public Stack Stack { get; set; }
    }
}
