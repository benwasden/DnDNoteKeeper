using System;
using System.ComponentModel.DataAnnotations;

namespace DnDNoteKeeper.Models.ViewModels;

public class CampaignViewModel
{
    [Required(ErrorMessage = "Campaign name is required")]
    [StringLength(100, ErrorMessage = "Campaign name cannot exceed 100 characters")]

    public string Name { get; set; } = string.Empty;
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]

    [Required(ErrorMessage = "Please enter a campaign description")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Please select when the campaign will begin")]
    public DateTime? Time { get; set; }
    
    [Required(ErrorMessage = "Location is required")]
    [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters")]
    public string? Location { get; set; }
    
    [Required(ErrorMessage = "User ID is required")]
    public int UserId { get; set; }
}
