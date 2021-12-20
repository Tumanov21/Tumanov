using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.ViewsModels.Users
{
    public class PasswordUsersViewsModels
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="NewPassword")]
        public string NewPassword { get; set; }
    }
}
