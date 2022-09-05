using AutoMapper;
using CursoRestWebApi.Api.DTOs;
using CursoRestWebApi.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoRestWebApi.Api.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Produto, ProdutoDTO>()
                .ForMember(dest => dest.NomeFornecedor, map => map.MapFrom(src => src.Fornecedor.Nome));

            CreateMap<ProdutoDTO, Produto>();

            CreateMap<Fornecedor, FornecedorDTO>().ReverseMap();

            CreateMap<Endereco, EnderecoDTO>().ReverseMap();

            CreateMap<Fornecedor, FornecedorProdutoCombo>()
                .ForMember(dest => dest.FornecedorId, map => map.MapFrom(src => src.Id))
                .ForMember(dest => dest.NomeFornecedor, map => map.MapFrom(src => src.Nome));
        }
    }
}
