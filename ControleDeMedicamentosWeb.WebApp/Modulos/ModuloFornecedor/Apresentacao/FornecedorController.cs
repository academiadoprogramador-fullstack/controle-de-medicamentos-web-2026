using AutoMapper;
using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Apresentacao;

public class FornecedorController(ServicoFornecedor servicoFornecedor, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarFornecedoresDto> dtos = servicoFornecedor.SelecionarTodos();
        List<ListarFornecedoresViewModel> listarVms = mapeador.Map<List<ListarFornecedoresViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarFornecedorViewModel cadastrarVm = new CadastrarFornecedorViewModel(
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarFornecedorViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarFornecedorDto dto = mapeador.Map<CadastrarFornecedorDto>(cadastrarVm);
        Result resultado = servicoFornecedor.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesFornecedorDto> resultado = servicoFornecedor.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarFornecedorViewModel editarVm = mapeador.Map<EditarFornecedorViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarFornecedorViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarFornecedorDto dto = mapeador.Map<EditarFornecedorDto>(editarVm);
        Result resultado = servicoFornecedor.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesFornecedorDto> resultado = servicoFornecedor.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirFornecedorViewModel excluirVm = mapeador.Map<ExcluirFornecedorViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirFornecedorViewModel excluirVm)
    {
        Result resultado = servicoFornecedor.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}
