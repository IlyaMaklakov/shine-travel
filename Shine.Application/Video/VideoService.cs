using System.Collections.Generic;
using System.Web.Script.Serialization;

using Shine.Core.Video;
using Shine.Framework.Json;

namespace Shine.Application.Video
{
    public class VideoService : IVideoService
    {
        public List<VideoSettings> GetVideoSettingsByVersion(string filePath)
        {
            var json = System.IO.File.ReadAllText(filePath);
            var serializer = new JavaScriptSerializer();
            var videoSettings = serializer.Deserialize<List<VideoSettings>>(json);
            return videoSettings;
        }
    }
}