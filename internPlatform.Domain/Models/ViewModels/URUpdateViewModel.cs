using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace internPlatform.Domain.Models.ViewModels
{
    public class URUpdateViewModel 
    {
        public URManager User  { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        [DisplayName("Role")]
        public string SelectedRole{ get; set; }

        [DisplayName("New Password")]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "Password must contain at least one non digit and letter character")]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.CompareAttribute("NewPassword")]
        public string ConfirmPassword { get; set; }


        public URUpdateViewModel() 
        {
        }
        public URUpdateViewModel(URManager user , IEnumerable<SelectListItem> roles)
        {
            User = user;
            Roles = roles;  
        }

    }
}