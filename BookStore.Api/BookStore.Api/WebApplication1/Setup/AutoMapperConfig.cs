﻿using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Domain.Entity;
using BookStore.Services.DTO;

namespace BookStore.Api.Setup
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Mapeamento entre Autor e AutorDTO
            CreateMap<Autor, AutorDto>().ReverseMap();
            CreateMap<Assunto, AssuntoDto>().ReverseMap();

            CreateMap<LivroAutor, LivroAutorDto>()
                    .ForMember(x => x.NomeAutor, y => y.MapFrom(m => m.Autor.Nome))
                    .ReverseMap();

            CreateMap<LivroAssunto, LivroAssuntoDto>()
                    .ForMember(x => x.Descricao, y => y.MapFrom(m => m.Assunto.Descricao))
                    .ReverseMap();

            CreateMap<Livro, LivroDto>()
                .ForMember(x => x.AutoresIds, y => y.MapFrom(m => m.LivroAutores.Select(a => a.CodAutor)))
                .ForMember(x => x.AssuntosIds, y => y.MapFrom(m => m.LivroAssuntos.Select(a => a.CodAssunto)))
                .ReverseMap();
        }
    }
}
