using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Infra;

public class RepositorioRequisicaoEmArquivo : IRepositorioRequisicao
{
    private readonly ContextoJson contexto;

    public RepositorioRequisicaoEmArquivo(ContextoJson contexto)
    {
        this.contexto = contexto;
    }

    public void Cadastrar(RequisicaoBase requisicao)
    {
        contexto.Requisicoes.Add(requisicao);

        contexto.Salvar();
    }

    public List<RequisicaoEntrada> SelecionarRequisicoesEntrada()
    {
        return contexto.Requisicoes.OfType<RequisicaoEntrada>().ToList();
    }

    public List<RequisicaoSaida> SelecionarRequisicoesSaida()
    {
        return contexto.Requisicoes.OfType<RequisicaoSaida>().ToList();
    }
}
