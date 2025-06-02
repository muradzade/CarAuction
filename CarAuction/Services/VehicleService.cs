using CarAuction.Exceptions;
using CarAuction.Models.DTOs;
using CarAuction.Models.Entities;
using CarAuction.Services.Interfaces;

namespace CarAuction.Services;

public class VehicleService : IVehicleService
{
    private readonly Dictionary<string, Vehicle> _vehicles;

    public VehicleService()
    {
        _vehicles = new Dictionary<string, Vehicle>(StringComparer.OrdinalIgnoreCase);
    }

    public IEnumerable<Vehicle> Filter(SearchInput? searchInput)
    {
        if (searchInput == null)
        {
            return _vehicles.Values;
        }

        return _vehicles.Values.Where(vehicle =>
        {
            if (searchInput.Type.HasValue && vehicle.Type != searchInput.Type.Value)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(searchInput.Manufacturer) &&
                !vehicle.Manufacturer.Contains(searchInput.Manufacturer, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(searchInput.Model) &&
                !vehicle.Model.Contains(searchInput.Model, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (searchInput.Year.HasValue && vehicle.Year != searchInput.Year.Value)
            {
                return false;
            }

            if (searchInput.MinYear.HasValue && vehicle.Year < searchInput.MinYear.Value)
            {
                return false;
            }

            if (searchInput.MaxYear.HasValue && vehicle.Year > searchInput.MaxYear.Value)
            {
                return false;
            }

            return true;
        });
    }

    public Vehicle Get(string vehicleId)
    {
        if (string.IsNullOrWhiteSpace(vehicleId))
        {
            throw new ArgumentException("Vehicle ID cannot be null or empty.");
        }

        if (!_vehicles.TryGetValue(vehicleId, out var vehicle))
        {
            throw new VehicleNotFoundException($"Vehicle with ID '{vehicleId}' not found.");
        }

        return vehicle;
    }

    public void Add(Vehicle vehicle)
    {
        if (vehicle == null)
        {
            throw new ArgumentNullException(nameof(vehicle));
        }

        if (_vehicles.ContainsKey(vehicle.Id))
        {
            throw new DuplicateVehicleException($"Vehicle with ID '{vehicle.Id}' already exists in the inventory.");
        }

        _vehicles[vehicle.Id] = vehicle;
    }

    public bool Exists(string vehicleId)
    {
        if (string.IsNullOrWhiteSpace(vehicleId))
        {
            return false;
        }

        return _vehicles.ContainsKey(vehicleId);
    }
}