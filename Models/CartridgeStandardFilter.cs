namespace PenShop.Models;

using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CartridgeStandardFilter
{
    public int? CartridgeStandardId{ get; set; } = null;

    public bool Match(int cartridgeStandardId){
        return CartridgeStandardId is null || cartridgeStandardId == CartridgeStandardId;
    }
}
