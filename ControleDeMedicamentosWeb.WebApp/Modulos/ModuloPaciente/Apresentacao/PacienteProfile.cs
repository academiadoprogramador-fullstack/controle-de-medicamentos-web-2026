using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Apresentacao;

public class PacienteProfile : Profile
{
    public PacienteProfile()
    {
        CreateMap<ListarPacientesDto, ListarPacientesViewModel>();
        CreateMap<CadastrarPacienteViewModel, CadastrarPacienteDto>();
        CreateMap<EditarPacienteViewModel, EditarPacienteDto>();
        CreateMap<DetalhesPacienteDto, EditarPacienteViewModel>();
        CreateMap<DetalhesPacienteDto, ExcluirPacienteViewModel>();
    }
}
