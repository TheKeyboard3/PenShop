namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public abstract class Pen : Product
{
    public int CartridgeStandardId{ get; set; }
    [Display(Name = "Cartridge standard")]
    public virtual CartridgeStandard? CartridgeStandard{ get; set; }
    public int MaterialId{ get; set; }
    public virtual Material? Material{ get; set; }
}
