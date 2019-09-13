using System.Collections.Generic;

namespace YoutubeService.Model
{
    public class YoutubeUserVideos
    {
        public string PlaylistName { get; set; }
        public Dictionary<string, string> VideoIDsDictionary { get; set; } = new Dictionary<string, string>();
    }
}