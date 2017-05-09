using System;

namespace ShineMvc.Models
{
    public class VideoSettings
    {
        public string Description { get; set; }

        public string Id { get; set; }

        public int Order { get; set; }

        public string ShortDescription { get; set; }

        public string LockedThumbnailUrl { get; set; }
        public string UnLockedThumbnailUrl { get; set; }

        public string Title { get; set; }

        public string UnlockAfter { get; set; }

        public string VimeoId { get; set; }

        public int? ShowBookingBlockAfter { get; set; }

        public bool IsUnlockedForDate(DateTime date)
        {
            var currentDate = DateTime.Now;
            var result = false;
            if (string.IsNullOrWhiteSpace(this.UnlockAfter))
            {
                result = true;
            }
            else
            {
                var dateDeltaType = this.UnlockAfter.Substring(this.UnlockAfter.Length - 1, 1).ToLower();
                var dateDeltaValue = Convert.ToInt32(
                    this.UnlockAfter.Substring(0, this.UnlockAfter.Length - 1));
                switch (dateDeltaType)
                {
                    case "m":
                        result =
                            (currentDate - date).TotalMinutes > dateDeltaValue;
                        break;
                    case "h":
                        result =
                            (currentDate - date).TotalHours > dateDeltaValue;
                        break;
                    case "d":
                        result =
                            (currentDate - date).TotalDays > dateDeltaValue;
                        break;
                    default: break;
                }
            }

            return result;
        }
    }
}