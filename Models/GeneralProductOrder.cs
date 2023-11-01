namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class GeneralProductOrder : ProductOrder
{
    [Required]
    public int GeneralProductId{ get; set; }
    public override int ProductId => GeneralProductId;
    private Product? _generalProduct;
    [Display(Name = "Product")]
    public virtual Product? GeneralProduct{ get => _generalProduct; set => _generalProduct = value is not FountainPen ? value : throw new ArgumentOutOfRangeException("A general product order may not be used for a fountain pen"); }
    public override Product? Product => GeneralProduct;
    [DataType (DataType.Currency)]
    public override float Price => Product?.Price ?? float.PositiveInfinity;
}
