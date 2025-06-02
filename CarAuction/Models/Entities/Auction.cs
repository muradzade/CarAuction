using CarAuction.Exceptions;
using CarAuction.Models.Enums;

namespace CarAuction.Models.Entities;

public class Auction
{
    public string VehicleId { get; }
    public AuctionState Status { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public List<Proposal> Proposals { get; }
    public Proposal? HighestProposal => Proposals.MaxBy(b => b.Amount);

    public Auction(string vehicleId)
    {
        if (string.IsNullOrWhiteSpace(vehicleId))
        {
            throw new ArgumentException("Vehicle ID cannot be null or empty.");
        }

        VehicleId = vehicleId;
        Status = AuctionState.Created;
        Proposals = new List<Proposal>();
    }

    public void Start()
    {
        if (Status == AuctionState.Started)
        {
            throw new AuctionAlreadyActiveException("Auction can only be started if it hasn't been started before.");
        }

        Status = AuctionState.Started;
        StartDate = DateTime.Now;
    }

    public void Close()
    {
        if (Status != AuctionState.Started)
        {
            throw new AuctionNotActiveException("Auction must be started to close.");
        }

        Status = AuctionState.Closed;
        EndDate = DateTime.Now;
    }

    public void AddProposal(Proposal proposal)
    {
        if (Status != AuctionState.Started)
        {
            throw new AuctionNotActiveException($"Cannot add proposal to inactive auction for vehicle {VehicleId}.");
        }

        if (HighestProposal != null && proposal.Amount <= HighestProposal.Amount)
        {
            throw new InvalidProposalException($"Proposal amount €{proposal.Amount:N2} must be higher than current highest proposal €{HighestProposal.Amount:N2}.");
        }

        Proposals.Add(proposal);
    }
}