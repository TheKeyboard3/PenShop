namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class FountainPenOrder : ProductOrder
{
    [Required]
    public int PenId{ get; set; }
    public override int ProductId => PenId;
    public virtual FountainPen? Pen{ get; set; }
    public override Product? Product => Pen;
    [Required]
    [Display(Name = "Remove the nib")]
    public bool RemoveNib{ get; set; }
    [DataType (DataType.Currency)]
    public override float Price => RemoveNib ? (Pen?.Price ?? float.PositiveInfinity - Pen?.Nib?.Price ?? 0) : Pen?.Price ?? float.PositiveInfinity;
}
