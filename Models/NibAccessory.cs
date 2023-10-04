namespace PenShop.Models;

using System.Web;

public class NibAccessory : Accessory
{
    public int NibId{ get; set; }
    public virtual Nib? Nib{ get; set; }
    public override float Price{
        get => Nib!.Price;
        set => throw new NotImplementedException();
    }
}
