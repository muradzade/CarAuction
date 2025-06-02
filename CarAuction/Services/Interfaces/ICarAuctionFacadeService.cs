using CarAuction.Models.DTOs;
using CarAuction.Models.Entities;

namespace CarAuction.Services.Interfaces;

public interface ICarAuctionFacadeService
{
    public void AddVehicle(Vehicle vehicle);
    public IEnumerable<Vehicle> FilterVehicles(SearchInput? searchInput);
    public void StartAuction(string vehicleId);
    public void CloseAuction(string vehicleId);
    public void ProposeToAuction(string vehicleId, decimal proposalAmount, string proposerName);
}