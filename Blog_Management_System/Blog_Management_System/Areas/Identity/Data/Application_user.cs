using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blog_Management_System.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Application_user class
public class Application_user : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string FName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }


    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Phone { get; set; }


}

