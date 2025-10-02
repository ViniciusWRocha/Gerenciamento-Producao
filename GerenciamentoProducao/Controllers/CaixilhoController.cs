using GerenciamentoProducao.Interfaces;
using GerenciamentoProducao.Models;
using GerenciamentoProducaoo.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//foi o ryan que fez
namespace GerenciamentoProducaoo.Controllers
{
    public class CaixilhoController : Controller
    {
        private readonly ICaixilhoRepository _caixilhoRepository;
        private readonly ITipoCaixilhoRepository _tipoCaixilhoRepository;
        private readonly IFamiliaCaixilhoRepository _familiaCaixilhoRepository;
        private readonly IObraRepository _obraRepository;
        public CaixilhoController(ICaixilhoRepository caixilhoRepository, ITipoCaixilhoRepository tipoCaixilhoRepository, IFamiliaCaixilhoRepository familiaCaixilhoRepository, IObraRepository obraRepository)
        {
            _caixilhoRepository = caixilhoRepository;
            _tipoCaixilhoRepository = tipoCaixilhoRepository;
            _familiaCaixilhoRepository = familiaCaixilhoRepository;
            _obraRepository = obraRepository;
        }

        private async Task<CaixilhoViewModel> CriarCaixilhoViewModel(CaixilhoViewModel? model = null)
        {
            var tipoCaixilhos = await _tipoCaixilhoRepository.GetAllAsync();
            var familiaCaixilhos = await _familiaCaixilhoRepository.GetAllAsync();
            var obras = await _obraRepository.GetAllAsync();
            return new CaixilhoViewModel
            {
                IdCaixilho = model?.IdCaixilho ?? 0,
                NomeCaixilho = model?.NomeCaixilho,
                Largura = model?.Largura ?? 0,
                Altura = model?.Altura ?? 0,
                Quantidade = model?.Quantidade ?? 0,
                PesoUnitario = model?.PesoUnitario ?? 0,
                ObraId = model?.ObraId ?? 0,
                IdFamiliaCaixilho = model?.IdFamiliaCaixilho ?? 0,
                IdTipoCaixilho = model?.IdTipoCaixilho ?? 0,
                //Dropdrowns 
                Obra = obras.Select(o => new SelectListItem
                {
                    Value = o.IdObra.ToString(),
                    Text = o.Nome
                }),
                FamiliaCaixilho = familiaCaixilhos.Select(f => new SelectListItem
                {
                    Value = f.IdFamiliaCaixilho.ToString(),
                    Text = f.DescricaoFamilia
                }),
                TipoCaixilho = tipoCaixilhos.Select(t => new SelectListItem
                {
                    Value = t.IdTipoCaixilho.ToString(),
                    Text = t.DescricaoCaixilho
                })
            };
        }
        public async Task<IActionResult> Index()
        {
            var caixilhos = await _caixilhoRepository.GetAllAsync();
            return View(caixilhos);
        }

        public async Task<IActionResult> Create()
        {
            var model = await CriarCaixilhoViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CaixilhoViewModel caixilhoViewModel)
        {
            if (!ModelState.IsValid)
            {
                // Mapear ViewModel para Model
                var caixilho = new Caixilho
                {
                    NomeCaixilho = caixilhoViewModel.NomeCaixilho,
                    Largura = caixilhoViewModel.Largura,
                    Altura = caixilhoViewModel.Altura,
                    Quantidade = caixilhoViewModel.Quantidade,
                    PesoUnitario = caixilhoViewModel.PesoUnitario,
                    ObraId = caixilhoViewModel.ObraId,
                    IdFamiliaCaixilho = caixilhoViewModel.IdFamiliaCaixilho,
                    IdTipoCaixilho = caixilhoViewModel.IdTipoCaixilho
                };
                await _caixilhoRepository.AddAsync(caixilho);
                return RedirectToAction(nameof(Index));
            }
            var model = await CriarCaixilhoViewModel(caixilhoViewModel);
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var caixilho = await _caixilhoRepository.GetById(id);
            var tipoCaixilhos = await _tipoCaixilhoRepository.GetAllAsync();
            var familiaCaixilhos = await _familiaCaixilhoRepository.GetAllAsync();
            var obras = await _obraRepository.GetAllAsync();

            if (caixilho == null) return NotFound();
            var model = new CaixilhoViewModel
            {
                IdCaixilho = caixilho.IdCaixilho,
                NomeCaixilho = caixilho.NomeCaixilho,
                Largura = caixilho.Largura,
                Altura = caixilho.Altura,
                Quantidade = caixilho.Quantidade,
                PesoUnitario = caixilho.PesoUnitario,

                ObraId = caixilho.ObraId,
                IdFamiliaCaixilho = caixilho.IdFamiliaCaixilho,
                IdTipoCaixilho = caixilho.IdTipoCaixilho,
                //Dropdrowns 
                Obra = obras.Select(o => new SelectListItem
                {
                    Value = o.IdObra.ToString(),
                    Text = o.Nome
                }),
                FamiliaCaixilho = familiaCaixilhos.Select(f => new SelectListItem
                {
                    Value = f.IdFamiliaCaixilho.ToString(),
                    Text = f.DescricaoFamilia
                }),
                TipoCaixilho = tipoCaixilhos.Select(t => new SelectListItem
                {
                    Value = t.IdTipoCaixilho.ToString(),
                    Text = t.DescricaoCaixilho
                })
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CaixilhoViewModel viewModel)
        {
            if (id != viewModel.IdCaixilho)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) {
                var caixilho = await _caixilhoRepository.GetById(id);
                if (caixilho == null) { return NotFound(); }
                caixilho.NomeCaixilho = viewModel.NomeCaixilho;
                caixilho.Largura = viewModel.Largura;
                caixilho.Altura = viewModel.Altura;
                caixilho.Quantidade = viewModel.Quantidade;
                caixilho.PesoUnitario = viewModel.PesoUnitario;
                caixilho.ObraId = viewModel.ObraId;
                caixilho.IdFamiliaCaixilho = viewModel.IdFamiliaCaixilho;
                caixilho.IdTipoCaixilho = viewModel.IdTipoCaixilho;
                await _caixilhoRepository.UpdateAsync(caixilho);
                return RedirectToAction(nameof(Index));
            }
            //Se o modelo não estiver válido, recarrega o formulário
            // podemos trocar para outra coisa invés disso
            viewModel = await CriarCaixilhoViewModel(viewModel);
            return View(viewModel);
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _caixilhoRepository.GetById(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _caixilhoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
