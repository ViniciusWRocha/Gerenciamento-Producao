using GerenciamentoProducao.Interfaces;
using GerenciamentoProducao.Models;
using GerenciamentoProducao.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoProducaoo.Controllers
{
    public class ObraController : Controller
    {
        private readonly IObraRepository _obraRepository;
        public ObraController(IObraRepository obraRepository)
        {
            _obraRepository = obraRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lista = await _obraRepository.GetAllAsync();
            return View(lista);
        }
        [HttpGet]

        public IActionResult Create() => View();
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Obra obra)
        {
            if (ModelState.IsValid)
            {
                await _obraRepository.AddAsync(obra);
                return RedirectToAction(nameof(Index));
            }
            return View(obra);
        }
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id)
        {
            var obra = await _obraRepository.GetById(id);
            if (obra == null) return NotFound();
            return View(obra);
        }
        //[Authorize(Roles = "Admin")]

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Obra obra)
        {
            if (id != obra.IdObra)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _obraRepository.UpdateAsync(obra);
                return RedirectToAction(nameof(Index));
            }
            return View(obra);
        }
        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _obraRepository.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }
        //[Authorize(Roles = "Admin")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _obraRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }

}
