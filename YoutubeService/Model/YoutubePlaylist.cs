using System.Collections.Generic;

namespace YoutubeService.Model
{
    public class YoutubePlaylist
    {
        public string PlaylistName { get; set; }
        public string ID { get; set; }
        public Dictionary<string, string> VideoIDsDictionary { get; set; } = new Dictionary<string, string>();
    }
}