using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PhotoApp_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Contact
{
    public int Id { get; set; }

    [Required(ErrorMessage = "�����O����͂��Ă��������B")]
    public string Name { get; set; }

    [Required(ErrorMessage = "���[���A�h���X����͂��Ă��������B")]
    [EmailAddress(ErrorMessage = "�L���ȃ��[���A�h���X����͂��Ă��������B")]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "�L���ȃ��[���A�h���X����͂��Ă��������B")]

    public string EmailAdress { get; set; }

    [Required(ErrorMessage = "���b�Z�[�W����͂��Ă��������B")]
    public string Message { get; set; }

}
