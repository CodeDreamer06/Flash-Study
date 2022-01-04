using System;

namespace DotNet_Flash_Study
{
    public class FlashCardToViewDTO
    {
      public string Title { get; set; }
      public string Answer { get; set; }

      public FlashCardToViewDTO(FlashCard flashCard) {
        this.Title = flashCard.Title;
        this.Answer = flashCard.Answer;
      }
    }
}
