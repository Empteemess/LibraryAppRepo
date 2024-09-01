using BLL.Commands.AuthorC.Commands;
using Domain.Entities;
using Domain.Helper;
using Domain.Interfaces;
using MediatR;

namespace BLL.Commands.AuthorC.Handlers;

public class AddBookInAuthorCommandHandler : IRequestHandler<AddBookInAuthorCommand,Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddBookInAuthorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(AddBookInAuthorCommand request, CancellationToken cancellationToken)
    {
        var bookIds = request.AuthorIdBookIds.BookIds!.ToList();

        await CheckHelper.Check(request.AuthorIdBookIds.AuthorId, _unitOfWork.AuthorRepository);
        
        foreach (var ids in bookIds)
        {
            await CheckHelper.Check(ids, _unitOfWork.BookRepository);
        }
        
        for (var i = 0; i < request.AuthorIdBookIds.BookIds!.Count(); i++)
        {
            var authorBook = new AuthorBook()
            {
                AuthorId = request.AuthorIdBookIds.AuthorId,
                BookId = bookIds[i]
            };

            await _unitOfWork.AuthorBookRepository.AddAsync(authorBook);
            await _unitOfWork.SaveAsync();
        }
        return Unit.Value;
    }
}