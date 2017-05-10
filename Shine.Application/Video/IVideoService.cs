using System.Collections.Generic;

using Shine.Core.Video;

namespace Shine.Application.Video
{
    public interface IVideoService
    {
        List<VideoSettings> GetVideoSettingsByVersion(string version);
    }
}