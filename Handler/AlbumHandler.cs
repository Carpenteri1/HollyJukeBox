using HollyJukeBox.Endpoints;
using HollyJukeBox.Models;
using HollyJukeBox.QueryModels;
using MediatR;

namespace HollyJukeBox.Handler;

public class AlbumHandler(IAlbumEndPoint albumEndPoint) :     
    IRequestHandler<AlbumQuery.GetById, AlbumDto>
{
    public async Task<AlbumDto> Handle(AlbumQuery.GetById query, CancellationToken cancellationToken) 
        => await albumEndPoint.GetById(query.Id);
}