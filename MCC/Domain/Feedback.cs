using System;
using System.ComponentModel.DataAnnotations;


namespace MCC.Domain
{
    public class Feedback
    {
      public Guid FeedbackId{ get; set; }
      public int FeedbackNumber{ get; set; }
      public DateTime FeedbackDate{ get; set; }
      public DateTime ModifyDate{ get; set; }
      public string Title{ get; set; }
      public DateTime CheckIn { get; set; }
      public DateTime CheckOut { get; set; }
      public FeedbackType Type { get; set; }
      public FeedbackPriority Priority { get; set; }
      public FeedbackStatus Status{ get; set; }
      public FeedbackCategory Category { get; set; }
      public string ResourceGroup{ get; set; }
      public Guid UserId { get; set; }
      public Guid? HotelId { get; set; }
      public Guid? TransportCompanyId { get; set; }
      public string Subject{ get; set; }
      public string Comments{ get; set; }
      public string RoomNumber{ get; set; }
      [Required]
      public string ArrivalFlightNumber{ get; set; }
      public string DepartureFlightNumber{ get; set; }
      public string FirstName{ get; set; }
      public string LastName{ get; set; }
      public string BranchId { get; set; }
      public string ImageUpload{ get; set; }
      public bool HotelAttention{ get; set; }
      public Guid FeedbackTopicId { get; set; }
      public Guid AirlineId { get; set; }

    }
}