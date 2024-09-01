using BLL.Commands.AuthorC.Commands;
using Domain.Helper;
using Domain.Interfaces;
using MediatR;

namespace BLL.Commands.AuthorC.Handlers;

public class RemoveAuthorByIdCommandHandler : IRequestHandler<RemoveAuthorByIdCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAuthorByIdCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveAuthorByIdCommand request, CancellationToken cancellationToken)
    {
        var authorIds = request.AuthorIds.AuthorIds!.ToList();

        for (var i = 0; i < authorIds.Count(); i++)
        {
            await CheckHelper.Check(authorIds[i], _unitOfWork.AuthorRepository);
        }

        for (var i = 0; i < authorIds.Count(); i++)
        {
            await _unitOfWork.AuthorRepository.RemoveByIdAsync(authorIds[i]);
            await _unitOfWork.SaveAsync();
        }

        return Unit.Value;
    }
}