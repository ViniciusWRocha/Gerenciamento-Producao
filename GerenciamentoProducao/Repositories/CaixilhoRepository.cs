using GerenciamentoProducao.Data;
using GerenciamentoProducao.Interfaces;
using GerenciamentoProducao.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoProducao.Repositories
{
    public class CaixilhoRepository : ICaixilhoRepository
    {
        private readonly GerenciamentoProdDbContext _context;
        public CaixilhoRepository(GerenciamentoProdDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Caixilho caixilho)
        {
            await _context.Caixilhos.AddAsync(caixilho);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var caixilho = await _context.Caixilhos.FindAsync(id);
            if (caixilho != null)
            {
                _context.Caixilhos.Remove(caixilho);
                _context.SaveChangesAsync();
            }
        }

        public async Task<List<Caixilho>> GetAllAsync()
        {
            return await _context.Caixilhos.ToListAsync();
        }

        public async Task<Caixilho> GetById(int id)
        {
            return await _context.Caixilhos.FindAsync(id);
        }

        public async Task UpdateAsync(Caixilho caixilho)
        {
            _context.Caixilhos.Update(caixilho);
            await _context.SaveChangesAsync();
        }
    }
}
