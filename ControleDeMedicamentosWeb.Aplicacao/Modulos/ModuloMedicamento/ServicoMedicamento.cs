using FluentResults;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloMedicamento;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFornecedor;

namespace ControleDeMedicamentosWeb.Aplicacao.Modulos.ModuloMedicamento;

public class ServicoMedicamento
{
    private readonly IRepositorioMedicamento repositorioMedicamento;
    private readonly IRepositorioFornecedor repositorioFornecedor;

    public ServicoMedicamento(
        IRepositorioMedicamento repositorioMedicamento,
        IRepositorioFornecedor repositorioFornecedor
    )
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public Result Cadastrar(CadastrarMedicamentoDto dto)
    {
        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(dto.FornecedorId);
        if (fornecedor == null)
            return Result.Fail("Fornecedor não encontrado.");

        Medicamento medicamento = new(dto.Nome, dto.Descricao, fornecedor);

        List<string> erros = medicamento.Validar();
        if (erros.Count > 0)
            return Result.Fail(erros);

        repositorioMedicamento.Cadastrar(medicamento);

        return Result.Ok();
    }

    public Result Editar(EditarMedicamentoDto dto)
    {
        Medicamento? medicamento = repositorioMedicamento.SelecionarPorId(dto.Id);
        if (medicamento == null)
            return Result.Fail("Medicamento não encontrado.");

        Fornecedor? fornecedor = repositorioFornecedor.SelecionarPorId(dto.FornecedorId);
        if (fornecedor == null)
            return Result.Fail("Fornecedor não encontrado.");

        Medicamento medicamentoAtualizado = new(dto.Nome, dto.Descricao, fornecedor);

        List<string> erros = medicamentoAtualizado.Validar();
        if (erros.Count > 0)
            return Result.Fail(erros);

        repositorioMedicamento.Editar(dto.Id, medicamentoAtualizado);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        bool excluido = repositorioMedicamento.Excluir(id);
        if (!excluido)
            return Result.Fail("Medicamento não encontrado.");

        return Result.Ok();
    }

    public List<ListarMedicamentosDto> SelecionarTodos()
    {
        return repositorioMedicamento.SelecionarTodos()
            .Select(m => new ListarMedicamentosDto(
                m.Id,
                m.Nome,
                m.Descricao,
                m.Fornecedor.Nome,
                m.QuantidadeEmEstoque
            ))
            .ToList();
    }

    public DetalhesMedicamentoDto? SelecionarPorId(Guid id)
    {
        Medicamento? m = repositorioMedicamento.SelecionarPorId(id);
        if (m == null) return null;

        return new DetalhesMedicamentoDto(
            m.Id,
            m.Nome,
            m.Descricao,
            m.Fornecedor.Nome,
            m.QuantidadeEmEstoque
        );
    }

    public List<OpcaoFornecedorDto> SelecionarFornecedores()
    {
        return repositorioFornecedor.SelecionarTodos()
            .Select(f => new OpcaoFornecedorDto(f.Id, f.Nome))
            .ToList();
    }
}