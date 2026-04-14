using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DnDNoteKeeper.Models;
using Microsoft.AspNetCore.Components;
using DnDNoteKeeper.Data;

namespace DnDNoteKeeper.Components.Pages.Campaign;

public partial class MyCampaigns
{
    private List<CampaignDetails>? campaigns;
    private int currentUserId;
    private string? errorMessage;

    [Inject] DnDDbContext AppDbContext { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated == true)
        {
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null)
            {
                currentUserId = int.Parse(idClaim.Value);
                await LoadCampaigns();
            }
        }
    }

    private async Task LoadCampaigns()
    {
        // Fetch only campaigns belonging to the current user
        campaigns = await AppDbContext.Campaigns
            .Where(c => c.UserId == currentUserId)
            .OrderByDescending(c => c.Id)
            .ToListAsync();
    }

    private async Task ConfirmDelete(int campaignId)
    {
        
            var success = await CampaignService.DeleteCampaignAsync(campaignId, currentUserId);
            if (success)
            {
                await LoadCampaigns();
                StateHasChanged();
            }
            else
            {
                errorMessage = "Failed to delete the campaign. Please try again.";
            }
        }
    
}