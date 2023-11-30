namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

[Display(Name = "Administrator")]
public class Administrator : User
{
}
