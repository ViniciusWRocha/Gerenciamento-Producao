using GerenciamentoProducao.Models;

namespace GerenciamentoProducao.Interfaces
{
    public interface IProducaoRepository
    {
        Task<List<Producao>> GetAllAsync();
      
         Task AddAsync(Producao producao);
         Task UpdateAsync(Producao producao);
         Task DeleteAsync(int id);
        Task<Producao> GetByIdAsync(int id);
    }
}
