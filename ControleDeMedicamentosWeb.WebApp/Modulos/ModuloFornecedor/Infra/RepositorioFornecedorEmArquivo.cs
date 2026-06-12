using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Infra;

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
