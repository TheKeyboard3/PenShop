namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class NibMaterial
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
    [Required]
    [Range(1, 10)]
    public double Hardness { get; set; }
    public string Text => (Name ?? "") + " " + Hardness;
}
