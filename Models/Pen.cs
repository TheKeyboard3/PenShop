namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "Pen")]
public abstract class Pen : Product
{
    [Required]
    public int CartridgeStandardId{ get; set; }
    [Display(Name = "CartridgeStandard")]
    public virtual CartridgeStandard? CartridgeStandard{ get; set; }
    [Required]
    public int MaterialId{ get; set; }
    [Display(Name = "Material")]
    public virtual Material? Material{ get; set; }
}
