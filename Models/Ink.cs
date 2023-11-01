namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public abstract class Ink : Product
{
    [Required]
    [Range(1,1000)]
    public float Capacity{ get; set; }
    [Required]
    public int ColourId{ get; set; }
    public virtual InkColour? Colour{ get; set; }
}
