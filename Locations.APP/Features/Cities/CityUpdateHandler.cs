using System.ComponentModel.DataAnnotations;
using CORE.APP.Models;
using CORE.APP.Services;
using Locations.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Features.Cities;

public class CityUpdateRequest : Request, IRequest<CommandResponse>
{
    [Required]
    [StringLength(100)]
    public string CityName { get; set; }
    
    public int CountryId { get; set; }
    
    public Country Country { get; set; }
}

public class CityUpdateHandler : ServiceBase, IRequestHandler<CityUpdateRequest, CommandResponse>
{
    
    private readonly LocationsDb _locationsDb;

    public CityUpdateHandler(LocationsDb locationsDb)
    {
        _locationsDb = locationsDb;
    }
    
    public async Task<CommandResponse> Handle(CityUpdateRequest request, CancellationToken cancellationToken)
    {
        if (await _locationsDb.Cities.AnyAsync(city => city.Id != request.Id && city.CityName == request.CityName,
                cancellationToken))
            return Error("City with the same name exists");
        var entity = await _locationsDb.Cities.SingleOrDefaultAsync(city => city.Id == request.Id, cancellationToken);
        if (entity is null)
            return Error("City not found");

        _locationsDb.Cities.Update(entity);
        await _locationsDb.SaveChangesAsync(cancellationToken);
        return Success("City updated", entity.Id);
    }
}