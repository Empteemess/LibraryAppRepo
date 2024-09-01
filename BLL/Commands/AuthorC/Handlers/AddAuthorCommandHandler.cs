using BLL.Commands.AuthorC.Commands;
using Domain.Interfaces;
using Domain.Mapper;
using MediatR;

namespace BLL.Commands.AuthorC.Handlers;

public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddAuthorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = request.AddAuthorDto.ToAuthor();

        await _unitOfWork.AuthorRepository.AddAsync(author);
        await _unitOfWork.SaveAsync();
        
        return Unit.Value;
    }
}