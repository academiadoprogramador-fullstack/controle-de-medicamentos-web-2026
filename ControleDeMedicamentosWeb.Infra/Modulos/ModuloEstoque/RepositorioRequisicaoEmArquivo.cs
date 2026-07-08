using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloEstoque;
using ControleDeMedicamentosWeb.Infra.Compartilhado.Arquivos;

namespace ControleDeMedicamentosWeb.Infra.Modulos.ModuloEstoque;

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