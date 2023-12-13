namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "Filters")]
public class InkCartridgeFilters : InkFilters
{
    [Required]
    [Display(Name = "CartridgeStandard")]
    public CartridgeStandardFilter CartridgeStandard{ get; set; } = new CartridgeStandardFilter();

    public bool MatchInkCartridge(InkCartridge inkCartridge){
        return base.MatchInk(inkCartridge) && CartridgeStandard.Match(inkCartridge.CartridgeStandardId);
    }
}
