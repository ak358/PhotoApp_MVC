using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp_MVC.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "�L���ȃ��[���A�h���X����͂��Ă��������B")]
        public string EmailAdress { get; set; }
        public string Password { get; set; }
        [ValidateNever]
        public Role Role { get; set; }
        [ValidateNever]
        public List<Category> Categories { get; set; }
        [ValidateNever]
        public List<PhotoPost> PhotoPosts { get; set; }
    }
}
