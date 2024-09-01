using Domain.DTOs;
using Domain.DTOs.AuthDtos;
using MediatR;

namespace BLL.Queries.AuthQ.Queries;

public record GetAllAuthorsQuery() : IRequest<IEnumerable<AuthorsWithBookIdsDto>>;
