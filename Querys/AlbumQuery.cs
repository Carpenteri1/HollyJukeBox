using HollyJukeBox.Models;
using MediatR;

namespace HollyJukeBox.QueryModels;

public class AlbumQuery
{
    public record GetById(string Id) : IRequest<AlbumDto>;
}