using System.ComponentModel.DataAnnotations;
using CORE.APP.Models;
using CORE.APP.Services;
using Locations.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Features.Cities;

public class CityCreateRequest : Request , IRequest<CommandResponse>
{
    [Required]
    [StringLength(100)]
    public string CityName { get; set; }
    
    public int CountryId { get; set; }
    
    public Country Country { get; set; }
}

public class CityCreateHandler : ServiceBase, IRequestHandler<CityCreateRequest, CommandResponse>
{
    private readonly LocationsDb _locationsDb;

    public CityCreateHandler(LocationsDb locationsDb)
    {
        _locationsDb = locationsDb;
    }
    
    public async Task<CommandResponse> Handle(CityCreateRequest request, CancellationToken cancellationToken)
    {
        if (await _locationsDb.Cities.AnyAsync(city => city.CityName == request.CityName.Trim(), cancellationToken))
            return Error("City With the same name exists");
        
        var entity = new City
        {
            CityName = request.CityName.Trim(),
            CountryId = request.CountryId,
            Guid = Guid.NewGuid().ToString()
        };
        
        _locationsDb.Cities.Add(entity);
        await _locationsDb.SaveChangesAsync(cancellationToken);
        return Success("City created successfully.", entity.Id);
    }
}