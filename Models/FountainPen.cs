namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class FountainPen : Pen
{
    [Required]
    public int NibId{ get; set; }
    public virtual Nib? Nib{ get; set; }
}
