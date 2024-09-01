using BLL.Commands.UserC.Commands;
using Domain.CustomException;
using Domain.Helper;
using Domain.Interfaces;
using MediatR;

namespace BLL.Commands.UserC.Handlers;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReturnBookCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserIdBookIdsDto.UserId;
        var bookIds = request.UserIdBookIdsDto.BookIds;
        
        await CheckHelper.Check(userId, _unitOfWork.UserRepository);

        foreach (var bookId in bookIds)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);

            if (book is null) throw new LibraryException($"Book with ID {bookId} not found.");
            if (book.CurrentCount >= book.TotalCount)
                throw new LibraryException($"Maximum quantity is {book.TotalCount}");
            
            book.CurrentCount += 1;
            await _unitOfWork.BookRepository.UpdateAsync(book);

            await CheckHelper.Check(userId, _unitOfWork.BookUserRepository, bookId);

            await _unitOfWork.BookUserRepository.ReturnBookByIds(userId, bookId);
            
            await _unitOfWork.SaveAsync();
        }
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}