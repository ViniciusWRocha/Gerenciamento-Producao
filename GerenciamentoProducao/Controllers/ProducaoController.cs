using GerenciamentoProducao.Interfaces;
using GerenciamentoProducao.Models;
using GerenciamentoProducao.Repositories;
using GerenciamentoProducaoo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoProducaoo.Controllers
{
    public class ProducaoController : Controller
    {
        private readonly IProducaoRepository _producaoRepository;
        private readonly IFamiliaCaixilhoRepository _familiaCaixilhoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProducaoController(IProducaoRepository producaoRepository, IFamiliaCaixilhoRepository familiaCaixilhoRepository, IUsuarioRepository usuarioRepository)
        {
            _producaoRepository = producaoRepository;
            _familiaCaixilhoRepository = familiaCaixilhoRepository;
            _usuarioRepository = usuarioRepository;
        }
        private async Task<ProducaoViewModel> CriarProducaoViewModel(ProducaoViewModel? model = null)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var familias = await _familiaCaixilhoRepository.GetAllAsync();

            return new ProducaoViewModel
            {
                IdProducao = model?.IdProducao ?? 0,
                NomeProducao = model?.NomeProducao,
                DataProducao = model?.DataProducao ?? DateTime.Now,
                Produzido = model?.Produzido ?? false,
                Descricao = model?.Descricao,
                EhLiberado = model?.EhLiberado ?? false,
                Usuario = usuarios.Select(u => new SelectListItem
                {
                    Value = u.IdUsuario.ToString(),
                    Text = u.NomeUsuario
                }),
                FamiliaCaixilho = familias.Select(f => new SelectListItem
                {
                    Value = f.IdFamiliaCaixilho.ToString(),
                    Text = f.DescricaoFamilia
                })
            };
        }

        // Lista todas as produções
        public async Task<IActionResult> Index()
        {
            var lista = await _producaoRepository.GetAllAsync();
            return View(lista);
        }

        // Criar nova produção (GET)
        public async Task<IActionResult> Create()
        {
            var model = await CriarProducaoViewModel();
            return View(model);
        }

        // Criar nova produção (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProducaoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var producao = new Producao
                {
                    IdProducao = viewModel.IdProducao,
                    NomeProducao = viewModel.NomeProducao,
                    DataProducao = viewModel.DataProducao,
                    Produzido = viewModel.Produzido,
                    Descricao = viewModel.Descricao,
                    EhLiberado = viewModel.EhLiberado,
                    IdUsuario = viewModel.UsuarioId,
                    FamiliaCaixilhoId = viewModel.FamiliaCaixilhoId
                };

                await _producaoRepository.AddAsync(producao);
                return RedirectToAction(nameof(Index));
            }

            viewModel = await CriarProducaoViewModel(viewModel);
            return View(viewModel);
        }

        // Editar produção (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var producao = await _producaoRepository.GetByIdAsync(id);
            if (producao == null) return NotFound();

            var model = await CriarProducaoViewModel(new ProducaoViewModel
            {
                IdProducao = producao.IdProducao,
                NomeProducao = producao.NomeProducao,
                DataProducao = producao.DataProducao,
                Produzido = producao.Produzido,
                Descricao = producao.Descricao,
                EhLiberado = producao.EhLiberado,
                UsuarioId = producao.IdUsuario,
                FamiliaCaixilhoId = producao.FamiliaCaixilhoId
            });

            return View(model);
        }

        // Editar produção (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProducaoViewModel viewModel)
        {
            if (id != viewModel.IdProducao) return NotFound();

            if (ModelState.IsValid)
            {
                var producao = await _producaoRepository.GetByIdAsync(id);
                if (producao == null) return NotFound();

                producao.NomeProducao = viewModel.NomeProducao;
                producao.DataProducao = viewModel.DataProducao;
                producao.Produzido = viewModel.Produzido;
                producao.Descricao = viewModel.Descricao;
                producao.EhLiberado = viewModel.EhLiberado;
                producao.IdUsuario = producao.IdUsuario;
                producao.FamiliaCaixilhoId = producao.FamiliaCaixilhoId;

                await _producaoRepository.UpdateAsync(producao);
                return RedirectToAction(nameof(Index));
            }

            viewModel = await CriarProducaoViewModel(viewModel);
            return View(viewModel);
        }

        // Excluir produção (GET - confirmação)
        public async Task<IActionResult> Delete(int id)
        {
            var producao = await _producaoRepository.GetByIdAsync(id);
            if (producao == null) return NotFound();

            return View(producao);
        }

        // Excluir produção (POST - confirmado)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmado(int id)
        {
            await _producaoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        
    }
}