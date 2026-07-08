using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFuncionario;
using ControleDeMedicamentosWeb.Infra.Compartilhado.Sql;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ControleDeMedicamentosWeb.Infra.Modulos.ModuloFuncionario;

public sealed class RepositorioFuncionarioEmSql(ISqlConnectionFactory connectionFactory)
    : IRepositorioFuncionario
{
    private const string InserirSql = """
        INSERT INTO dbo.TBFuncionario (Id, Nome, Telefone, Cpf)
        VALUES (@Id, @Nome, @Telefone, @Cpf)
        """;

    private const string EditarSql = """
        UPDATE dbo.TBFuncionario
        SET Nome = @Nome, Telefone = @Telefone, Cpf = @Cpf
        WHERE Id = @Id
        """;

    private const string ExcluirSql = """
        DELETE FROM dbo.TBFuncionario WHERE Id = @Id
        """;

    private const string SelecionarPorIdSql = """
        SELECT Id, Nome, Telefone, Cpf
        FROM dbo.TBFuncionario
        WHERE Id = @Id
        """;

    private const string SelecionarTodosSql = """
        SELECT Id, Nome, Telefone, Cpf
        FROM dbo.TBFuncionario
        """;

    public void Cadastrar(Funcionario funcionario)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        connection.Execute(InserirSql, funcionario);
    }

    public bool Editar(Guid idSelecionado, Funcionario funcionarioAtualizado)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        int linhasAfetadas = connection.Execute(EditarSql, new
        {
            funcionarioAtualizado.Nome,
            funcionarioAtualizado.Telefone,
            funcionarioAtualizado.Cpf,
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

    public Funcionario? SelecionarPorId(Guid idSelecionado)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        return connection.QueryFirstOrDefault<Funcionario>(SelecionarPorIdSql, new { Id = idSelecionado });
    }

    public List<Funcionario> SelecionarTodos()
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        return connection.Query<Funcionario>(SelecionarTodosSql).ToList();
    }

    public List<Funcionario> Filtrar(Predicate<Funcionario> filtro)
    {
        using SqlConnection connection = connectionFactory.CreateConnection();
        return connection.Query<Funcionario>(SelecionarTodosSql).Where(f => filtro(f)).ToList();
    }
}