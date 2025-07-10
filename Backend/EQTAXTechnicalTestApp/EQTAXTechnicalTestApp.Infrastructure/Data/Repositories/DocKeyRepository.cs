using EQTAXTechnicalTestApp.Application.DTOs.Responses;
using EQTAXTechnicalTestApp.Domain.Entities;
using EQTAXTechnicalTestApp.Domain.Repositories;
using EQTAXTechnicalTestApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EQTAXTechnicalTestApp.Infrastructure.Data.Repositories
{
    public class DocKeyRepository : IDocKeyRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DocKeyRepository> _logger;

        public DocKeyRepository( AppDbContext context, ILogger<DocKeyRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<DocKey?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.DocKey
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo DocKey por ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<DocKey>> GetAllAsync()
        {
            try
            {
                return await _context.DocKey
                    .AsNoTracking()
                    .OrderBy(d => d.Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todos los DocKeys");
                throw;
            }
        }

        public async Task<PaginatedResult<DocKey>> GetPaginatedAsync( int pageNumber, int pageSize, string? searchTerm = null)
        {
            try
            {
                var query = _context.DocKey.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(d =>
                        d.DocName.Contains(searchTerm) ||
                        d.Key.Contains(searchTerm));
                }

                var totalItems = await query.CountAsync();
                var items = await query
                    .OrderBy(d => d.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedResult<DocKey>
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
                _logger.LogError(ex,
                    "Error obteniendo DocKeys paginados. Página: {PageNumber}, Tamaño: {PageSize}",
                    pageNumber, pageSize);
                throw;
            }
        }

        public async Task<int> AddAsync(DocKey entity)
        {
            try
            {
                await _context.DocKey.AddAsync(entity);
                await _context.SaveChangesAsync();

                _context.Entry(entity).State = EntityState.Detached;

                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error agregando nuevo DocKey");
                throw;
            }
        }

        public async Task UpdateAsync(DocKey entity)
        {
            try
            {
                var existingEntity = await _context.DocKey
                    .FirstOrDefaultAsync(d => d.Id == entity.Id);

                if (existingEntity == null)
                {
                    throw new KeyNotFoundException($"DocKey con ID {entity.Id} no encontrado");
                }

                existingEntity.DocName = entity.DocName;
                existingEntity.Key = entity.Key;

                await _context.SaveChangesAsync();
                _context.Entry(existingEntity).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando DocKey con ID: {Id}", entity.Id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.DocKey.FindAsync(id);
                if (entity != null)
                {
                    _context.DocKey.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando DocKey con ID: {Id}", id);
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                return await _context.DocKey.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error contando DocKeys");
                throw;
            }
        }

    }
}