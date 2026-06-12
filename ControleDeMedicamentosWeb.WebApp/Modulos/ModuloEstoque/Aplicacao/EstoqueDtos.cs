namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

public record DetalhesEstoqueMedicamentoDto(
    Guid MedicamentoId,
    string MedicamentoNome,
    string MedicamentoDescricao,
    string FornecedorNome,
    uint QuantidadeEmEstoque
);

public record OpcaoFuncionarioDto(
    Guid Id,
    string Nome
);

public record OpcaoPacienteDto(
    Guid Id,
    string Nome
);

public record RegistrarEntradaDto(
    Guid MedicamentoId,
    Guid FuncionarioId,
    uint Quantidade
);

public record RegistrarSaidaDto(
    Guid MedicamentoId,
    Guid PacienteId,
    uint Quantidade
);

public record ListarRequisicoesEntradaDto(
    Guid Id,
    DateTime DataCriacao,
    string FuncionarioNome,
    uint Quantidade
);

public record ListarRequisicoesSaidaDto(
    Guid Id,
    DateTime DataCriacao,
    string PacienteNome,
    uint Quantidade
);
