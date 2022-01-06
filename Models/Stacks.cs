using System;
using FlashStudy.Utilities;

namespace FlashStudy.Models
{
  public class Stack
  {
    public Stack(string stackName) {
      this.StackName = stackName;
    }

    public Stack(int stackId, string stackName) {
      this.StackId = Convert.ToInt32(stackId);
      this.StackName = stackName;
    }

    public int StackId { get; set; }
    public string StackName { get; set; }
  }
}
