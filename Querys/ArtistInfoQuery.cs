using JukeBox.Models;
using MediatR;

namespace JukeBox.QueryModels;

public class ArtistInfoQuery
{
    public record GetById(string Id) : IRequest<ArtistInfo>;
}