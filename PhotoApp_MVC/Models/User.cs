using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp_MVC.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "名前を入力してください。")]
        public string Name { get; set; }

        [Required(ErrorMessage = "メールアドレスを入力してください。")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください。")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "有効なメールアドレスを入力してください。")]
        
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "パスワードを入力してください。")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "パスワードは6文字以上20文字以下で入力してください。")]
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
