using System;
using System.Linq;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using YoutubeService.Model;

namespace YoutubeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserVideosController : ControllerBase
    {
        [HttpGet("{Username}")]
        public ActionResult<YoutubeUserVideos> Get(string Username)
        {
            var result = new YoutubeUserVideos
            {
                
            };
            var youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = "YoutubeListSyncronizer",
                ApiKey = "AIzaSyDgUR4esr5twkPl5jRwGlx6yPGR8e6zBPs"
            });

            //fetch playlist name
            {
                var request = youtubeService.Channels.List("contentDetails");
                request.ForUsername = Username;
                var response = request.Execute();

                result.PlaylistName = response.Items.ElementAtOrDefault(0)?.ContentDetails?.RelatedPlaylists?.Uploads;
                if (result.PlaylistName == null)
                    throw new Exception(Username + " kullanıcısının yüklenen videoları bulunamadı.");
            }

            var nextPageToken = "";
            while (nextPageToken != null)
            {
                var request = youtubeService.PlaylistItems.List("snippet");
                request.PlaylistId = result.PlaylistName;
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