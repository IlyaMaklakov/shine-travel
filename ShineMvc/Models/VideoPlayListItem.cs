namespace ShineMvc.Models
{
    public class VideoPlayListItem
    {
        public string Id { get; set; }

        public string ShortDescription { get; set; }

        public string Title { get; set; }

        public bool IsUnlocked { get; set; }

        public string Version { get; set; }

        public string UnlockAfter { get; set; }

        public string LockedThumbnailUrl { get; set; }

        public string UnLockedThumbnailUrl { get; set; }
    }
}