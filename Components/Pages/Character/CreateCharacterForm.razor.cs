using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using DnDNoteKeeper.Models;
using DnDNoteKeeper.Data;
using System.Security.Claims;

namespace DnDNoteKeeper.Components.Pages.Character;

public partial class CreateCharacterForm
{
    [Inject] private DnDDbContext AppDbContext { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Parameter] public int? Id { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        // Get the user ID 
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity?.IsAuthenticated == true)
        {
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null) _currentUserId = int.Parse(idClaim.Value);
        }

        // Checks to see if user is editing rather than creating new character
        if (Id.HasValue && characterModel.Name == "")
        {
            // If character exists, get the data for it and switch to editing mode
            var existing = await AppDbContext.Characters.FindAsync(Id.Value);

            // Checks to make sure user exists and that user is creator of character
            if (existing != null && existing.UserId == _currentUserId)
            {
                // Ties content in db to form to edit
                characterModel = new CreateCharacterModel
                {
                    Name = existing.Name,
                    Race = existing.Race,
                    Class = existing.Class,
                    Background = existing.Background ?? "",
                    Alignment = existing.Alignment ?? "",
                    Level = existing.Level,
                    Strength = existing.Strength,
                    Dexterity = existing.Dexterity,
                    Constitution = existing.Constitution,
                    Intelligence = existing.Intelligence,
                    Wisdom = existing.Wisdom,
                    Charisma = existing.Charisma,
                    HitPoints = existing.HitPoints,
                    Backstory = existing.Backstory ?? ""
                };
            }
        }
    }

    [SupplyParameterFromForm]
    public CreateCharacterModel characterModel { get; set; } = default!;

    protected override void OnInitialized()
    {
        characterModel ??= new CreateCharacterModel();
    }

    private int _currentUserId;
    private string? errorMessage;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated == true)
        {
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null) _currentUserId = int.Parse(idClaim.Value);
        }
    }

    private readonly Random _rng = new();

    // Stores last set of dice rolls per stat for display
    private readonly Dictionary<string, int[]> _lastRolls = new();

    private int GetStat(string stat) => stat switch
    {
        "Strength"     => characterModel.Strength,
        "Dexterity"    => characterModel.Dexterity,
        "Constitution" => characterModel.Constitution,
        "Intelligence" => characterModel.Intelligence,
        "Wisdom"       => characterModel.Wisdom,
        "Charisma"     => characterModel.Charisma,
        _              => 0
    };

    private void SetStat(string stat, int value)
    {
        switch (stat)
        {
            case "Strength":     characterModel.Strength     = value; break;
            case "Dexterity":    characterModel.Dexterity    = value; break;
            case "Constitution": characterModel.Constitution = value; break;
            case "Intelligence": characterModel.Intelligence = value; break;
            case "Wisdom":       characterModel.Wisdom       = value; break;
            case "Charisma":     characterModel.Charisma     = value; break;
        }
    }

    private int[] GetLastRoll(string stat) =>
        _lastRolls.TryGetValue(stat, out var rolls) ? rolls : [];

    private void RollStat(string stat)
    {
        int[] dice = [_rng.Next(1, 7), _rng.Next(1, 7), _rng.Next(1, 7), _rng.Next(1, 7)];
        _lastRolls[stat] = dice;
        int total = dice.OrderByDescending(d => d).Take(3).Sum();
        SetStat(stat, total);
    }

    private void RollAllStats()
    {
        foreach (var stat in GameConstants.StatNames)
            RollStat(stat);
    }

    private string GetHitDie() =>
        !string.IsNullOrEmpty(characterModel.Class) && GameConstants.ClassHitDice.TryGetValue(characterModel.Class, out var die)
            ? $"d{die}"
            : "d8";

    private void RollHitPoints()
    {
        int die = !string.IsNullOrEmpty(characterModel.Class) && GameConstants.ClassHitDice.TryGetValue(characterModel.Class, out var d) ? d : 8;
        int conMod = GetModifier(characterModel.Constitution);
        int total = Math.Max(1, _rng.Next(1, die + 1) + conMod);
        characterModel.HitPoints = total;
    }

    private static int GetModifier(int score) => (score - 10) / 2;

    private static string GetModifierString(int score)
    {
        int mod = GetModifier(score);
        return mod >= 0 ? $"+{mod}" : $"{mod}";
    }

    async private Task HandleSubmit()
    {
        try
        {
            Models.Character? dbCharacter;

            // Double check if editing or making new character
            if (Id.HasValue)
            {
                // If exist, set to update
                dbCharacter = await AppDbContext.Characters.FindAsync(Id.Value);
                if (dbCharacter == null || dbCharacter.UserId != _currentUserId) return;
            }
            else
            {
                // If making new, create new character
                dbCharacter = new Models.Character { UserId = _currentUserId };
                AppDbContext.Characters.Add(dbCharacter);
            }

            // Mapping values from model to entity
            dbCharacter.Name = characterModel.Name;
            dbCharacter.Race = characterModel.Race;
            dbCharacter.Class = characterModel.Class;
            dbCharacter.Background = characterModel.Background;
            dbCharacter.Alignment = characterModel.Alignment;
            dbCharacter.Level = characterModel.Level;
            dbCharacter.Strength = characterModel.Strength;
            dbCharacter.Dexterity = characterModel.Dexterity;
            dbCharacter.Constitution = characterModel.Constitution;
            dbCharacter.Intelligence = characterModel.Intelligence;
            dbCharacter.Wisdom = characterModel.Wisdom;
            dbCharacter.Charisma = characterModel.Charisma;
            dbCharacter.HitPoints = characterModel.HitPoints;
            dbCharacter.Backstory = characterModel.Backstory;
            
            // Save to database (either edits or new character)
            await AppDbContext.SaveChangesAsync();
            Navigation.NavigateTo("/characters");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            errorMessage = "A database error occurred. Please try again.";
        }
    }
}