namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class RollerballPen : Pen
{
    [Required]
    [Display(Name = "Rollerball diameter")]
    [Range(1, 10)]
    public float RollerballDiameter {get; set; }
}
