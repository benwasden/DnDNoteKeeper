using System;
using System.ComponentModel.DataAnnotations;

namespace DnDNoteKeeper.Models.ViewModels;

public class CampaignViewModel
{
    [StringLength(45, ErrorMessage = "Campaign name cannot exceed 45 characters")]
    [Required(ErrorMessage = "Campaign name is required")]
    public string? Name { get; set; }

    [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
    [Required(ErrorMessage = "Please enter a campaign description")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Please select when the campaign will begin")]
    public DateTime? Time { get; set; }
    
    [Required(ErrorMessage = "Location is required")]
    [StringLength(45, ErrorMessage = "Location cannot exceed 45 characters")]
    public string? Location { get; set; }
    
    public int UserId { get; set; }
}
