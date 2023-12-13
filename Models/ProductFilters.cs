namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Display(Name = "Filters")]
public class ProductFilters : IValidatableObject
{
    [Required]
    [Display(Name = "Price")]
    public NumericFilter Price { get; set; } = new NumericFilter();

    public bool Match(Product product){
        return Price.Match(product.Price);
    }

    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext){
        return Price.Validate(validationContext);
    }
}
