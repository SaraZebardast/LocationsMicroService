using CORE.APP.Models;
using CORE.APP.Services;
using Locations.APP.Domain;
using MediatR;

namespace Locations.APP.Features.Cities;

public class CityDeleteRequest : Request, IRequest<CommandResponse>
{
}

public class CityDeleteHandler : ServiceBase, IRequestHandler<CityDeleteRequest, CommandResponse>
{
    
    private readonly LocationsDb _locationsDb;

    public CityDeleteHandler(LocationsDb locationsDb)
    {
        _locationsDb = locationsDb;
    }
    
    public async Task<CommandResponse> Handle(CityDeleteRequest request, CancellationToken cancellationToken)
    {
        var entity = await _locationsDb.Cities.FindAsync(request.Id, cancellationToken);
        if (entity is null)
            return Error("City not found");

        _locationsDb.Cities.Remove(entity);
        await _locationsDb.SaveChangesAsync(cancellationToken);
        return Success("City deleted successfully.", entity.Id);
    }
}