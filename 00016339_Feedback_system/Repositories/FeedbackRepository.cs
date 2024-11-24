using _00016339_Feedback_system.Data;
using _00016339_Feedback_system.Models;
using Microsoft.EntityFrameworkCore;

namespace _00016339_Feedback_system.Repositories
{
    public class FeedbackRepository : IRepository<Feedback>
    {
        private readonly FeedbackDbContext _context;
        public FeedbackRepository(FeedbackDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Feedback entity)
        {
            await _context.Feedbacks.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Feedbacks.FindAsync(id);
            if (entity != null)
            {
                _context.Feedbacks.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Feedback>> GetAllAsync() => await _context.Feedbacks.ToArrayAsync();

        public async Task<Feedback> GetByIdAsync(int id)
        {
            return await _context.Feedbacks.FindAsync(id);
        }

        public async Task UpdateAsync(Feedback entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
