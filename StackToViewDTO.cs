using System;

namespace DotNet_Flash_Study
{
  public class StackToViewDTO
  {
    public string StackName { get; set; }

    public StackToViewDTO(Stack stack) {
      this.StackName = stack.StackName;
    }
  }
}
