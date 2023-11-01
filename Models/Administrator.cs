namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Administrator
{
    public int Id { get; set; }
    [Required]
    [DataType (DataType.EmailAddress)]
    [StringLength(128)]
    public string? Email{ get; set; }
    [Display(Name = "Password hash")]
    public string? PasswordHash{ get; set; }
    [NotMapped]
    [DataType (DataType.Password)]
    [StringLength(128)]
    public string? Password { get; set; }
}
