using AutoMapper;
using NnGames.Poe2.Books;
using NnGames.Poe2.Domains.DmGem;

namespace NnGames.Poe2;

public class Poe2ApplicationAutoMapperProfile : Profile
{
    public Poe2ApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Gem, GemDto>();
        CreateMap<GemDto, Gem>();
    }
}
