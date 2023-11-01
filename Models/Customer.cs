namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Customer
{
    public int Id { get; set; }
    [Required]
    [DataType (DataType.EmailAddress)]
    [StringLength(128)]
    public string? Email{ get; set; }
    [Display(Name = "Password hash")]
    public string? PasswordHash{ get; set; }
    [Required]
    [StringLength(50)]
    public string? Name{ get; set; }
    [Required]
    [StringLength(50)]
    public string? Surname{ get; set; }
    [Display(Name = "Default shipping address")]
    [StringLength(1000)]
    public string? DefaultShippingAddress{ get; set; }
    public virtual ICollection<Order>? Orders{ get; set;}
    public virtual ICollection<ProductOrder>? ShoppingCart{ get; set; }
    [Display(Name = "Full name")]
    public string? FullName => (Name is not null ? Name : "") + " " + (Surname is not null ? Surname : "");
    [NotMapped]
    [DataType (DataType.Password)]
    [StringLength(128)]
    public string? Password { get; set; }
}
