using PhotoApp_MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoApp_MVC.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesByUserIdAsync(int userId);

        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryByNameAsync(string name);

        //Task AddCategoryAsync(Category category);

        //Task UpdateCategoryAsync(Category category);

        //Task DeleteCategoryAsync(Category category);
    }
}
