using BLL.Commands.UserC.Commands;
using Domain.CustomException;
using Domain.Entities;
using Domain.Helper;
using Domain.Interfaces;
using MediatR;

namespace BLL.Commands.UserC.Handlers;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public BorrowBookCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserIdBookIdsDto.UserId;
        var bookIds = request.UserIdBookIdsDto.BookIds;
        
        await CheckHelper.Check(userId, _unitOfWork.UserRepository);

        foreach (var bookId in bookIds)
        {
            await CheckHelper.Check(bookId, _unitOfWork.BookRepository);
            
            var bookUser = new BookUser
            {
                UserId = userId,
                BookId = bookId
            };

            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);
            
            if (book.CurrentCount <= 0) throw new LibraryException("Quantity is less than 0.");
                
            book.CurrentCount -= 1;

            await _unitOfWork.BookRepository.UpdateAsync(book);
            
            await _unitOfWork.BookUserRepository.AddAsync(bookUser);
            
            await _unitOfWork.SaveAsync();
        }

        return Unit.Value;
    }
}