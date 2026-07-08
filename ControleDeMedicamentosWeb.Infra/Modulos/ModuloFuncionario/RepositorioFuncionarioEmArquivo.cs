using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFuncionario;
using ControleDeMedicamentosWeb.Infra.Compartilhado.Arquivos;

namespace ControleDeMedicamentosWeb.Infra.Modulos.ModuloFuncionario;

public class RepositorioFuncionarioEmArquivo :
    RepositorioBaseEmArquivo<Funcionario>, IRepositorioFuncionario
{
    public RepositorioFuncionarioEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Funcionario> CarregarRegistros()
    {
        return contexto.Funcionarios;
    }
}