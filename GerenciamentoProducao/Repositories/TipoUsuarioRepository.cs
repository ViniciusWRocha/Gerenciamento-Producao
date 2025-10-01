using GerenciamentoProducao.Data;
using GerenciamentoProducao.Interfaces;
using GerenciamentoProducao.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoProducao.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly GerenciamentoProdDbContext _context;
        public TipoUsuarioRepository(GerenciamentoProdDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TipoUsuario tipoUsuario)
        {
            await _context.TipoUsuarios.AddAsync(tipoUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tipoUsuario = await _context.TipoUsuarios.FindAsync(id);
            if (tipoUsuario != null)
            {
                _context.TipoUsuarios.Remove(tipoUsuario);
                _context.SaveChangesAsync();
            }
        }

        public async Task<List<TipoUsuario>> GetAllAsync()
        {
            return await _context.TipoUsuarios.ToListAsync();
        }

        public async Task<TipoUsuario> GetById(int id)
        {
            return await _context.TipoUsuarios.FindAsync(id);

        }

        public async  Task UpdateAsync(TipoUsuario tipoUsuario)
        {
            _context.TipoUsuarios.Update(tipoUsuario);
            await _context.SaveChangesAsync();
        }
    }
}
