namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Display(Name = "Product")]
public abstract class Product
{
    public int Id { get; set; }
    [Required]
    [Range(1, 50000)]
    [Display(Name = "Price")]
    public virtual float Price { get; set; }
    [Required]
    [StringLength(50)]
    [Display(Name = "Name")]
    public string? Name { get; set; }
    [DataType (DataType.MultilineText)]
    [Required]
    [StringLength(500)]
    [Display(Name = "Description")]
    public string? Description { get; set; }
    [Display(Name = "Image")]
    public string? ImageName { get; set; }
    [NotMapped]
    [Display(Name = "Image")]
    public IFormFile? ImageFile { get; set; }
    [NotMapped]
    public string ImageNameForDisplay => ImageName ?? "no-image.jpg";
}
