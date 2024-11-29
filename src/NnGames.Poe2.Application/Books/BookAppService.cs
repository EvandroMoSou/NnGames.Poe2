using System;
using NnGames.Poe2.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace NnGames.Poe2.Books;

public class BookAppService :
    CrudAppService<
        Book, //The Book entity
        BookDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateBookDto>, //Used to create/update a book
    IBookAppService //implement the IBookAppService
{
    public BookAppService(IRepository<Book, Guid> repository)
        : base(repository)
    {
        GetPolicyName = Poe2Permissions.Books.Default;
        GetListPolicyName = Poe2Permissions.Books.Default;
        CreatePolicyName = Poe2Permissions.Books.Create;
        UpdatePolicyName = Poe2Permissions.Books.Edit;
        DeletePolicyName = Poe2Permissions.Books.Delete;
    }
}