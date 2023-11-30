namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "Ink")]
public abstract class Ink : Product
{
    [Required]
    [Range(1,1000)]
    [Display(Name = "Capacity")]
    public float Capacity{ get; set; }
    [Required]
    public int ColourId{ get; set; }
    [Display(Name = "Colour")]
    public virtual InkColour? Colour{ get; set; }
}
