#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Shine.Application.Video;
using Shine.Core;
using Shine.Core.Video;

using ShineMvc.Models;

#endregion

namespace ShineMvc.Controllers
{
    public class HomeController : ShineBaseController
    {
        private readonly IVideoService _videoService;


        public HomeController()
        {
            this._videoService = new VideoService();
        }
        public ActionResult Index()
        {
            var filePath = this.GetVideoSettingsFilePath(ShineConsts.DefaultVideoSettingsVersion);
            var videoList = this._videoService.GetVideoSettingsByVersion(filePath);

            var firstVideo = videoList.FirstOrDefault(v => v.Order == 1);
            if (firstVideo == null)
            {
                throw new Exception("Video not found");
            }

            return this.RedirectToRoute(
                "VideoRoute",
                new { version = ShineConsts.DefaultVideoSettingsVersion, friendlyUrl = firstVideo.FriendlyUrl });
        }

        public ActionResult Video(string version, string friendlyUrl)
        {
            var viewModel = this.GetVideoViewModel(version, friendlyUrl);
            this.UpdateUserInfoCookie();
            return this.View(viewModel);
        }

        private static int UpdateLastUnlockedVideoInfo(
            IEnumerable<VideoSettings> videoSettingsList,
            string version,
            VideoSettings selectedVideo)
        {
            if (string.IsNullOrWhiteSpace(version) || selectedVideo == null)
            {
                throw new Exception("version or selectedVideo is null");
            }

            var maxOrder = 0;

            if (CurrentUser.LastUnlockedVideosInfo.ContainsKey(version))
            {
                var lastUnlockedVideo =
                    videoSettingsList.FirstOrDefault(
                        v => string.Equals(v.Id, CurrentUser.LastUnlockedVideosInfo[version]));

                if (lastUnlockedVideo != null)
                {
                    if (lastUnlockedVideo.Order < selectedVideo.Order)
                    {
                        maxOrder = selectedVideo.Order;
                        CurrentUser.LastUnlockedVideosInfo[version] = selectedVideo.Id;
                    }
                    else
                    {
                        maxOrder = lastUnlockedVideo.Order;
                    }
                }
            }
            else
            {
                CurrentUser.LastUnlockedVideosInfo.Add(version, selectedVideo.Id);
                maxOrder = selectedVideo.Order;
            }

            return maxOrder;
        }

        private List<VideoPlayListItem> GetPlayListViewModel(string version, int maxOrder)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                version = "v1";
            }

            var filePath = this.GetVideoSettingsFilePath(version);
            var videSettingsFromFile = this._videoService.GetVideoSettingsByVersion(filePath);

            return videSettingsFromFile.Select(
                videoItem => new VideoPlayListItem
                {
                    Id = videoItem.Id,
                    LockedThumbnailUrl = videoItem.LockedThumbnailUrl,
                    UnLockedThumbnailUrl = videoItem.UnLockedThumbnailUrl,
                    Title = videoItem.Title,
                    ShortDescription = videoItem.ShortDescription,
                    Version = version,
                    FriendlyUrl = videoItem.FriendlyUrl,
                    IsUnlocked = maxOrder >= videoItem.Order
                }).ToList();
        }

        private string GetVideoSettingsFilePath(string version)
        {
            var file = $"~/App_Data/shine-config.{version}.json";
            var filePath = this.Server.MapPath(file);
            return filePath;
        }

        private VideoViewModel GetVideoViewModel(string version, string friendlyUrl)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                version = "v1";
            }

            var user = CurrentUser;

            var filePath = this.GetVideoSettingsFilePath(version);
            var videoList = this._videoService.GetVideoSettingsByVersion(filePath);

            var selectedVideo = string.IsNullOrWhiteSpace(friendlyUrl)
                                    ? videoList.FirstOrDefault(v => v.Order == 1)
                                    : videoList.FirstOrDefault(
                                        v => string.Equals(
                                            v.FriendlyUrl,
                                            friendlyUrl,
                                            StringComparison.InvariantCultureIgnoreCase));

            if (selectedVideo == null)
            {
                throw new Exception("Video not found");
            }

            var videoViewModel = new VideoViewModel
            {
                CurrentUser = user,
                VimeoUrl = selectedVideo.VimeoUrl,
                VideoDescription = selectedVideo.Description,
                VideoTitle = selectedVideo.Title,
                ShowBookingBlockAfter = selectedVideo.ShowBookingBlockAfter
            };

            var maxOrder = UpdateLastUnlockedVideoInfo(videoList, version, selectedVideo);

            if (selectedVideo.Order == maxOrder && selectedVideo.Order != videoList.Select(v => v.Order).Max())
            {
                videoViewModel.ShowNextVideoTeaserText = true;
            }
            var playlist = this.GetPlayListViewModel(version, maxOrder);

            videoViewModel.VideoPlayList = playlist;
            return videoViewModel;
        }


    }
}