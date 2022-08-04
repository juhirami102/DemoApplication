using DemoApplication.DemoApplication.ApiModels.Album;
using System.Collections.Generic;

namespace DemoApplication.ApiModels.Album
{
    public class UserAlbumResultApiModel
    {
        public int UserId { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<PhotoResultApiModel> photos { get; set; }
    }
}
