using HollyJukeBox.Models;
using MediatR;

namespace HollyJukeBox.QueryModels;

public class AlbumQuery
{
    public record GetById(Guid Id) : IRequest<AlbumDto>;
    public record GetByName(string Name) : IRequest<AlbumDto>;
}