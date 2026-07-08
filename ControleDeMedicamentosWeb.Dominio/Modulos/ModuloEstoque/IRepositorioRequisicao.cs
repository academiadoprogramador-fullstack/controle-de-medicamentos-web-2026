using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloEstoque;

namespace ControleDeMedicamentosWeb.Dominio.Modulos.ModuloEstoque;

public interface IRepositorioRequisicao
{
    void Cadastrar(RequisicaoBase requisicao);
    List<RequisicaoEntrada> SelecionarRequisicoesEntrada();
    List<RequisicaoSaida> SelecionarRequisicoesSaida();
}