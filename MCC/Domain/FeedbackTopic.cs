using System;
namespace MCC.Domain
{
    public class FeedbackTopic
    {
      public static Guid Compliment = Guid.Parse("D4CDCBC3-54BE-4C84-A19D-EF38D8A8A712");
      public Guid FeedbackTopicId {get; set;}
      public int AutoNumber {get; set;}
      public string Name {get; set;}
      public FeedbackCategory Category { get; set;}
      public string Group{get; set;}
      public FeedbackPriority Priority { get; set; }
    }
}