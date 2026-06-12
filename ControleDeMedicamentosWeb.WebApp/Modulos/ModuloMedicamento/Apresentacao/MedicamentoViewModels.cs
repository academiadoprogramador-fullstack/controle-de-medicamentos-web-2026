using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Apresentacao;

public record OpcaoFornecedorViewModel(
    Guid Id,
    string Nome
);

public record ListarMedicamentosViewModel(
    Guid Id,
    string Nome,
    string Descricao,
    uint QuantidadeEmEstoque,
    Guid FornecedorId,
    string FornecedorNome
);

public record CadastrarMedicamentoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Descricao\" deve ser preenchido.")]
    [StringLength(255, MinimumLength = 5, ErrorMessage = "O campo \"Descricao\" deve conter entre 5 e 255 caracteres.")]
    string Descricao,

    [Required(ErrorMessage = "O campo \"Fornecedor\" deve ser preenchido.")]
    Guid FornecedorId,

    [ValidateNever]
    List<OpcaoFornecedorViewModel> Fornecedores
);

public record EditarMedicamentoViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Descricao\" deve ser preenchido.")]
    [StringLength(255, MinimumLength = 5, ErrorMessage = "O campo \"Descricao\" deve conter entre 5 e 255 caracteres.")]
    string Descricao,

    [Required(ErrorMessage = "O campo \"Fornecedor\" deve ser preenchido.")]
    Guid FornecedorId,

    [ValidateNever]
    List<OpcaoFornecedorViewModel> Fornecedores
);

public record ExcluirMedicamentoViewModel(
    Guid Id,
    string Nome,
    string Descricao,
    uint QuantidadeEmEstoque,
    Guid FornecedorId,
    string FornecedorNome
);
