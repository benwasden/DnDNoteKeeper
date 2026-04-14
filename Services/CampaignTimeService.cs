using DnDNoteKeeper.Models;

namespace DnDNoteKeeper.Services
{
    public static class CampaignTime
    {
        public static string getCampaignTime(CampaignDetails campaign) {
        return campaign.Time.HasValue ? campaign.Time.Value.ToString("MMM dd, h:mm tt"): "Not Set";
    }
    }
}