namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "FountainPenOrder")]
public class FountainPenOrder : ProductOrder
{
    [Required]
    public int PenId{ get; set; }
    public override int ProductId => PenId;
    [Display(Name = "Pen")]
    public virtual FountainPen? Pen{ get; set; }
    [Display(Name = "Product")]
    public override Product? Product => Pen;
    [Required]
    [Display(Name = "RemoveNib")]
    public bool RemoveNib{ get; set; }
    [DataType (DataType.Currency)]
    [Display(Name = "Price")]
    public override float Price => RemoveNib ? (Pen?.Price ?? float.PositiveInfinity - Pen?.Nib?.Price ?? 0) : Pen?.Price ?? float.PositiveInfinity;
}
