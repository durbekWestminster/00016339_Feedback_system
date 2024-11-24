using _00016339_Feedback_system.Models;
using Microsoft.EntityFrameworkCore;

namespace _00016339_Feedback_system.Data
{
    public class FeedbackDbContext : DbContext
    {
        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : base(options) {}
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}