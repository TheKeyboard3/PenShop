namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Customer
{
    public int Id { get; set; }
    public string? Email{ get; set; }
    [Display(Name = "Password hash")]
    public string? PasswordHash{ get; set; }
    public string? Name{ get; set; }
    public string? Surname{ get; set; }
    [Display(Name = "Default shipping address")]
    public string? DefaultShippingAddress{ get; set; }
    public virtual ICollection<Order>? Orders{ get; set;}
    public virtual ICollection<ProductOrder>? ShoppingCart{ get; set; }
    [Display(Name = "Full name")]
    public string? FullName => (Name is not null ? Name : "") + " " + (Surname is not null ? Surname : "");
    [NotMapped]
    public string? Password { get; set; }
}
