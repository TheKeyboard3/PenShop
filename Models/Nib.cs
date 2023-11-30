namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

[Display(Name = "Nib")]
public class Nib
{
    public int Id { get; set; }
    [Required]
    public int BodyMaterialId{ get; set; }
    [Display(Name = "BodyMaterial")]
    public virtual NibMaterial? BodyMaterial{ get; set; }
    [Required]
    public int TipMaterialId{ get; set; }
    [Display(Name = "TipMaterial")]
    public virtual NibMaterial? TipMaterial{ get; set; }
    [Required]
    [Range(1,10)]
    [Display(Name = "TipDiameter")]
    public float TipDiameter{ get; set; }
    [Required]
    [DataType (DataType.Currency)]
    [Range(1,10000)]
    [Display(Name = "Price")]
    public float Price{ get; set; }
    [Display(Name = "Name")]
    public string Name {
        get{
            string bodyMaterialName = BodyMaterial is not null ? BodyMaterial!.Name! : "";
            string tipMaterialName = TipMaterial is not null ? TipMaterial!.Name! : "";
            string retval = bodyMaterialName;
            if(tipMaterialName != bodyMaterialName)
                retval+="tipped with " + tipMaterialName;

            retval += $", tip diameter: {TipDiameter}";
            return retval;
        }
    }
}
