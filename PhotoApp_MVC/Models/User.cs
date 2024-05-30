using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp_MVC.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "���O����͂��Ă��������B")]
        public string Name { get; set; }

        [Required(ErrorMessage = "���[���A�h���X����͂��Ă��������B")]
        [EmailAddress(ErrorMessage = "�L���ȃ��[���A�h���X����͂��Ă��������B")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "�L���ȃ��[���A�h���X����͂��Ă��������B")]
        
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "�p�X���[�h����͂��Ă��������B")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "�p�X���[�h��6�����ȏ�20�����ȉ��œ��͂��Ă��������B")]
        public string Password { get; set; }

        [ValidateNever]
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ValidateNever]
        public List<Category> Categories { get; set; }

        [ValidateNever]
        public List<PhotoPost> PhotoPosts { get; set; }

        public int RoleId { get; internal set; }
    }
}
