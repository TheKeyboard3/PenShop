namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "NibMaterial")]
public class NibMaterial
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    [Display(Name = "Name")]
    public string? Name { get; set; }
    [Required]
    [Range(1, 10)]
    [Display(Name = "Hardness")]
    public double Hardness { get; set; }
    public string Text => (Name ?? "") + " " + Hardness;
}
