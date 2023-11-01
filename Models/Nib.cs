namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;

public class Nib
{
    public int Id { get; set; }
    [Required]
    public int BodyMaterialId{ get; set; }
    [Display(Name = "Body material")]
    public virtual NibMaterial? BodyMaterial{ get; set; }
    [Required]
    public int TipMaterialId{ get; set; }
    [Display(Name = "Tip material")]
    public virtual NibMaterial? TipMaterial{ get; set; }
    [Required]
    [Display(Name = "Tip diameter")]
    [Range(1,10)]
    public float TipDiameter{ get; set; }
    [Required]
    [DataType (DataType.Currency)]
    [Range(1,10000)]
    public float Price{ get; set; }
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
