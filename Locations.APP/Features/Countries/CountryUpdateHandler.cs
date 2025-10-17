using System.ComponentModel.DataAnnotations;
using CORE.APP.Models;
using CORE.APP.Services;
using Locations.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Features.Countries;

public class CountryUpdateRequest : Request, IRequest<CommandResponse>
{
    [Required]
    [StringLength(100)]
    public string CountryName { get; set; }
}

public class CountryUpdateHandler : ServiceBase, IRequestHandler<CountryUpdateRequest, CommandResponse>
{
    private readonly LocationsDb _locationsDb;

    public CountryUpdateHandler(LocationsDb locationsDb)
    {
        _locationsDb = locationsDb;
    }


    public async Task<CommandResponse> Handle(CountryUpdateRequest request, CancellationToken cancellationToken)
    {
        if (await _locationsDb.Countries.AnyAsync(c => c.Id != request.Id && c.CountryName == request.CountryName,
                cancellationToken))
            return Error("Country with the same title exists!");
        var entity = await _locationsDb.Countries.SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (entity is null)
            return Error("Country not found!");
        entity.CountryName = request.CountryName?.Trim();
        _locationsDb.Countries.Update(entity);
        await _locationsDb.SaveChangesAsync(cancellationToken);
        return Success("Country updated successfully!", entity.Id);
    }
}