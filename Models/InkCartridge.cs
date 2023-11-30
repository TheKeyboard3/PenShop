namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "InkCartridge")]
public class InkCartridge : Ink
{
    [Required]
    public int CartridgeStandardId{ get; set; }
    [Display(Name = "CartridgeStandard")]
    public virtual CartridgeStandard? CartridgeStandard{ get; set; }
}
