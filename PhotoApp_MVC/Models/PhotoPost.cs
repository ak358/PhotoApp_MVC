using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp_MVC.Models
{
    public class PhotoPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string imageURL { get; set; }

        public int CategoryId { get; internal set; }

        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int UserId { get; internal set; }

        [ValidateNever]
        [ForeignKey("UserId")]

        public User User { get; set; }

    }
}
