using HollyJukeBox.Models;
using MediatR;

namespace HollyJukeBox.QueryModels;

public class ArtistQuery : IRequest
{
    public record GetById(string Id) : IRequest<ArtistDto>;
    public record GetByName(string Name) : IRequest<ArtistDto>;
    public record Get : IRequest<ArtistsDto>;
}