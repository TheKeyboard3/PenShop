namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public abstract class Pen : Product
{
    [Required]
    public int CartridgeStandardId{ get; set; }
    [Display(Name = "Cartridge standard")]
    public virtual CartridgeStandard? CartridgeStandard{ get; set; }
    [Required]
    public int MaterialId{ get; set; }
    public virtual Material? Material{ get; set; }
}
