using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Apresentacao;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<ListarFuncionariosDto, ListarFuncionariosViewModel>();
        CreateMap<CadastrarFuncionarioViewModel, CadastrarFuncionarioDto>();
        CreateMap<EditarFuncionarioViewModel, EditarFuncionarioDto>();
        CreateMap<DetalhesFuncionarioDto, EditarFuncionarioViewModel>();
        CreateMap<DetalhesFuncionarioDto, ExcluirFuncionarioViewModel>();
    }
}
