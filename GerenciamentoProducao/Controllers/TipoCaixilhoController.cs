using GerenciamentoProducao.Interfaces;
using GerenciamentoProducao.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoProducaoo.Controllers
{
    public class TipoCaixilhoController : Controller
    {
        private readonly ITipoCaixilhoRepository _tipoCaixilhoRepository;
        public TipoCaixilhoController(ITipoCaixilhoRepository tipoCaixilhoRepository)
        {
            _tipoCaixilhoRepository = tipoCaixilhoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _tipoCaixilhoRepository.GetAllAsync();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoCaixilho tipoCaixilho)
        {
            if (ModelState.IsValid)
            {
                await _tipoCaixilhoRepository.AddAsync(tipoCaixilho);
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCaixilho);
        }
        //[HttpPut]
        public async Task<IActionResult> Edit(int id)
        {
            var tipoCaixilho = await _tipoCaixilhoRepository.GetById(id);
            if (tipoCaixilho == null) return NotFound();
            return View(tipoCaixilho);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TipoCaixilho tipoCaixilho)
        {
            if (id != tipoCaixilho.IdTipoCaixilho)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _tipoCaixilhoRepository.UpdateAsync(tipoCaixilho);
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCaixilho);
        }
        //[Authorize(Roles = "Admin
        public async Task<IActionResult> Delete(int id) { 
            var item = await _tipoCaixilhoRepository.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }
        //autorize aqui tbm

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tipoCaixilhoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
