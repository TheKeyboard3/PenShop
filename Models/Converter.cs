namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class Converter : Accessory
{
    public float? Height{ get; set; }
    public float? Capacity{ get; set; }
    public int CartridgeStandardId{ get; set; }
    [Display(Name = "Cartridge standard")]
    public virtual CartridgeStandard? CartridgeStandard{ get; set; }
}
