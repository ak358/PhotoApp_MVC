using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp_MVC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        [ValidateNever]
        public List<Category> Categories { get; set; }
        [ValidateNever]
        public List<PhotoPost> PhotoPosts { get; set; }
    }
}
