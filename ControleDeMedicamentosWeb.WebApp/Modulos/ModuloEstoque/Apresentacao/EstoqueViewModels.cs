using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Apresentacao;

public record OpcaoFuncionarioViewModel(
    Guid Id,
    string Nome
);

public record OpcaoPacienteViewModel(
    Guid Id,
    string Nome
);

public record RegistrarEntradaViewModel(
    Guid MedicamentoId,
    string MedicamentoNome,
    string MedicamentoDescricao,
    string FornecedorNome,
    uint QuantidadeEmEstoque,

    [Required(ErrorMessage = "O campo \"Funcionario\" deve ser preenchido.")]
    Guid FuncionarioId,

    [Range(1, uint.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    uint Quantidade,

    [ValidateNever]
    List<OpcaoFuncionarioViewModel> Funcionarios
);

public record RegistrarSaidaViewModel(
    Guid MedicamentoId,
    string MedicamentoNome,
    string MedicamentoDescricao,
    string FornecedorNome,
    uint QuantidadeEmEstoque,

    [Required(ErrorMessage = "O campo \"Paciente\" deve ser preenchido.")]
    Guid PacienteId,

    [Range(1, uint.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    uint Quantidade,

    [ValidateNever]
    List<OpcaoPacienteViewModel> Pacientes
);

public record ListarRequisicoesEntradaViewModel(
    Guid Id,
    DateTime DataCriacao,
    string FuncionarioNome,
    uint Quantidade
);

public record ListarRequisicoesSaidaViewModel(
    Guid Id,
    DateTime DataCriacao,
    string PacienteNome,
    uint Quantidade
);

public record HistoricoEstoqueViewModel(
    Guid MedicamentoId,
    string MedicamentoNome,
    string MedicamentoDescricao,
    string FornecedorNome,
    uint QuantidadeEmEstoque,
    List<ListarRequisicoesEntradaViewModel> Entradas,
    List<ListarRequisicoesSaidaViewModel> Saidas
);
