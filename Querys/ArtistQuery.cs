using JukeBox.Models;
using MediatR;

namespace JukeBox.QueryModels;

public class ArtistQuery
{
    public record GetById(string Id) : IRequest<ArtistDto>;
    public record GetWikiData(string id) : IRequest<WikiDataDto>;
    public record GetWikipediaSummary(string enwikiTitle) : IRequest<WikipediaSummaryDto>;
}