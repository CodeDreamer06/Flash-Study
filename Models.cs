// using System.Collections.Generic;

namespace DotNet_Flash_Study
{
  public class Stack
  {
    public int StackId { get; set; }
    public string StackName { get; set; }
  }

  public class FlashCard
  {
    public int CardId { get; set; }
    public string Title { get; set; }
    public string Answer { get; set; }
    
    // Foreign Key
    public int StackId { get; set; }

    // Navigation Property
    public Stack Stack { get; set; }
  }
}
