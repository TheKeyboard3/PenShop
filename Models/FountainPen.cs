namespace PenShop.Models;

using System.Web;

public class FountainPen : Pen
{
    public int NibId{ get; set; }
    public virtual Nib? Nib{ get; set; }
}
