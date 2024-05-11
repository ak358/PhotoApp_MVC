using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp_MVC.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ValidateNever]
        [ForeignKey("UserId")]

        public User User { get; set; }
        [ValidateNever]
        public List<PhotoPost> PhotoPosts { get; set; }
        public int UserId { get; internal set; }
    }
}
