using BLL.Commands.BookC.Commands;
using Domain.Helper;
using Domain.Interfaces;
using MediatR;

namespace BLL.Commands.BookC.Handlers;

public class RemoveBookCommandHandler : IRequestHandler<RemoveBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveBookCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
    {
        if (request.BookIds.Ids is not null)
        {
            foreach (var id in request.BookIds.Ids)
            {
                await CheckHelper.Check(id, _unitOfWork.BookRepository);
                
                await _unitOfWork.BookRepository.RemoveByIdAsync(id);
                await _unitOfWork.SaveAsync();
            }
        }

        return Unit.Value;
    }
}