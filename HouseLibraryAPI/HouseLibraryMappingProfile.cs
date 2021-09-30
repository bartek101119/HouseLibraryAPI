using AutoMapper;
using HouseLibrary.Models;
using HouseLibraryAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLibrary
{
    public class HouseLibraryMappingProfile : Profile
    {
        public HouseLibraryMappingProfile()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<CreateAuthorDto, Author>();

            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>();

            // test gita x
        }
    }
}
