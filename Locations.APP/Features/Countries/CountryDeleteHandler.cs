using CORE.APP.Models;
using CORE.APP.Services;
using Locations.APP.Domain;
using MediatR;

namespace Locations.APP.Features.Countries;

public class CountryDeleteRequest : Request, IRequest<CommandResponse>
{
}

public class CountryDeleteHandler :ServiceBase, IRequestHandler<CountryDeleteRequest, CommandResponse>
{
    private readonly LocationsDb _locationsDb;

    public CountryDeleteHandler(LocationsDb locationsDb)
    {
        _locationsDb = locationsDb;
    }
    
    public async Task<CommandResponse> Handle(CountryDeleteRequest request, CancellationToken cancellationToken)
    {
        var entity = await _locationsDb.Countries.FindAsync(request.Id, cancellationToken);
        if (entity is null)
            return Error("Country not found!");
        _locationsDb.Countries.Remove(entity);
        await _locationsDb.SaveChangesAsync(cancellationToken);
        return Success("Country deleted successfully.", entity.Id);
    }
}