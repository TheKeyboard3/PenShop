namespace PenShop.Models;

using System.Web;

public abstract class Product
{
    public int Id { get; set; }
    public virtual float Price { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
