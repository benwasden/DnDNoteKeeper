using DnDNoteKeeper.Data;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;

namespace DnDNoteKeeper.Services;

public class CampaignService : ICampaignService
{
    private readonly DnDDbContext _context;
    private readonly BlobServiceClient _blobServiceClient;

    public CampaignService(DnDDbContext context, BlobServiceClient blobServiceClient)
    {
        _context = context;
        _blobServiceClient = blobServiceClient;
    }

    public async Task<bool> DeleteCampaignAsync(int campaignId, int currentUserId) {
        try {
            // Gets campaign from db and compares it against signed in user
            // If validated, then proceed
            var campaign = await _context.Campaigns.FirstOrDefaultAsync(c => c.Id == campaignId && c.UserId == currentUserId);

            if (campaign == null) return false;

            // If an image URL was stored in sql, it probably is in blob
            if (!string.IsNullOrEmpty(campaign.CampaignImage))
            {
                // If it is, delete it
                await DeleteBlobAsync(campaign.CampaignImage);
            }

            // Now that image is deleted, delete campaign from the database
            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();

            return true;
        } catch (Exception ex) {
            Console.WriteLine($"Error deleting campaign: {ex.Message}");
            return false;
        }
    }

    private async Task DeleteBlobAsync(string imageUrl) {
        try {
            // Gets path of blob, then searches in "campaign-images" (the container for the blob db in azure)
            var uri = new Uri(imageUrl);
            
            var blobName = Path.GetFileName(uri.LocalPath);

            var containerClient = _blobServiceClient.GetBlobContainerClient("campaign-images");
            var blobClient = containerClient.GetBlobClient(blobName);

            // If it's there, delete the image from the blob db
            await blobClient.DeleteIfExistsAsync();
        } catch (Exception ex) {
            Console.WriteLine($"Blob cleanup failed (non-critical): {ex.Message}");
        }
    }
}