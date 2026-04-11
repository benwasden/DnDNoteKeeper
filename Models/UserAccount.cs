using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDNoteKeeper.Models;

[Table("user_account")]

public class UserAccount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("user_id")]
    public int Id { get; set; }

    [Column("username")]
    [MaxLength(100)]

    public string? UserName { get; set; }

    [Column("password")]
    [MaxLength(100)]

    public string? Password { get; set; }
}