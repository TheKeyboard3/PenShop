namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "Material")]
public class Material
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    [Display(Name = "Name")]
    public string? Name { get; set; }
}
