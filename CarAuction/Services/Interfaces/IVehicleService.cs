using CarAuction.Models.DTOs;
using CarAuction.Models.Entities;

namespace CarAuction.Services.Interfaces;

public interface IVehicleService
{
    public IEnumerable<Vehicle> Filter(SearchInput? searchInput);
    public Vehicle Get(string vehicleId);
    public void Add(Vehicle vehicle);
    public bool Exists(string vehicleId);
}