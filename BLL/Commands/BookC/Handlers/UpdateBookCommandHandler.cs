using BLL.Commands.BookC.Commands;
using Domain.Helper;
using Domain.Interfaces;
using Domain.Mapper;
using MediatR;

namespace BLL.Commands.BookC.Handlers;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        await CheckHelper.Check(request.UpdateBookDto.BookId, _unitOfWork.BookRepository);

        var existingBook = await _unitOfWork.BookRepository.GetByIdAsync(request.UpdateBookDto.BookId);
        
        existingBook.Title = request.UpdateBookDto.Title!;
        existingBook.Descrption = request.UpdateBookDto.Descrption!;
        existingBook.TotalCount = request.UpdateBookDto.TotalCount;
        existingBook.CurrentCount = request.UpdateBookDto.CurrentCount;      
        
        await _unitOfWork.BookRepository.UpdateAsync(existingBook);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}