using System;

namespace DotNet_Flash_Study
{
    public class FlashCardToViewDTO
    {
      public int CardId { get; set; }
      public string Title { get; set; }
      public string Answer { get; set; }
      public int StackId { get; set; }

      // Navigation Property
      // public Stack Stack { get; set; }

      public FlashCardToViewDTO(FlashCard flashCard) {
        this.CardId = flashCard.CardId;
        this.Title = flashCard.Title;
        this.Answer = flashCard.Answer;
      }
    }
}
