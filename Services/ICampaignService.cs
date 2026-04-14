namespace DnDNoteKeeper.Services;

public interface ICampaignService
{
    // Interface service that gets cooked into CampaignService.cs
    Task<bool> DeleteCampaignAsync(int campaignId, int currentUserId);
}