namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "CartridgeStandard")]
public class CartridgeStandard
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    [Display(Name = "Name")]
    public string? Name { get; set; }
}
