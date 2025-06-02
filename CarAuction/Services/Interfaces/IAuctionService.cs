namespace CarAuction.Services.Interfaces;

public interface IAuctionService
{
    public void Start(string vehicleId);
    public void Close(string vehicleId);
    public void Propose(string vehicleId, decimal proposalAmount, string proposerName, decimal startingProposalAmount);
}