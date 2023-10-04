namespace PenShop.Models;

using System.Web;

public abstract class Ink : Product
{
    public float? Capacity{ get; set; }
    public int ColourId{ get; set; }
    public virtual InkColour? Colour{ get; set; }
}
