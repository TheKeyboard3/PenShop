namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "Filters")]
public class ConverterFilters : AccessoryFilters
{
    [Required]
    [Display(Name = "CartridgeStandard")]
    public CartridgeStandardFilter CartridgeStandard{ get; set; } = new CartridgeStandardFilter();

    public bool MatchConverter(Converter converter){
        return base.Match(converter) && CartridgeStandard.Match(converter.CartridgeStandardId);
    }
}
