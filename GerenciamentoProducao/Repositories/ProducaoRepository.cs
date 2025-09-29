using GerenciamentoProducao.Data;
using GerenciamentoProducao.Interfaces;
using GerenciamentoProducao.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoProducao.Repositories
{
    public class ProducaoRepository : IProducaoRepository
    {
        private readonly GerenciamentoProdDbContext _context;
        public ProducaoRepository(GerenciamentoProdDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Producao producao)
        {
            await _context.Producoes.AddAsync(producao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var producao = await _context.Producoes.FindAsync(id);
            if (producao == null)
            {
                _context.Producoes.Remove(producao);
                _context.SaveChangesAsync();
            }
        }

        public async Task<List<Producao>> GetAllAsync()
        {
            return await _context.Producoes.ToListAsync();

        }

        public async Task<Producao> GetByIdAsync(int id)
        {
            return await _context.Producoes.FindAsync(id);
        }

        public async Task UpdateAsync(Producao producao)
        {
            _context.Producoes.Update(producao);
            await _context.SaveChangesAsync();
        }
    }
}
