using AutoMapper;
using NnGames.Poe2.Books;

namespace NnGames.Poe2.Blazor;

public class Poe2BlazorAutoMapperProfile : Profile
{
    public Poe2BlazorAutoMapperProfile()
    {
        CreateMap<BookDto, CreateUpdateBookDto>();
        
        //Define your AutoMapper configuration here for the Blazor project.
    }
}
