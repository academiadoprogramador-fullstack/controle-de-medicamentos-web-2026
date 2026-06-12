using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Aplicacao;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloEstoque.Apresentacao;

public class EstoqueProfile : Profile
{
    public EstoqueProfile()
    {
        CreateMap<OpcaoFuncionarioDto, OpcaoFuncionarioViewModel>();
        CreateMap<OpcaoPacienteDto, OpcaoPacienteViewModel>();
        CreateMap<RegistrarEntradaViewModel, RegistrarEntradaDto>();
        CreateMap<RegistrarSaidaViewModel, RegistrarSaidaDto>();
        CreateMap<ListarRequisicoesEntradaDto, ListarRequisicoesEntradaViewModel>();
        CreateMap<ListarRequisicoesSaidaDto, ListarRequisicoesSaidaViewModel>();
    }
}
