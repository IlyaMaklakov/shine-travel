#region Usings

using System.Collections.Generic;

using Shine.Core.User;
using Shine.Core.Video;

#endregion

namespace ShineMvc.Models
{
    public class VideoViewModel
    {
        public User CurrentUser { get; set; }

        public int? ShowBookingBlockAfter { get; set; }

        public string VideoDescription { get; set; }

        public string VideoTitle { get; set; }

        public string VimeoUrl { get; set; }

        public bool ShowNextVideoTeaserText { get; set; }

        public List<VideoPlayListItem> VideoPlayList { get; set; }
    }
}