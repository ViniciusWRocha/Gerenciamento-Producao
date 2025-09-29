using System.Security.Claims;
using GerenciamentoProducao.Interfaces;
using GerenciamentoProducao.Models;
using GerenciamentoProducao.Repositories;
using GerenciamentoProducaoo.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace GerenciamentoProducao.Controllers;
//[Authorize(Roles = "Administrador,Gerente")]
public class UsuarioController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository,
            ITipoUsuarioRepository tipoUsuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }

    public async Task<IActionResult> Index()
    {
        var lista = await _usuarioRepository.GetAllAsync();
        return View(lista);
    }

    [Authorize(Roles = "Administrador,Gerente")]
    public async Task<IActionResult> Create()
    {
        var vm = await CriarUsuarioViewModel();
        return View(vm);
    }
    // POST: Usuario/Create
    public async Task<IActionResult> Create(UsuarioViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            var vm = await CriarUsuarioViewModel(viewModel);
            return View(vm);
        }

        var usuario = new Usuario
        {
            NomeUsuario = viewModel.Nome,
            Email = viewModel.Email,
            Senha = viewModel.Senha,
            IdTipoUsuario = viewModel.IdTipoUsuario,
            Ativo = true
        };

        await _usuarioRepository.AddAsync(usuario);
        return RedirectToAction(nameof(Index));
    }



    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }



    // -------- Apoio --------
    private async Task<UsuarioViewModel> CriarUsuarioViewModel(UsuarioViewModel? model = null)
    {
        var tipos = await _tipoUsuarioRepository.GetAllAsync();
        return new UsuarioViewModel
        {
            IdUsuario = model?.IdUsuario ?? 0,
            Nome = model?.Nome,
            Email = model?.Email,
            Senha = model?.Senha,
            DataNascimento = model?.DataNascimento ?? DateTime.Now,
            TipoUsuarioId = model?.TipoUsuarioId ?? 0,
            TiposUsuario = tipos.Select(t => new SelectListItem
            {
                Value = t.IdTipoUsuario.ToString(),
                Text = t.NomeTipoUsuario
            })
        };
    }


}
