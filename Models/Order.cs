namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class Order
{
    public int Id{ get; set; }
    public int? CustomerId{ get; set; }
    public virtual Customer? Customer{ get; set; }
    public virtual ICollection<ProductOrder>? ProductOrders{ get; set; }
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    [Display(Name = "Shipping address")]
    public string? ShippingAddress{ get; set; }
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
