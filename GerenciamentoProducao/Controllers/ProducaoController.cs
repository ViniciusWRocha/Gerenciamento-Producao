using GerenciamentoProducao.Interfaces;
using GerenciamentoProducao.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoProducaoo.Controllers
{
    public class ProducaoController : Controller
    {
        public readonly IProducaoRepository _producaoRepository;
        public ProducaoController(IProducaoRepository producaoRepository)
        {
            _producaoRepository = producaoRepository;
        }


        //lista de todas as produções
        public async Task<IActionResult> Index()
        {
            var lista = await _producaoRepository.GetAllAsync();
            return View(lista);
        }


        //criar nova produção
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producao producao)
        {
            if (ModelState.IsValid)
            {
                await _producaoRepository.AddAsync(producao);
                return RedirectToAction(nameof(Index));
            }
            return View(producao);
        }

        //Editar

        public async Task<IActionResult> Edit(int id)
        {
            var producao = await _producaoRepository.GetByIdAsync(id);
            if (producao == null)
            {
                return NotFound();
            }
            return View(producao);
        }

        // Mostra os dados para confirmação de exclusão

        public async Task<IActionResult> Delete(int id)
        {
            var producao = await _producaoRepository.GetByIdAsync(id);
            if (producao == null)
            {
                return NotFound();
            }
            return View(producao);
        }

        //confirma exclusão
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmado(int id)
        {
            await _producaoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //Mostra detalhes de uma produção especifica
        public async Task<IActionResult> Details(int id)
        {
            var producao = await _producaoRepository.GetByIdAsync(id);
            if (producao == null)
            {
                return NotFound();
            }
            return View(producao);
        }
    }
}
//