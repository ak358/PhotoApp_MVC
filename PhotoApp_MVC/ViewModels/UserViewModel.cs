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
        [Display(Name = "���O")]
        public string Name { get; set; }
        [Display(Name = "���[���A�h���X")]

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "�L���ȃ��[���A�h���X����͂��Ă��������B")]
        public string EmailAdress { get; set; }
        [Display(Name = "�p�X���[�h")]
        public string Password { get; set; }
        [Display(Name = "���[��")]
        public string RoleName { get; set; } = "User";
        [ValidateNever]
        public List<SelectListItem> Roles { get; set; }
    }
}
