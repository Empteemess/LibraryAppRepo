using BLL.Commands.AuthorC.Commands;
using Domain.Helper;
using Domain.Interfaces;
using MediatR;

namespace BLL.Commands.AuthorC.Handlers;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        await CheckHelper.Check(request.UpdateAuthorDto.Id, _unitOfWork.AuthorRepository);
        
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(request.UpdateAuthorDto.Id);

        author.FirstName = request.UpdateAuthorDto.FirstName;
        author.LastName = request.UpdateAuthorDto.LastName;
        author.DateOfBirth = request.UpdateAuthorDto.DateOfBirth;
        
        await _unitOfWork.AuthorRepository.UpdateAsync(author);
        await _unitOfWork.SaveAsync();
        
        return Unit.Value;
    }
}