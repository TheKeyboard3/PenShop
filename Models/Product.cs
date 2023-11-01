namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class Product
{
    public int Id { get; set; }
    [Required]
    [DataType (DataType.Currency)]
    [Range(1, 50000)]
    public virtual float Price { get; set; }
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
    [DataType (DataType.MultilineText)]
    [Required]
    [StringLength(500)]
    public string? Description { get; set; }
    [Display(Name = "Image")]
    public string? ImageName { get; set; }
    [NotMapped]
    [Display(Name = "Image")]
    public IFormFile? ImageFile { get; set; }
    [NotMapped]
    public string ImageNameForDisplay => ImageName ?? "no-image.jpg";
}
