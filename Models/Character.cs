using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDNoteKeeper.Models;

[Table("character")]
public class Character
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("character_id")]
    public int Id { get; set; }

    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column("race")]
    [MaxLength(50)]
    public string Race { get; set; } = string.Empty;

    [Column("class")]
    [MaxLength(50)]
    public string Class { get; set; } = string.Empty;

    [Column("background")]
    [MaxLength(50)]
    public string? Background { get; set; }

    [Column("alignment")]
    [MaxLength(30)]
    public string? Alignment { get; set; }

    [Column("level")]
    public int Level { get; set; } = 1;

    [Column("strength")]
    public int Strength { get; set; }

    [Column("dexterity")]
    public int Dexterity { get; set; }

    [Column("constitution")]
    public int Constitution { get; set; }

    [Column("intelligence")]
    public int Intelligence { get; set; }

    [Column("wisdom")]
    public int Wisdom { get; set; }

    [Column("charisma")]
    public int Charisma { get; set; }

    [Column("hit_points")]
    public int HitPoints { get; set; }

    [Column("backstory")]
    public string? Backstory { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("campaign_id")]
    public int? CampaignId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
