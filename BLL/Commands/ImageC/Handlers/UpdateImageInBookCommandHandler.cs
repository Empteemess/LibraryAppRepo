using BLL.Commands.ImageC.Commands;
using Domain.Entities;
using Domain.Helper;
using Domain.Interfaces;
using Google.Cloud.Storage.V1;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BLL.Commands.ImageC.Handlers;

public class UpdateImageInBookCommandHandler : IRequestHandler<UpdateImageInBookCommand>
{
    private const string BucketName = "librarybuck";
    private readonly IUnitOfWork _unitOfWork;

    public UpdateImageInBookCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateImageInBookCommand request, CancellationToken cancellationToken)
    {
        var bookId = request.BookId;
        var formFile = request.FormFile;

        var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId);

        await CheckHelper.Check(bookId, _unitOfWork.BookRepository);

        await RemoveImageAsync(book);

        book.Image = await UploadFileAsync(formFile, BucketName, book);

        await _unitOfWork.BookRepository.UpdateAsync(book);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }


    private async Task RemoveImageAsync(Book book)
    {
        if (string.IsNullOrEmpty(book.Title)) throw new ArgumentException();

        var storageClient = StorageClient.Create();

        await storageClient.DeleteObjectAsync(BucketName, book.UniqImageString);

        book.Image = string.Empty;
        book.UniqImageString = string.Empty;
    }

    private async Task<string> UploadFileAsync(IFormFile file, string bucketName, Book book)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is not valid.");
        }

        var imgName =  Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);

        book.UniqImageString = imgName;

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);

            memoryStream.Position = 0;

            var storageClient = StorageClient.Create();

            await storageClient.UploadObjectAsync(
                bucketName,
                imgName,
                file.ContentType,
                memoryStream);

            var fileUrl = $"https://storage.googleapis.com/{bucketName}/{imgName}";
            return fileUrl;
        }
    }
}