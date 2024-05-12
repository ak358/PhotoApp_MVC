using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp_MVC.Models
{
    public class PhotoPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [ValidateNever]
        [ForeignKey("UserId")]

        public User User { get; set; }

        public int CategoryId { get; internal set; }
        public int UserId { get; internal set; }
    }
}
