namespace _00016339_Feedback_MVC.Models
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SenderId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
