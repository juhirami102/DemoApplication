using DemoApplication.ApiModels.Album;
using DemoApplication.ApiModels.Configuration;
using DemoApplication.BusinessServices.Interfaces.Album;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace DemoApplication.Areas.Album
{
    [Area("Album")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AlbumController : ControllerBase
    {
        private readonly IUserAlbumsBusinessService _userAlbumsBusinessService;

        public AlbumController(IUserAlbumsBusinessService userAlbumsBusinessService)
        {
            _userAlbumsBusinessService = userAlbumsBusinessService;
        }
        
        /// <summary>
        /// End point for List of All Albums-Photos 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllAlbumDetails")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiError),(int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<UserAlbumResultApiModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AlbumDetailsAsync()
        {
            var albumDetails = await _userAlbumsBusinessService.GetAlbumsAsync();

            return Ok(albumDetails);
        }

        /// <summary>
        /// End point for List of All Albums-Photos By User
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAlbumDetailsByUser")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<UserAlbumResultApiModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAlbumDetailsByUserAsync([Required]int userId)
        {
            var userAlbumData = await _userAlbumsBusinessService.GetAlbumDetailsByUserAsync(userId);

            return Ok(userAlbumData);
        }


    }
}
