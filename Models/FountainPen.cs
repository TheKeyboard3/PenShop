namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;


[Display(Name = "FountainPen")]
public class FountainPen : Pen
{
    [Required]
    public int NibId{ get; set; }
    [Display(Name = "Nib")]
    public virtual Nib? Nib{ get; set; }
}
