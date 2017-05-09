#region Usings

using System.Collections.Generic;

using ShineMvc.Models.Users;

#endregion

namespace ShineMvc.Models
{
    public class VideoViewModel
    {
        public User CurrentUser { get; set; }

        public int? ShowBookingBlockAfter { get; set; }

        public string VideoDescription { get; set; }

        public string VideoTitle { get; set; }

        public string VimeoId { get; set; }

        public List<VideoPlayListItem> VideoPlayList { get; set; }
    }
}