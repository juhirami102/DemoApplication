using DemoApplication.ApiModels.Album;
using DemoApplication.ApiModels.Configuration;
using DemoApplication.BusinessServices.Interfaces.Album;
using DemoApplication.BusinessServices.Interfaces.Handler;
using DemoApplication.DemoApplication.ApiModels.Album;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApplication.BusinessService.Album
{
    public class UserAlbumsBusinessService : IUserAlbumsBusinessService
    {
        private readonly IWebApiCallerService _webApiCallerService;
        private readonly JsonplaceholderConfiguration _apiConfiguration;

        public UserAlbumsBusinessService(IWebApiCallerService webApiCallerService,
            IOptionsMonitor<JsonplaceholderConfiguration> apiConfiguration)
        {
            _webApiCallerService = webApiCallerService;
            _apiConfiguration = apiConfiguration.CurrentValue;
        }

        #region GetAlbumsAsync
        public async Task<IEnumerable<UserAlbumResultApiModel>> GetAlbumsAsync()
        {
            var userAlbumList = new List<UserAlbumResultApiModel>();

            var albumsCollection = await _webApiCallerService.GetDataAsync<List<AlbumResultApiModel>>($"{_apiConfiguration.ApiPrefix}{_apiConfiguration.AlbumsUri}");

            var photosCollection = await _webApiCallerService.GetDataAsync<List<PhotoResultApiModel>>($"{_apiConfiguration.ApiPrefix}{_apiConfiguration.PhotosUri}");

            if (albumsCollection != null && photosCollection != null)
            {

                var userAlbumDetails = (from album in albumsCollection.Take(200)
                                        join photos in photosCollection.Take(200) on album.Id equals photos.albumId
                                        into AlbumPhotoGroup
                                        from photo in AlbumPhotoGroup.DefaultIfEmpty()
                                        select new { album, photo });

                foreach (var useralbum in userAlbumDetails)
                {
                    var photoList = photosCollection
                                    .Where(p => p.albumId == useralbum.album.Id)
                                    .Select(model => new PhotoResultApiModel
                                    {
                                        albumId = model.albumId,
                                        Id = model.Id,
                                        Title = model.Title,
                                        ThumbnailUrl = model.ThumbnailUrl,
                                        Url = model.Url
                                    }).Take(10);

                    var userAlbumModel = new UserAlbumResultApiModel
                    {
                        UserId = useralbum.album.UserId,
                        Id = useralbum.album.Id,
                        Title = useralbum.album.Title,
                        photos = photoList != null ? photoList : null
                    };

                    if (!userAlbumList.Any(u => u.UserId == useralbum.album.UserId))
                    {
                        userAlbumList.Add(userAlbumModel);
                    }
                }
            }
            return userAlbumList;
        }

        #endregion


        #region GetAlbumDetailsByUserAsync
        public async Task<UserAlbumResultApiModel> GetAlbumDetailsByUserAsync(int userId)
        {
            var albumsCollection = await _webApiCallerService.GetDataAsync<List<AlbumResultApiModel>>($"{_apiConfiguration.ApiPrefix}{_apiConfiguration.AlbumsUri}");

            var photosCollection = await _webApiCallerService.GetDataAsync<List<PhotoResultApiModel>>($"{_apiConfiguration.ApiPrefix}{_apiConfiguration.PhotosUri}");

            if (albumsCollection != null && photosCollection != null)
            {
                var userAlbumDetails = (from album in albumsCollection.Take(100)
                                        join photos in photosCollection.Take(100) on album.Id equals photos.albumId
                                        into AlbumPhotoGroup
                                        from photo in AlbumPhotoGroup.DefaultIfEmpty()
                                        where album.UserId.Equals(userId)
                                        select new { album, photo }).FirstOrDefault();

                var photoList = photosCollection
                            .Where(p => p.albumId == userAlbumDetails?.album.Id)
                            .Select(s => new PhotoResultApiModel
                            {
                                albumId = s.albumId,
                                Id = s.Id,
                                Title = s.Title,
                                ThumbnailUrl = s.ThumbnailUrl,
                                Url = s.Url
                            }).ToList();

                return userAlbumDetails != null ? new UserAlbumResultApiModel
                {
                    UserId = userAlbumDetails.album.UserId,
                    Id = userAlbumDetails.album.Id,
                    photos = photoList != null ? photoList : null,
                    Title = userAlbumDetails.album.Title

                } : null;
            }

            return new UserAlbumResultApiModel();
        }

        #endregion end GetAlbumDetailsByUserAsync

    }
}
