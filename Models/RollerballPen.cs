namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class RollerballPen : Pen
{
    [Display(Name = "Rollerball diameter")]
    public float RollerballDiameter {get; set; }
}
