namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "Order")]
public class Order
{
    public int Id{ get; set; }
    [Required]
    public string? CustomerId{ get; set; }
    [Display(Name = "Customer")]
    public virtual Customer? Customer{ get; set; }
    public virtual ICollection<ProductOrder>? ProductOrders{ get; set; }
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date")]
    public DateTime Date { get; set; }
    [Required]
    [StringLength(1000)]
    [Display(Name = "ShippingAddress")]
    public string? ShippingAddress{ get; set; }
    [DataType (DataType.Currency)]
    [Display(Name = "Price")]
    public float Price{
        get{
            if(ProductOrders is null)
                return float.PositiveInfinity;

            float retval = 0;
            foreach(ProductOrder po in ProductOrders)
                retval += po.Price;

            return retval;
        }
    }
    public string Text{
        get{
            return "By " + (Customer?.FullName ?? "Unknown") + " from " + Date + " for " + Price;
        }
    }
}
