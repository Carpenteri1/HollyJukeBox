using HollyJukeBox.Endpoints;
using HollyJukeBox.Models;
using HollyJukeBox.QueryModels;
using HollyJukeBox.Repository;
using HollyJukeBox.Services;
using MediatR;

namespace HollyJukeBox.Handler;

public class CoverArtHandler(
    ICoverArtEndPoint coverArtEndPoint, 
    IMemoryCashingService memoryCashingService, 
    ICoverArtRepository coverArtRepository) :     
    IRequestHandler<CoverArtQuery.GetById, CoverArtDto>
{
    public async Task<CoverArtDto> Handle(CoverArtQuery.GetById request, CancellationToken cancellationToken)
    {
        var coverArt = memoryCashingService.Get<CoverArtDto>($"coverArt:{request.Id}");
        if (coverArt is not null)
        {
            return coverArt;
        }

        coverArt = await coverArtRepository.GetByIdAsync(request.Id);
        if (coverArt is not null)
        {
            memoryCashingService.Store($"coverArt:{request.Id}", coverArt);
            return coverArt;
        }
        
        coverArt = await coverArtEndPoint.GetById(request.Id);
        coverArt.Id = request.Id;
        var sortedImages = coverArt.Images
            .Where(x =>
                x.Types.Contains(nameof(ImageTypes.Front)) ||
                x.Types.Contains(nameof(ImageTypes.Back)));

        coverArt.Images = sortedImages.ToList();
        
        memoryCashingService.Store($"coverArt:{request.Id}", coverArt);
        await coverArtRepository.SaveAsync(coverArt);
        return coverArt;
    }
}