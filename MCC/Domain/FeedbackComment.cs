using System;

namespace MCC.Domain
{
    public class FeedbackComment
    {
        public Guid FeedbackCommentId { get; set; }
        public Guid FeedbackId { get; set; }
        public DateTime DateCreated { get; set; }
        public FeedbackStatus FeedbackStatus { get; set; }
        public string ResponseComment { get; set; }
        public string UserName{ get; set; }
    }
}