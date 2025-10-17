using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CORE.APP.Models;
using CORE.APP.Services;
using Locations.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Features.Countries;

public class CountryCreateRequest : Request, IRequest<CommandResponse>
{
    [Required]
    [StringLength(100)]
    public string CountryName { get; set; }
}

public class CountryCreateHandler : ServiceBase, IRequestHandler<CountryCreateRequest, CommandResponse>
{
    
    private readonly LocationsDb _locationsDb;

    public CountryCreateHandler(LocationsDb locationsDb)
    {
        _locationsDb = locationsDb;
    }
    
    public async Task<CommandResponse> Handle(CountryCreateRequest request, CancellationToken cancellationToken)
    {
        if (await _locationsDb.Countries.AnyAsync(c => c.CountryName == request.CountryName.Trim(), cancellationToken))
            return Error("Country with the same Name exists!");
        var entity = new Country
        {
            CountryName = request.CountryName.Trim(),
            Guid = Guid.NewGuid().ToString()
        };
        _locationsDb.Countries.Add(entity);
        await _locationsDb.SaveChangesAsync(cancellationToken);
        return Success("Country created successfully.", entity.Id);
    }
}

