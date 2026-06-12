using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Apresentacao;

public record ListarPacientesViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf,
    string CartaoSus
);

public record CadastrarPacienteViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "O campo \"Telefone\" deve estar no formato (DDD) 90000-0000.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [RegularExpression(@"^\d{11}$|^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O campo \"CPF\" deve conter 11 digitos.")]
    string Cpf,

    [Required(ErrorMessage = "O campo \"Cartao SUS\" deve ser preenchido.")]
    [RegularExpression(@"^\d{15}$", ErrorMessage = "O campo \"Cartao SUS\" deve conter 15 digitos.")]
    string CartaoSus
);

public record EditarPacienteViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\(\d{2}\) \d{4,5}-\d{4}$", ErrorMessage = "O campo \"Telefone\" deve estar no formato (DDD) 90000-0000.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [RegularExpression(@"^\d{11}$|^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O campo \"CPF\" deve conter 11 digitos.")]
    string Cpf,

    [Required(ErrorMessage = "O campo \"Cartao SUS\" deve ser preenchido.")]
    [RegularExpression(@"^\d{15}$", ErrorMessage = "O campo \"Cartao SUS\" deve conter 15 digitos.")]
    string CartaoSus
);

public record ExcluirPacienteViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf,
    string CartaoSus
);
