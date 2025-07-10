using EQTAXTechnicalTestApp.Domain.Entities;
using EQTAXTechnicalTestApp.Domain.Repositories;
using EQTAXTechnicalTestApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EQTAXTechnicalTestApp.Infrastructure.Data.Repositories
{ 
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
        }


        public async Task AddRefreshToken(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetRefreshToken(string token)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task RevokeRefreshToken(string token)
        {
            var refreshToken = await GetRefreshToken(token);
            if (refreshToken != null)
            {
                _context.RefreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
            }
        }
    }
}