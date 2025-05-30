using HollyJukeBox.Models;
using MediatR;

namespace HollyJukeBox.QueryModels;

public class ArtistQuery
{
    public record GetById(string Id) : IRequest<ArtistDto>;
    public record GetWikiData(string id) : IRequest<WikiDataDto>;
    public record GetWikipediaSummary(string enwikiTitle) : IRequest<WikipediaSummaryDto>;
}