﻿using AutoMapper;
using DevIO.api.ViewModels;
using DevIO.Business.Models;

namespace DevIO.api.Config
    {/// <summary>
    /// Classe de configuração para a conversão e mapeamento dos dados das Entitades para as ViewModels e vice-versa para
    /// trazer no Result da controller
    /// </summary>
    public class AutomapperConfig : Profile
    {
        /// <summary>
        /// Construtor para criação dos mappings
        /// </summary>
        public AutomapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<ProdutoImagemViewModel, Produto>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.NomeFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Nome));
        }
    }
}
