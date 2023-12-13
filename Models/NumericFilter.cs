namespace PenShop.Models;

using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class NumericFilter : IValidatableObject
{
    [Required]
    [Range(1, 50000)]
    [Display(Name = "From")]
    public float Min{ get; set; } = 1;
    [Required]
    [Range(1, 50000)]
    [Display(Name = "To")]
    public float Max{ get; set; } = 50000;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext){
        List<ValidationResult> validationResults = new List<ValidationResult>();

        if (Min > Max)
            validationResults.Add(new ValidationResult("Min must be less or equal to Max"));

        return validationResults;
    }

    public bool Match(double value){
        return value >= Min && value <= Max;
    }
}
