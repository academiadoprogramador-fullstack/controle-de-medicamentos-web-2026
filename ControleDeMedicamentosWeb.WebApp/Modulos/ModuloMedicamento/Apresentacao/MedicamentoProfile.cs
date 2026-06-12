using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Aplicacao;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloMedicamento.Apresentacao;

public class MedicamentoProfile : Profile
{
    public MedicamentoProfile()
    {
        CreateMap<OpcaoFornecedorDto, OpcaoFornecedorViewModel>();
        CreateMap<ListarMedicamentosDto, ListarMedicamentosViewModel>();
        CreateMap<CadastrarMedicamentoViewModel, CadastrarMedicamentoDto>();
        CreateMap<EditarMedicamentoViewModel, EditarMedicamentoDto>();

        CreateMap<DetalhesMedicamentoDto, EditarMedicamentoViewModel>()
            .ForCtorParam("Fornecedores", opt => opt.MapFrom(_ => new List<OpcaoFornecedorViewModel>()));

        CreateMap<DetalhesMedicamentoDto, ExcluirMedicamentoViewModel>();
    }
}
