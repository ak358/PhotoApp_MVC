using PhotoApp_MVC.Models;

namespace PhotoApp_MVC.Repositories.IRepositories
{
    public interface IPhotoPostRepository
    {
        Task<List<PhotoPost>> GetPhotoPostsAsync();
        Task<PhotoPost> GetPhotoPostByIdAsync(int id);
    }
}
