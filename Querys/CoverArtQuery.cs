using HollyJukeBox.Models;
using MediatR;

namespace HollyJukeBox.QueryModels;

public class CoverArtQuery
{
    public record GetById(string Id) : IRequest<CoverArtDto>;
}