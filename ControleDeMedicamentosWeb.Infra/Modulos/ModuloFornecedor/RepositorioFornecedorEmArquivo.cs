using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFornecedor;
using ControleDeMedicamentosWeb.Infra.Compartilhado.Arquivos;

namespace ControleDeMedicamentosWeb.Infra.Modulos.ModuloFornecedor;

public class RepositorioFornecedorEmArquivo :
    RepositorioBaseEmArquivo<Fornecedor>, IRepositorioFornecedor
{
    public RepositorioFornecedorEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Fornecedor> CarregarRegistros()
    {
        return contexto.Fornecedores;
    }
}