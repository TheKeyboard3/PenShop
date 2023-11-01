namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class InkCartridge : Ink
{
    [Required]
    public int CartridgeStandardId{ get; set; }
    [Display(Name = "Cartridge standard")]
    public virtual CartridgeStandard? CartridgeStandard{ get; set; }
}
