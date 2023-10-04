namespace PenShop.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

public abstract class ProductOrder
{
    public int Id{ get; set; }
    public int? CustomerId{ get; set; }
    public virtual Customer? Customer { get; set; }
    public int? OrderId{ get; set; }
    public virtual Order? Order { get; set; }
    public abstract int ProductId{ get; }
    public abstract Product? Product{ get; }
    public int Quantity{ get; set; }
    public abstract float Price{ get; }
}
