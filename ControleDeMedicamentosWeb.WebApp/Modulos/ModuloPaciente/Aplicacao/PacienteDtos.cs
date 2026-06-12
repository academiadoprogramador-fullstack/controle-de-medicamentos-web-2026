namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

public record ListarPacientesDto(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf,
    string CartaoSus
);

public record CadastrarPacienteDto(
    string Nome,
    string Telefone,
    string Cpf,
    string CartaoSus
);

public record EditarPacienteDto(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf,
    string CartaoSus
);

public record DetalhesPacienteDto(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf,
    string CartaoSus
);
