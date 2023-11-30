namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

[Display(Name = "User")]
public abstract class User : IdentityUser
{
    [Display(Name = "EmailConfirmed")]
    public new bool EmailConfirmed
    {
        get
        {
            return base.EmailConfirmed;
        }
        set
        {
            base.EmailConfirmed = value;
        }
    }
    [DataType(DataType.DateTime)]
    [Display(Name = "LockoutEnd")]
    public new DateTimeOffset? LockoutEnd
    {
        get
        {
            return base.LockoutEnd;
        }
        set
        {
            base.LockoutEnd = value;
        }
    }
    [Display(Name = "LockoutEnabled")]
    public new bool LockoutEnabled
    {
        get
        {
            return base.LockoutEnabled;
        }
        set
        {
            base.LockoutEnabled = value;
        }
    }
    [Display(Name = "FailedLogins")]
    public new int AccessFailedCount{
        get
        {
            return base.AccessFailedCount;
        }
        set
        {
            base.AccessFailedCount = value;
        }
    }
}
