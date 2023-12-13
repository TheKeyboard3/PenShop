namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "Filters")]
public class PenFilters : ProductFilters
{
    [Required]
    [Display(Name = "CartridgeStandard")]
    public CartridgeStandardFilter CartridgeStandard{ get; set; } = new CartridgeStandardFilter();

    public bool MatchPen(Pen pen){
        return base.Match(pen) && CartridgeStandard.Match(pen.CartridgeStandardId);
    }
}
