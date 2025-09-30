using GerenciamentoProducao.Data;
using GerenciamentoProducao.Interfaces;
using GerenciamentoProducao.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoProducao.Repositories
{
    public class TipoCaixilhoRepository : ITipoCaixilhoRepository
    {
        private readonly GerenciamentoProdDbContext _context;
        public TipoCaixilhoRepository(GerenciamentoProdDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TipoCaixilho tipoCaixilho)
        {
            await _context.TipoCaixilhos.AddAsync(tipoCaixilho);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Tipocaixilho = await _context.TipoCaixilhos.FindAsync(id);
            if (Tipocaixilho  != null)
            {
                _context.TipoCaixilhos.Remove(Tipocaixilho);
                _context.SaveChangesAsync();
            }
        }

        public async Task<List<TipoCaixilho>> GetAllAsync()
        {
            return await _context.TipoCaixilhos.ToListAsync();

        }

        public async Task<TipoCaixilho> GetById(int id)
        {
            return await _context.TipoCaixilhos.FindAsync(id);

        }

        public async  Task UpdateAsync(TipoCaixilho tipoCaixilho)
        {
            _context.TipoCaixilhos.Update(tipoCaixilho);
            await _context.SaveChangesAsync();
        }
    }
}
