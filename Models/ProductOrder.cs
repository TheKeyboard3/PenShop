namespace PenShop.Models;


using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class ProductOrder : IValidatableObject
{
    public int Id{ get; set; }
    public int? CustomerId{ get; set; }
    public virtual Customer? Customer { get; set; }
    public int? OrderId{ get; set; }
    public virtual Order? Order { get; set; }
    [Required]
    public abstract int ProductId{ get; }
    public abstract Product? Product{ get; }
    [Required]
    [Range(1, 100)]
    public int Quantity{ get; set; }
    [Required]
    [DataType (DataType.Currency)]
    public abstract float Price{ get; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext){
        List<ValidationResult> validationResults = new List<ValidationResult>();

        if(CustomerId is null && OrderId is null)
            validationResults.Add(new ValidationResult("A customer or an order must be specified"));

        if(CustomerId is not null && OrderId is not null)
            validationResults.Add(new ValidationResult("A customer and an order may not be specified simultaneously"));

        return validationResults;
    }
}
