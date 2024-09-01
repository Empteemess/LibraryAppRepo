using Domain.CustomException;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Helper;

public static class CheckHelper
{
    private const string ModelMessage = "model is null";
    private const string EntityMessage = "entity does not exist";

    public static async Task Check<T>(Guid id, IRepository<T> repository, Guid? bookId = null) where T : class
    {
        switch (repository)
        {
            case IAuthorRepository authorRepository:
                var author = await authorRepository.GetByIdAsync(id);
                NullCheck(author, EntityMessage);
                break;
            case IUserRepository userRepository:
                var user = await userRepository.GetByIdAsync(id);
                NullCheck(user, EntityMessage);
                break;
            case IBookRepository bookRepository:
                var book = await bookRepository.GetByIdAsync(id);
                NullCheck(book, EntityMessage);
                break;
            case IBookUserRepository bookUserRepository:
                if (bookId is not null)
                {
                    var bookUser = await bookUserRepository.GetByUserBookIdAsync(id, bookId);
                    NullCheck(bookUser!);
                    break;
                }
                break;
        }
    }

    private static void NullCheck(object model, string action = ModelMessage)
    {
        if (model is null)
        {
            throw new LibraryException($"{action}: {nameof(model)}");
        }
    }
}