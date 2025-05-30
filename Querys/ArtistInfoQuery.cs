using HollyJukeBox.Models;
using MediatR;

namespace HollyJukeBox.QueryModels;

public class ArtistInfoQuery
{
    public record GetById(string Id) : IRequest<ArtistInfo>;
}