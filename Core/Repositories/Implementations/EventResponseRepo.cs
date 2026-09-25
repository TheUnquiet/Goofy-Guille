using Core.Context;
using Core.Entities;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Repositories.Implementations
{
    public class EventResponseRepo : IEventResponseRepo
    {
        private readonly BasketBallClubDbContext _context;

        public EventResponseRepo(BasketBallClubDbContext context)
        {
            _context = context;
        }

        public async Task<EventResponse?> GetAsync(Guid eventId, Guid userId)
        {
            return await _context.EventResponses.FirstOrDefaultAsync(x => x.EventId == eventId && x.UserId == userId);
        }

        public async Task<List<EventResponse>> GetByEventIdAsync(Guid eventId)
        {
            return await _context.EventResponses.Where(x => x.EventId == eventId).ToListAsync();
        }

        public async Task<List<EventResponse>> GetByUserIdAsync(Guid userId)
        {
            return await _context.EventResponses.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<EventResponse> AddAsync(EventResponse response)
        {
            var entry = await _context.EventResponses.AddAsync(response);
            return entry.Entity;    
        }

        public async Task DeleteAsync(Guid eventId, Guid userId)
        {
            var response = await GetAsync(eventId, userId);
            if (response is not null) _context.EventResponses.Remove(response);
        }

        public Task<EventResponse> UpdateAsync(EventResponse response)
        {
            var entry = _context.EventResponses.Update(response);
            return Task.FromResult(entry.Entity);
        }
    
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}