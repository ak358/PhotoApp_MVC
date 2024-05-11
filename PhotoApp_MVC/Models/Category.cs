using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoApp_MVC.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int Id { get; set; }
        public string Name { get; set; }
        [ValidateNever]
        public User User { get; set; }
        [ValidateNever]
        public List<PhotoPost> PhotoPosts { get; set; }
    }
}
