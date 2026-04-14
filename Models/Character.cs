using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDNoteKeeper.Models;

[Table("characters")]
public class Character
{
    [Key]
    [Column("character_Id")]
    public int CharacterId { get; set; }

    [Required]
    [Column("user_Id")]
    public int UserId { get; set; }

    [Required]
    [Column("character_name")]
    [MaxLength(100)]
    public string Name { get; set; } = "";

    [Required]
    [Column("race")]
    [MaxLength(50)]
    public string Race { get; set; } = "";

    [Required]
    [Column("class")]
    [MaxLength(50)]
    public string Class { get; set; } = "";

    [Column("background")]
    [MaxLength(100)]
    public string? Background { get; set; }

    [Column("alignment")]
    [MaxLength(50)]
    public string? Alignment { get; set; }

    [Column("character_level")]
    public int Level { get; set; } = 1;

    [Column("strength")]
    public int Strength { get; set; } = 10;

    [Column("dexterity")]
    public int Dexterity { get; set; } = 10;

    [Column("constitution")]
    public int Constitution { get; set; } = 10;

    [Column("intelligence")]
    public int Intelligence { get; set; } = 10;

    [Column("wisdom")]
    public int Wisdom { get; set; } = 10;

    [Column("charisma")]
    public int Charisma { get; set; } = 10;

    [Column("hit_points")]
    public int HitPoints { get; set; } = 10;

    [Column("backstory")]
    [MaxLength(500)]
    public string? Backstory { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual UserAccount? UserAccount { get; set; }
}