namespace ControleDeMedicamentosWeb.Aplicacao.Modulos.ModuloFuncionario;

public record ListarFuncionariosDto(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf
);

public record CadastrarFuncionarioDto(
    string Nome,
    string Telefone,
    string Cpf
);

public record EditarFuncionarioDto(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf
);

public record DetalhesFuncionarioDto(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf
);