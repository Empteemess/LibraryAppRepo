using BLL.Commands.BookC.Commands;
using Domain.Interfaces;
using Domain.Mapper;
using Google.Cloud.Storage.V1;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BLL.Commands.BookC.Handlers;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddBookCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = request.BookDto.ToBook();

        await _unitOfWork.BookRepository.AddAsync(book);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }

   
}