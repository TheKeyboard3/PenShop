namespace PenShop.Models;

using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public abstract class User : IdentityUser
{
    [Display(Name = "Email confirmed")]
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
    [Display(Name = "Lockout end")]
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
    [Display(Name = "Lockout enabled")]
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
    [Display(Name = "Failed logins")]
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
