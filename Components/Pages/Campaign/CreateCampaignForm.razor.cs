using DnDNoteKeeper.Data;
using DnDNoteKeeper.Models;
using DnDNoteKeeper.Models.ViewModels;
using DnDNoteKeeper.Services;
using Microsoft.AspNetCore.Authorization;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace DnDNoteKeeper.Components.Pages.Campaign;

public partial class CreateCampaignForm
{
    [Inject] protected DnDDbContext AppDbContext { get; set; } = default!;
    [Inject] private BlobServiceClient BlobServiceClient { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    [SupplyParameterFromForm]
    public CampaignViewModel campaignModel { get; set; } = default!;

    protected override void OnInitialized() {
        campaignModel ??= new CampaignViewModel();
    }

    protected IBrowserFile? selectedFile;
    private string? currentUserName;
    private int currentUserId;
    private string? errorMessage;

    protected override async Task OnInitializedAsync() {
        // Get user's ID and username to add to db
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        // If user's authenticated...
        if (user.Identity?.IsAuthenticated == true) {
            // Username is equal to fetched username
            currentUserName = user.Identity.Name;
            var idClaim = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (idClaim != null) currentUserId = int.Parse(idClaim.Value);
            
            campaignModel.UserId = currentUserId;
        }
    }

    // Uploads image to blob db, gets url of image, then assigns form data and image url before
    // sending off to sql database
    private async Task HandleSubmit() {
        string? imageUrl = null;
        try {
            // Image upload to blob db, gets URL of image for insertion into sql db
            if (selectedFile != null) {
                var containerClient = BlobServiceClient.GetBlobContainerClient("campaign-images");
                var blobName = $"{Guid.NewGuid()}{Path.GetExtension(selectedFile.Name)}";
                var blobClient = containerClient.GetBlobClient(blobName);

                using var stream = selectedFile.OpenReadStream(maxAllowedSize: 1024 * 1024 * 5);
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = selectedFile.ContentType });
                imageUrl = blobClient.Uri.ToString();
            }

            // User entity info, assigned vs a CampaignDetails model. Uses url of image from prior if statement
            var dbCampaign = new CampaignDetails {
                CampaignName = campaignModel.Name,
                Description = campaignModel.Description,
                Location = campaignModel.Location,
                CampaignImage = imageUrl,
                UserId = currentUserId,
                UserName = currentUserName,
                Time = campaignModel.Time
            };

            // Save to sql db
            AppDbContext.Campaigns.Add(dbCampaign);
            await AppDbContext.SaveChangesAsync();

            NavigationManager.NavigateTo("/");  
            
        } catch (Exception ex) {
            Console.WriteLine(ex);
            errorMessage = "A database error occured. Please try again later.";
        }
    }
        private void HandleFileSelected(InputFileChangeEventArgs e) => selectedFile = e.File;
}
