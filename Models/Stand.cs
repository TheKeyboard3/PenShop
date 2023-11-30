namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "Stand")]
public class Stand : Accessory
{
    [Required]
    public int MaterialId{ get; set; }
    [Display(Name = "Material")]
    public virtual Material? Material{ get; set; }
}
