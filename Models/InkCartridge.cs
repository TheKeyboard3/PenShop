namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class InkCartridge : Ink
{

    public int CartridgeStandardId{ get; set; }
    [Display(Name = "Cartridge standard")]
    public virtual CartridgeStandard? CartridgeStandard{ get; set; }
}
