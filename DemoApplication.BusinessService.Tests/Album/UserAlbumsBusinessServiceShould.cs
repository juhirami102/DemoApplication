using DemoApplication.ApiModels.Configuration;
using DemoApplication.BusinessService.Album;
using DemoApplication.BusinessServices.Interfaces.Handler;
using DemoApplication.DemoApplication.ApiModels.Album;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DemoApplication.BusinessService.Tests.Album
{
    public class UserAlbumsBusinessServiceShould
    {
       
        private readonly UserAlbumsBusinessService _userAlbumsBusinessService;
        private readonly Mock<IWebApiCallerService> _webApiCallerServiceMock;
        private readonly Mock<IOptionsMonitor<JsonplaceholderConfiguration>> _apiConfigurationMock;


        public UserAlbumsBusinessServiceShould()
        {
            var apiConfiguration = GetApiUrlConfiguration();

            _webApiCallerServiceMock = new Mock<IWebApiCallerService>();

            _apiConfigurationMock = new Mock<IOptionsMonitor<JsonplaceholderConfiguration>>();

            _apiConfigurationMock.Setup(ap => ap.CurrentValue).Returns(apiConfiguration);

            _userAlbumsBusinessService = new UserAlbumsBusinessService(_webApiCallerServiceMock.Object,
                _apiConfigurationMock.Object);
        }

        [Fact]
        public async Task GetAlbumsAsync_WHEN_Album_IS_NOT_NULL_Verify1_GetAlbumAsync_GetPhotosAsync()
        {
            //Arrange 
             string albumUrl = $"{_apiConfigurationMock.Object.CurrentValue.ApiPrefix}{_apiConfigurationMock.Object.CurrentValue.AlbumsUri}";
             string photoUrl = $"{_apiConfigurationMock.Object.CurrentValue.ApiPrefix}{_apiConfigurationMock.Object.CurrentValue.PhotosUri}";

            //Act
            await _userAlbumsBusinessService.GetAlbumsAsync();

            //Assert
            _webApiCallerServiceMock.Verify(x => x.GetDataAsync<List<AlbumResultApiModel>>(albumUrl), Times.Once);
            _webApiCallerServiceMock.Verify(x => x.GetDataAsync<List<PhotoResultApiModel>>(photoUrl), Times.Once);
        }

        [Fact]
        public async Task GetAlbumDetailsByUserAsync_WHEN_UserId_IS_Valid_Verify1_GetAlbumAsync_GetPhotosAsync()
        {
            //Arrange 
            string albumUrl = $"{_apiConfigurationMock.Object.CurrentValue.ApiPrefix}{_apiConfigurationMock.Object.CurrentValue.AlbumsUri}";
            string photoUrl = $"{_apiConfigurationMock.Object.CurrentValue.ApiPrefix}{_apiConfigurationMock.Object.CurrentValue.PhotosUri}";
            var userId = 5;

            //Act
            await _userAlbumsBusinessService.GetAlbumDetailsByUserAsync(userId);

            //Assert
            _webApiCallerServiceMock.Verify(x => x.GetDataAsync<List<AlbumResultApiModel>>(albumUrl), Times.Once);
            _webApiCallerServiceMock.Verify(x => x.GetDataAsync<List<PhotoResultApiModel>>(photoUrl), Times.Once);
        }

        private JsonplaceholderConfiguration GetApiUrlConfiguration()
            => new JsonplaceholderConfiguration() 
            { 
                 ApiPrefix = "https://jsonplaceholder.typicode.com/",
                 AlbumsUri = "albums",
                 PhotosUri = "photos"
            };
    }
}
