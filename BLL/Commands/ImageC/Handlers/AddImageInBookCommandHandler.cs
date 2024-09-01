using BLL.Commands.ImageC.Commands;
using Domain.Entities;
using Domain.Helper;
using Domain.Interfaces;
using Google.Cloud.Storage.V1;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BLL.Commands.ImageC.Handlers;

public class AddImageInBookCommandHandler : IRequestHandler<AddImageInBookCommand>
{
    private const string BucketName = "librarybuck";
    private readonly IUnitOfWork _unitOfWork;

    public AddImageInBookCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AddImageInBookCommand request, CancellationToken cancellationToken)
    {
        var formFile = request.FormFile;
        var bookId = request.BookId;
        
        await CheckHelper.Check(bookId, _unitOfWork.BookRepository);
        
        var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);

        book.Image = await UploadFileAsync(formFile, BucketName,book);

        await _unitOfWork.BookRepository.UpdateAsync(book);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }

    private async Task<string> UploadFileAsync(IFormFile file, string bucketName,Book book)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is not valid.");
        }
        
        var uniqueFileName =  Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);

        book.UniqImageString = uniqueFileName;

        await _unitOfWork.SaveAsync();
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);

            memoryStream.Position = 0;

            var storageClient = StorageClient.Create();

            await storageClient.UploadObjectAsync(
                bucketName,
                uniqueFileName,
                file.ContentType,
                memoryStream);

            var fileUrl = $"https://storage.googleapis.com/{bucketName}/{uniqueFileName}";
            return fileUrl;
        }
    }
}