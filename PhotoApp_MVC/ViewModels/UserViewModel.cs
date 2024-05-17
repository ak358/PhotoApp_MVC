using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoApp_MVC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhotoApp_MVC.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "有効なメールアドレスを入力してください。")]
        public string EmailAdress { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        [ValidateNever]
        public List<SelectListItem> Roles { get; set; }
    }
}
