namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class NibAccessory : Accessory
{
    [Required]
    public int NibId{ get; set; }
    public virtual Nib? Nib{ get; set; }
    [DataType (DataType.Currency)]
    [Range(1, double.PositiveInfinity)]
    public override float Price{
        get => Nib?.Price ?? float.PositiveInfinity;
        set => throw new NotImplementedException();
    }
}
