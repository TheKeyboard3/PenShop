namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class Material
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
}
