namespace ControleDeMedicamentosWeb.Aplicacao.Modulos.ModuloMedicamento;

public record OpcaoFornecedorDto(
    Guid Id,
    string Nome
);

public record ListarMedicamentosDto(
    Guid Id,
    string Nome,
    string Descricao,
    string FornecedorNome,
    uint QuantidadeEmEstoque
);

public record CadastrarMedicamentoDto(
    string Nome,
    string Descricao,
    Guid FornecedorId
);

public record EditarMedicamentoDto(
    Guid Id,
    string Nome,
    string Descricao,
    Guid FornecedorId
);

public record DetalhesMedicamentoDto(
    Guid Id,
    string Nome,
    string Descricao,
    string FornecedorNome,
    uint QuantidadeEmEstoque
);