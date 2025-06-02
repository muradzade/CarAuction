using CarAuction.Models.Enums;

namespace CarAuction.Models.DTOs;

public class SearchInput
{
    public VehicleType? Type { get; set; }
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
}
