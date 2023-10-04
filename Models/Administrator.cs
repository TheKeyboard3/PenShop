namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Administrator
{
    public int Id { get; set; }
    public string? Email{ get; set; }
    [Display(Name = "Password hash")]
    public string? PasswordHash{ get; set; }
    [NotMapped]
    public string? Password { get; set; }
}
