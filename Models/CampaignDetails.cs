using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDNoteKeeper.Models;

[Table("campaigns")]
public class CampaignDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("campaign_id")]
    public int Id { get; set; }

    [Column("name")]
    [MaxLength(50)]
    public string? CampaignName { get; set; }

    [Column("description")]
    [MaxLength(400)]
    public string? Description { get; set; }

    [Column("campaign_image")]
    [MaxLength(2048)]
    public string? CampaignImage { get; set; }

    [Column("location")]
    [MaxLength(200)]
    public string? Location { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("user_name")]
    [MaxLength(50)]
    public string? UserName { get; set; }
}