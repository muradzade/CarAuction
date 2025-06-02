using CarAuction.Exceptions;
using CarAuction.Models.Entities;
using CarAuction.Models.Enums;
using CarAuction.Services.Interfaces;

namespace CarAuction.Services;

public class AuctionService : IAuctionService
{
    private readonly Dictionary<string, Auction> _auctions;

    public AuctionService()
    {
        _auctions = new Dictionary<string, Auction>(StringComparer.OrdinalIgnoreCase);
    }

    public void Start(string vehicleId)
    {
        if (string.IsNullOrWhiteSpace(vehicleId))
        {
            throw new ArgumentException("Vehicle ID cannot be null or empty.");
        }

        if (_auctions.TryGetValue(vehicleId, out var value) && value.Status == AuctionState.Started)
        {
            throw new AuctionAlreadyActiveException($"Auction for vehicle '{vehicleId}' is already active.");
        }

        var auction = new Auction(vehicleId);
        auction.Start();
        _auctions[vehicleId] = auction;
    }

    public void Close(string vehicleId)
    {
        if (string.IsNullOrWhiteSpace(vehicleId))
        {
            throw new ArgumentException("Vehicle ID cannot be null or empty.");
        }

        if (!_auctions.TryGetValue(vehicleId, out var auction))
        {
            throw new AuctionNotFoundException($"No auction found for vehicle '{vehicleId}'.");
        }

        auction.Close();
    }

    public void Propose(string vehicleId, decimal proposalAmount, string proposerName, decimal startingProposalAmount)
    {
        if (string.IsNullOrWhiteSpace(vehicleId))
        {
            throw new ArgumentException("Vehicle ID cannot be null or empty.");
        }

        if (!_auctions.TryGetValue(vehicleId, out var auction))
        {
            throw new AuctionNotActiveException($"No auction found for vehicle '{vehicleId}'.");
        }

        var minimumProposal = auction.HighestProposal?.Amount ?? startingProposalAmount;

        if (proposalAmount <= minimumProposal)
        {
            throw new InvalidProposalException($"Proposal amount €{proposalAmount:N2} must be higher than €{minimumProposal:N2}.");
        }

        var proposal = new Proposal(proposalAmount, proposerName);
        auction.AddProposal(proposal);
    }
}