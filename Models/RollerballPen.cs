namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "RollerballPen")]
public class RollerballPen : Pen
{
    [Required]
    [Display(Name = "RollerballDiameter")]
    [Range(1, 10)]
    public float RollerballDiameter {get; set; }
}
