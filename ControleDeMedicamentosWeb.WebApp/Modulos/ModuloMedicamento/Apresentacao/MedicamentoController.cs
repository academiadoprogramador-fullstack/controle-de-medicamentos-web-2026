using AutoMapper;
using FluentResults;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Apresentacao;

public class MedicamentoController(ServicoMedicamento servicoMedicamento, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarMedicamentosDto> dtos = servicoMedicamento.SelecionarTodos();
        List<ListarMedicamentosViewModel> listarVms = mapeador.Map<List<ListarMedicamentosViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarMedicamentoViewModel cadastrarVm = new CadastrarMedicamentoViewModel(
            string.Empty,
            string.Empty,
            Guid.Empty,
            SelecionarFornecedores()
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm with { Fornecedores = SelecionarFornecedores() });

        CadastrarMedicamentoDto dto = mapeador.Map<CadastrarMedicamentoDto>(cadastrarVm);
        Result resultado = servicoMedicamento.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm with { Fornecedores = SelecionarFornecedores() });
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesMedicamentoDto> resultado = servicoMedicamento.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarMedicamentoViewModel editarVm =
            mapeador.Map<EditarMedicamentoViewModel>(resultado.Value) with { Fornecedores = SelecionarFornecedores() };

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarMedicamentoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm with { Fornecedores = SelecionarFornecedores() });

        EditarMedicamentoDto dto = mapeador.Map<EditarMedicamentoDto>(editarVm);
        Result resultado = servicoMedicamento.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm with { Fornecedores = SelecionarFornecedores() });
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesMedicamentoDto> resultado = servicoMedicamento.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirMedicamentoViewModel excluirVm = mapeador.Map<ExcluirMedicamentoViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirMedicamentoViewModel excluirVm)
    {
        Result resultado = servicoMedicamento.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    private List<OpcaoFornecedorViewModel> SelecionarFornecedores()
    {
        List<OpcaoFornecedorDto> dtos = servicoMedicamento.SelecionarFornecedores();

        return mapeador.Map<List<OpcaoFornecedorViewModel>>(dtos);
    }
}
