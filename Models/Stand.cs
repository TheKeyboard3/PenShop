namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class Stand : Accessory
{
    [Required]
    public int MaterialId{ get; set; }
    public virtual Material? Material{ get; set; }
}
