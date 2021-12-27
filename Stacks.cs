using System;

namespace DotNet_Flash_Study
{
  public class Stack
  {
    public Stack(int stackId, string stackName) {
      this.StackId = Convert.ToInt32(stackId);
      this.StackName = stackName;
    }

    public int StackId { get; set; }
    public string StackName { get; set; }
  }
}
