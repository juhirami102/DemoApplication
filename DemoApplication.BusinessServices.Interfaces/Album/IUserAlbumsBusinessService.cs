using DemoApplication.ApiModels.Album;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApplication.BusinessServices.Interfaces.Album
{
    public interface IUserAlbumsBusinessService
    {
        Task<IEnumerable<UserAlbumResultApiModel>> GetAlbumsAsync();

        Task<UserAlbumResultApiModel> GetAlbumDetailsByUserAsync(int userId);
    }
}
