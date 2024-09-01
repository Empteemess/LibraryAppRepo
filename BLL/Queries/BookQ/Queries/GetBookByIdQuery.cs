using Domain.DTOs;
using Domain.DTOs.BookDtos;
using MediatR;

namespace BLL.Queries.BookQ.Queries;

public record GetBookByIdQuery(Guid Id) : IRequest<ResponseBookDto>;
