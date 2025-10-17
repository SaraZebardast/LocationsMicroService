using System.ComponentModel.DataAnnotations;
using CORE.APP.Models;
using CORE.APP.Services;
using Locations.APP.Domain;
using Locations.APP.Features.Countries;
using MediatR;
namespace Locations.APP.Features.Cities;

public class CityQueryResponse : Response
{
    public string CityName { get; set; }
    public CountryQueryResponse Country { get; set; }
}

public class CityQueryRequest : Request, IRequest<IQueryable<CityQueryResponse>>
{
    
}

public class CityQueryHandler : ServiceBase, IRequestHandler<CityQueryRequest, IQueryable<CityQueryResponse>>
{
    private readonly LocationsDb _locationsDb;

    public CityQueryHandler(LocationsDb locationsDb)
    {
        _locationsDb = locationsDb;
    }
    
    public Task<IQueryable<CityQueryResponse>> Handle(CityQueryRequest request, CancellationToken cancellationToken)
    {
        var query = _locationsDb.Cities.Select(cityEntity => new CityQueryResponse()
        {
            Id = cityEntity.Id,
            Guid = cityEntity.Guid,
            CityName = cityEntity.CityName,
            Country = new CountryQueryResponse
            {
                Id = cityEntity.Country.Id,
                Guid = cityEntity.Country.Guid,
                CountryName = cityEntity.Country.CountryName
            }
        });
        return Task.FromResult(query);
    }
}