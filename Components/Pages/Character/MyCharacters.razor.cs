using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using DnDNoteKeeper.Data;
using System.Security.Claims;

namespace DnDNoteKeeper.Components.Pages.Character;

public partial class MyCharacters
{
    [Inject] private DnDDbContext AppDbContext { get; set; } = default!;
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    private List<Models.Character>? characterList;

    private string? errorMessage;
    protected override async Task OnInitializedAsync()
    {
        try {
            // Check to see if the user is who they say they are
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            // If logged in user matches, proceed
            if (user.Identity?.IsAuthenticated == true)
            {
                var idClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                if (idClaim != null && int.TryParse(idClaim.Value, out int userId))
                {
                    // Fetch only characters for this specific user
                    characterList = await AppDbContext.Characters.Where(c => c.UserId == userId).ToListAsync();
                }
            }
        } catch (Exception ex)
        {
            Console.WriteLine(ex);
            errorMessage = "The database had an issue (likely timed out). Please try again shortly!";
        }
    }
    // Removes a character if it finds them from the database
    private async Task DeleteCharacter(int characterId)
    {
        try {
            // Look through db for character
            var character = await AppDbContext.Characters.FindAsync(characterId);
            // If requested character is found...
            if (character != null)
            {
                // Remove from db
                AppDbContext.Characters.Remove(character);
                await AppDbContext.SaveChangesAsync();
                
                // Remove from the local list to update UI immediately
                characterList?.Remove(character);
            }
        } catch (Exception ex) {
            Console.WriteLine(ex);
            errorMessage = "Failed to delete the character. Please try again.";
        }
    }
}