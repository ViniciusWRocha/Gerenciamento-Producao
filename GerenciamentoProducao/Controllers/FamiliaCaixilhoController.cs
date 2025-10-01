using GerenciamentoProducao.Data;
using GerenciamentoProducao.Interfaces;
using GerenciamentoProducao.Models;
using GerenciamentoProducao.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoProducao.Controllers
{
    public class FamiliaCaixilhoController : Controller
    {

        private readonly IFamiliaCaixilhoRepository _familiaCaixilhoRepository;
        public FamiliaCaixilhoController(IFamiliaCaixilhoRepository familiaCaixilhoRepository)
        {
            _familiaCaixilhoRepository = familiaCaixilhoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _familiaCaixilhoRepository.GetAllAsync();
            return View(lista);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FamiliaCaixilho familia)
        {
            if (ModelState.IsValid)
            {
                await _familiaCaixilhoRepository.AddAsync(familia);
                return RedirectToAction(nameof(Index));
            }
            return View(familia);
        }
        public async Task<ActionResult> Edit(int id)
        {
            var familia = await _familiaCaixilhoRepository.GetByIdAsync(id);
            if (familia == null)
            {
                return NotFound();
            }
            return View(familia);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FamiliaCaixilho familia)
        {
            if (id != familia.IdFamiliaCaixilho)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _familiaCaixilhoRepository.UpdateAsync(familia);
                return RedirectToAction(nameof(Index));
            }
            return View(familia);
        }
        public async Task<IActionResult> Delete(int id)
        {
            
            var item = await _familiaCaixilhoRepository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _familiaCaixilhoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        //public async Task<IActionResult> Details(int id)
        //{
        //    var familia = await _familiaCaixilhoRepository.GetByIdAsync(id);
        //    if (familia == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(familia);
        //}
    }
}
