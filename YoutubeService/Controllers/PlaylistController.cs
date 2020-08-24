using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YoutubeService.Model;

namespace YoutubeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PlaylistController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{playlistid}")]
        public ActionResult<YoutubePlaylist> Get(string playlistid)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = _configuration["AppName"],
                ApiKey = _configuration["ApiKey"]
            });
            var result = new YoutubePlaylist
            {
                ID = playlistid
            };
            //fetch playlist name
            {
                var request = youtubeService.Playlists.List("snippet");
                request.Id = playlistid;
                var response = request.Execute();
                if (response.Items == null || response.Items.Count == 0)
                    return new YoutubePlaylist();
                result.PlaylistName = response.Items[0].Snippet.Title;
            }

            var nextPageToken = "";
            while (nextPageToken != null)
            {
                var request = youtubeService.PlaylistItems.List("snippet");
                request.PlaylistId = playlistid;
                request.MaxResults = 20;
                request.PageToken = nextPageToken;

                var response = request.Execute();

                foreach (var playlistItem in response.Items)
                {
                    var videoId = playlistItem.Snippet.ResourceId.VideoId;
                    if (result.VideoIDsDictionary.ContainsKey(videoId))
                        continue;
                    var title = playlistItem.Snippet.Title;
                    result.VideoIDsDictionary.Add(videoId, title);
                }

                nextPageToken = response.NextPageToken;
            }

            return result;
        }
    }
}