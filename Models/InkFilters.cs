namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "Filters")]
public class InkFilters : ProductFilters
{
    [Required]
    [Display(Name = "Capacity")]
    public NumericFilter Capacity{ get; set; } = new NumericFilter();

    public bool MatchInk(Ink ink){
        return base.Match(ink) && Capacity.Match(ink.Capacity);
    }

    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext){
        return base.Validate(validationContext).Concat(Capacity.Validate(validationContext));
    }
}
