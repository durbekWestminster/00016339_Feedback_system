using _00016339_Feedback_system.Models;

namespace _00016339_Feedback_system.DTO
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Sender Sender { get; set; }
        public DateTime DateCreated { get; set; }

        public FeedbackDto()
        {
        }
    }
}
