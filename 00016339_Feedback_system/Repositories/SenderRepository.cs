using _00016339_Feedback_system.Data;
using _00016339_Feedback_system.Models;
using Microsoft.EntityFrameworkCore;

namespace _00016339_Feedback_system.Repositories
{
    public class SenderRepository : IRepository<Sender>
    {
        private readonly FeedbackDbContext _context;
        public SenderRepository(FeedbackDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Sender entity)
        {
            await _context.Senders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Senders.FindAsync(id);
            if (entity != null)
            {
                _context.Senders.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Sender>> GetAllAsync() => await _context.Senders.ToArrayAsync();

        public async Task<Sender> GetByIdAsync(int id)
        {
            return await _context.Senders.FindAsync(id);
        }

        public async Task UpdateAsync(Sender entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
