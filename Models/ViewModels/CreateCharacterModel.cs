using System.ComponentModel.DataAnnotations;

namespace DnDNoteKeeper.Models;

public class CreateCharacterModel
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Please select a race.")]
    [StringLength(50)]
    public string Race { get; set; } = "";

    [Required(ErrorMessage = "Please select a class.")]
    [StringLength(50)]
    public string Class { get; set; } = "";

    [StringLength(100, ErrorMessage = "Background cannot exceed 100 characters.")]
    public string Background { get; set; } = "";

    [StringLength(50, ErrorMessage = "Alignment cannot exceed 50 characters.")]
    public string Alignment { get; set; } = "";

    [Range(1, 20, ErrorMessage = "Level must be between 1 and 20.")]
    public int Level { get; set; } = 1;

    [Range(1, 30, ErrorMessage = "Stats must be between 1 and 30.")]
    public int Strength { get; set; } = 10;

    [Range(1, 30, ErrorMessage = "Stats must be between 1 and 30.")]
    public int Dexterity { get; set; } = 10;

    [Range(1, 30, ErrorMessage = "Stats must be between 1 and 30.")]
    public int Constitution { get; set; } = 10;

    [Range(1, 30, ErrorMessage = "Stats must be between 1 and 30.")]
    public int Intelligence { get; set; } = 10;

    [Range(1, 30, ErrorMessage = "Stats must be between 1 and 30.")]
    public int Wisdom { get; set; } = 10;

    [Range(1, 30, ErrorMessage = "Stats must be between 1 and 30.")]
    public int Charisma { get; set; } = 10;

    [Range(1, 500, ErrorMessage = "Hit points must be at least 1.")]
    public int HitPoints { get; set; } = 10;

    [StringLength(500, ErrorMessage = "Backstory cannot exceed 500 characters.")]
    public string Backstory { get; set; } = "";
}