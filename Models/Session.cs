using System;

namespace FlashStudy.Models
{
  public class Session
  {
    public Session(int score, int stackId) {
      this.Score = score;
      this.StackId = stackId;
    }

    public Session(int sessionId, string createdOn, int score, int stackId) {
      this.SessionId = sessionId;
      this.CreatedOn = createdOn;
      this.Score = score;
      this.StackId = stackId;
    }

    public int SessionId { get; set; }
    public string CreatedOn { get; set; }
    public int Score { get; set; }
    public int StackId { get; set; }
  }
}
