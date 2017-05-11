#region Usings

using System.Collections.Generic;

#endregion

namespace Shine.Core.Video
{
    public class VersionVideoSettings
    {
        public VersionVideoSettings()
        {
            this.VideoSettingsList = new List<VideoSettings>();
        }

        public string Version { get; set; }

        public List<VideoSettings> VideoSettingsList { get; set; }
    }
}