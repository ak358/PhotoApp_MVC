using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PhotoApp_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Contact
{
    public int Id { get; set; }

    [Required(ErrorMessage = "お名前を入力してください。")]
    public string Name { get; set; }

    [Required(ErrorMessage = "メールアドレスを入力してください。")]
    [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください。")]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "有効なメールアドレスを入力してください。")]

    public string EmailAdress { get; set; }

    [Required(ErrorMessage = "メッセージを入力してください。")]
    public string Message { get; set; }

}
