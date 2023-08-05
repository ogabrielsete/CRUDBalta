using AutoMapper;
using TesteBalta.Models.ViewModels;
using TesteBalta.Models;

namespace TesteBalta.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Aluno, AlunoViewModel>()
                .ForMember(dest => dest.ImagemAluno, opt => opt.Ignore());
            CreateMap<AlunoViewModel, Aluno>()
                .ForMember(dest => dest.Assinaturas, opt => opt.Ignore());
            CreateMap<Assinatura, AssinaturaViewModel>().ReverseMap();
        }
    }
}
