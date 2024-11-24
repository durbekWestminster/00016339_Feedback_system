namespace _00016339_Feedback_system.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SenderId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
