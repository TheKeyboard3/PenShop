namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "Converter")]
public class Converter : Accessory
{
    [Required]
    [Range(0, 1000)]
    [Display(Name = "Height")]
    public float Height{ get; set; }
    [Required]
    [Range(1, 100)]
    [Display(Name = "Capacity")]
    public float Capacity{ get; set; }
    [Required]
    public int CartridgeStandardId{ get; set; }
    [Display(Name = "CartridgeStandard")]
    public virtual CartridgeStandard? CartridgeStandard{ get; set; }
}
