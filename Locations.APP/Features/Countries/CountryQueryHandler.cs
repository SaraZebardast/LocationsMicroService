using CORE.APP.Models;
using CORE.APP.Services;
using Locations.APP.Domain;
using MediatR;

namespace Locations.APP.Features.Countries;

// request
public class CountryQueryRequest : Request, IRequest<IQueryable<CountryQueryResponse>>
{
}

//response
public class CountryQueryResponse : Response
{
    public string CountryName { get; set; }
}

//handler
public class CountryQueryHandler : ServiceBase, IRequestHandler<CountryQueryRequest, IQueryable<CountryQueryResponse>>
{
    private readonly LocationsDb _locationsDb;

    public CountryQueryHandler(LocationsDb locationsDb)
    {
        _locationsDb = locationsDb;
    }

    public Task<IQueryable<CountryQueryResponse>> Handle(CountryQueryRequest request, CancellationToken cancellationToken)
    {
        var query = _locationsDb.Countries.Select(countryEntity => new CountryQueryResponse()
        {
            Id = countryEntity.Id,
            Guid = countryEntity.Guid,
            CountryName = countryEntity.CountryName
        });
        return Task.FromResult(query);
    }
}