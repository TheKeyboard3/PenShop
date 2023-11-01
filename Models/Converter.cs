namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class Converter : Accessory
{
    [Required]
    [Range(0, 1000)]
    public float Height{ get; set; }
    [Required]
    [Range(1, 100)]
    public float Capacity{ get; set; }
    [Required]
    public int CartridgeStandardId{ get; set; }
    [Display(Name = "Cartridge standard")]
    public virtual CartridgeStandard? CartridgeStandard{ get; set; }
}
