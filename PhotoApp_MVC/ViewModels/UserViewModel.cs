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
        [Display(Name = "名前")]
        public string Name { get; set; }
        [Display(Name = "メールアドレス")]

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "有効なメールアドレスを入力してください。")]
        public string EmailAdress { get; set; }
        [Display(Name = "パスワード")]
        public string Password { get; set; }
        [Display(Name = "ロール")]
        public string RoleName { get; set; } = "User";
        [ValidateNever]
        public List<SelectListItem> Roles { get; set; }
    }
}
