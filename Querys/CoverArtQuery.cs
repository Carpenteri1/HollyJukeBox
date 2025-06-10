using JukeBox.Models;
using MediatR;

namespace JukeBox.QueryModels;

public class CoverArtQuery
{
    public record GetById(string Id) : IRequest<CoverArtDto>;
}