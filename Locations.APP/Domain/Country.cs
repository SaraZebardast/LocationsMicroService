using System.ComponentModel.DataAnnotations;
using CORE.APP.Domain;

namespace Locations.APP.Domain;

public class Country : Entity
{
    [Required]
    [StringLength(100)]
    public string CountryName { get; set; }
}