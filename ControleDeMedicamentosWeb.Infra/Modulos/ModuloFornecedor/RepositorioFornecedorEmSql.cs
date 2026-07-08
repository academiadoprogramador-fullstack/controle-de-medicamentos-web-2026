using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFornecedor;
using ControleDeMedicamentosWeb.Infra.Compartilhado.Sql;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentosWeb.Infra.Modulos.ModuloFornecedor;

public sealed class RepositorioFornecedorEmSql(ISqlConnectionFactory connectionFactory)
    : IRepositorioFornecedor
{
    private const string InserirSql = """
        INSERT INTO dbo.TBFornecedor (Id, Nome, Telefone, Cnpj)
        VALUES (@Id, @Nome, @Telefone, @Cnpj)
        """;

    private const string EditarSql = """
        UPDATE dbo.TBFornecedor
        SET Nome = @Nome, Telefone = @Telefone, Cnpj = @Cnpj
        WHERE Id = @Id
        """;

    private const string ExcluirSql = """
        DELETE FROM dbo.TBFornecedor WHERE Id = @Id
        """;

    private const string SelecionarPorIdSql = """
        SELECT Id, Nome, Telefone, Cnpj
        FROM dbo.TBFornecedor
        WHERE Id = @Id
        """;

    private const string SelecionarTodosSql = """
        SELECT Id, Nome, Telefone, Cnpj
        FROM dbo.TBFornecedor
        """;

    public void Cadastrar(Fornecedor fornecedor)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        connection.Execute(InserirSql, fornecedor);
    }

    public bool Editar(Guid idSelecionado, Fornecedor fornecedorAtualizado)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        int linhasAfetadas = connection.Execute(EditarSql, new
        {
            fornecedorAtualizado.Nome,
            fornecedorAtualizado.Telefone,
            fornecedorAtualizado.Cnpj,
            Id = idSelecionado
        });

        return linhasAfetadas > 0;
    }

    public bool Excluir(Guid idSelecionado)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        int linhasAfetadas = connection.Execute(ExcluirSql, new { Id = idSelecionado });

        return linhasAfetadas > 0;
    }

    public Fornecedor? SelecionarPorId(Guid idSelecionado)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        return connection.QueryFirstOrDefault<Fornecedor>(SelecionarPorIdSql, new { Id = idSelecionado });
    }

    public List<Fornecedor> SelecionarTodos()
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        return connection.Query<Fornecedor>(SelecionarTodosSql).ToList();
    }

    public List<Fornecedor> Filtrar(Predicate<Fornecedor> filtro)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        return connection.Query<Fornecedor>(SelecionarTodosSql).Where(f => filtro(f)).ToList();
    }
}