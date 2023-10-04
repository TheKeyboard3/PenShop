namespace PenShop.Models;

using System.Web;

public class NibMaterial
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Hardness { get; set; }
    public string Text => (Name ?? "") + " " + Hardness;
}
