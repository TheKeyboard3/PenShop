namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "Nib")]
public class NibAccessory : Accessory
{
    [Required]
    public int NibId{ get; set; }
    [Display(Name = "Nib")]
    public virtual Nib? Nib{ get; set; }
    [DataType (DataType.Currency)]
    [Range(1, double.PositiveInfinity)]
    [Display(Name = "Price")]
    public override float Price{
        get => Nib?.Price ?? float.PositiveInfinity;
        set => throw new NotImplementedException();
    }
}
