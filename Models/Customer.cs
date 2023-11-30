namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


[Display(Name = "Customer")]
public class Customer : User
{
    [Required]
    [StringLength(50)]
    [Display(Name = "PersonName")]
    public string? Name{ get; set; }
    [Required]
    [StringLength(50)]
    [Display(Name = "Surname")]
    public string? Surname{ get; set; }
    [Display(Name = "DefaultShippingAddress")]
    [StringLength(1000)]
    public string? DefaultShippingAddress{ get; set; }
    public virtual ICollection<Order>? Orders{ get; set;}
    public virtual ICollection<ProductOrder>? ShoppingCart{ get; set; }
    [Display(Name = "FullName")]
    public string? FullName => (Name is not null ? Name : "") + " " + (Surname is not null ? Surname : "");
}
