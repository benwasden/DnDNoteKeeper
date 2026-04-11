using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDNoteKeeper.Models;

[Table("user_account")]

public class UserAccount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_name")]
    [MaxLength(100)]

    public string? UserName { get; set; }

    [Column("password")]
    [MaxLength(255)]

    public string? Password { get; set; }
}