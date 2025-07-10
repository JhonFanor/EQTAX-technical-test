using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using EQTAXTechnicalTestApp.Domain.Entities;
using EQTAXTechnicalTestApp.Domain.Repositories;
using EQTAXTechnicalTestApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EQTAXTechnicalTestApp.Infrastructure.Data.Repositories
{
    public class LogProcessRepository : ILogProcessRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LogProcessRepository> _logger;

        public LogProcessRepository(AppDbContext context, ILogger<LogProcessRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<LogProcess?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.LogProcess
                    .AsNoTracking()
                    .FirstOrDefaultAsync(l => l.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo LogProcess por ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<LogProcess>> GetAllAsync()
        {
            try
            {
                return await _context.LogProcess
                    .AsNoTracking()
                    .OrderBy(l => l.Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todos los LogProcess");
                throw;
            }
        }

        public async Task<PaginatedResult<LogProcess>> GetPaginatedAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            try
            {
                var query = _context.LogProcess.AsNoTracking();

                // Puedes aplicar un filtro por filename o status si quieres
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(l =>
                        l.OriginalFileName.Contains(searchTerm) ||
                        (l.NewFileName != null && l.NewFileName.Contains(searchTerm)) ||
                        l.Status.Contains(searchTerm));
                }

                var totalItems = await query.CountAsync();
                var items = await query
                    .OrderBy(l => l.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedResult<LogProcess>
                {
                    Items = items,
                    TotalCount = totalItems,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo LogProcess paginados. Página: {PageNumber}, Tamaño: {PageSize}", pageNumber, pageSize);
                throw;
            }
        }

        public async Task<int> AddAsync(LogProcess entity)
        {
            try
            {
                await _context.LogProcess.AddAsync(entity);
                await _context.SaveChangesAsync();
                _context.Entry(entity).State = EntityState.Detached;

                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error agregando nuevo LogProcess");
                throw;
            }
        }

        public async Task UpdateAsync(LogProcess entity)
        {
            try
            {
                var existingEntity = await _context.LogProcess.FirstOrDefaultAsync(l => l.Id == entity.Id);
                if (existingEntity == null)
                {
                    throw new KeyNotFoundException($"LogProcess con ID {entity.Id} no encontrado");
                }

                existingEntity.OriginalFileName = entity.OriginalFileName;
                existingEntity.NewFileName = entity.NewFileName;
                existingEntity.Status = entity.Status;
                existingEntity.DateProcess = entity.DateProcess;

                await _context.SaveChangesAsync();
                _context.Entry(existingEntity).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando LogProcess con ID: {Id}", entity.Id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.LogProcess.FindAsync(id);
                if (entity != null)
                {
                    _context.LogProcess.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando LogProcess con ID: {Id}", id);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                return await _context.LogProcess.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error contando LogProcess");
                throw;
            }
        }
    }
}
