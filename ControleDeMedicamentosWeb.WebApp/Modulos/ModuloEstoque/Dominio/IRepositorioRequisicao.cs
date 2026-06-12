namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Dominio;

public interface IRepositorioRequisicao
{
    void Cadastrar(RequisicaoBase requisicao);
    List<RequisicaoEntrada> SelecionarRequisicoesEntrada();
    List<RequisicaoSaida> SelecionarRequisicoesSaida();
}
