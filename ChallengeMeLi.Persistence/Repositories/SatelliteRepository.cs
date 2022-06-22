using ChallengeMeLi.Application.Repositories;
using ChallengeMeLi.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeMeLi.Persistence.Repositories
{
    public class SatelliteRepository : ISatelliteRepository
    {
        private readonly AppDbContext _context;

        public SatelliteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Satellite entity)
        {
            await _context.Satellites.AddAsync(entity);
            await SaveChangesAsync();
        }

        public void Delete(Satellite entity)
        {
            _context.Remove(entity);
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Satellites.AnyAsync(e => e.Name == name);
        }

        public async Task<List<Satellite>> GetAllAsync()
        {
            return await _context.Satellites.ToListAsync();
        }

        public async Task<Satellite> GetByNameAsync(string name)
        {
            return await _context.FindAsync<Satellite>(name);
        }

        public void Attach(Satellite entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}