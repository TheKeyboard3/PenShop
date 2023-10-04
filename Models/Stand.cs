namespace PenShop.Models;

using System.Web;

public class Stand : Accessory
{
    public int MaterialId{ get; set; }
    public virtual Material? Material{ get; set; }
}
