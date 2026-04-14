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

    async private void HandleSubmit()
    {
        Console.WriteLine($"Character created: {characterModel.Name} ({characterModel.Race} {characterModel.Class})");
        // Save to database and navigate
        try
        {
            // Map model to the database entity
            var dbCharacter = new Models.Character
            {
                UserId = _currentUserId,
                Name = characterModel.Name,
                Race = characterModel.Race,
                Class = characterModel.Class,
                Background = characterModel.Background,
                Alignment = characterModel.Alignment,
                Level = characterModel.Level,
                Strength = characterModel.Strength,
                Dexterity = characterModel.Dexterity,
                Constitution = characterModel.Constitution,
                Intelligence = characterModel.Intelligence,
                Wisdom = characterModel.Wisdom,
                Charisma = characterModel.Charisma,
                HitPoints = characterModel.HitPoints,
                Backstory = characterModel.Backstory
            };

            AppDbContext.Characters.Add(dbCharacter);
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