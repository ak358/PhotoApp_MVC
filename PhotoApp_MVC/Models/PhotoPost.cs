using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp_MVC.Models
{
    public class PhotoPost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public User User { get; set; }
    }
}
