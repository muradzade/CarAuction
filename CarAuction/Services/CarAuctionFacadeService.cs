using CarAuction.Exceptions;
using CarAuction.Models.DTOs;
using CarAuction.Models.Entities;
using CarAuction.Services.Interfaces;

namespace CarAuction.Services;

public class CarAuctionFacadeService : ICarAuctionFacadeService
{
    private readonly IVehicleService _vehicleService;
    private readonly IAuctionService _auctionService;

    public CarAuctionFacadeService()
    {
        _vehicleService = new VehicleService();
        _auctionService = new AuctionService();
    }

    // Constructor injection for IVehicleService and IAuctionService
    public CarAuctionFacadeService(IVehicleService vehicleService, IAuctionService auctionService)
    {
        _vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
        _auctionService = auctionService ?? throw new ArgumentNullException(nameof(auctionService));
    }

    public void AddVehicle(Vehicle vehicle)
    {
        _vehicleService.Add(vehicle);
    }

    public IEnumerable<Vehicle> FilterVehicles(SearchInput? searchInput)
    {
        return _vehicleService.Filter(searchInput);
    }

    public void StartAuction(string vehicleId)
    {
        if (!_vehicleService.Exists(vehicleId))
        {
            throw new VehicleNotFoundException($"Vehicle with ID '{vehicleId}' not found.");
        }

        _auctionService.Start(vehicleId);
    }

    public void ProposeToAuction(string vehicleId, decimal proposalAmount, string proposerName)
    {
        var vehicle = _vehicleService.Get(vehicleId);
        _auctionService.Propose(vehicleId, proposalAmount, proposerName, vehicle.StartingProposal);
    }

    public void CloseAuction(string vehicleId)
    {
        _auctionService.Close(vehicleId);
    }
}