#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using ShineMvc.Models;
using ShineMvc.Models.Users;

#endregion

namespace ShineMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly string cookieId = "Shine";

        private readonly string openDateCookieId = "openDate";

        public ActionResult Index()
        {
            const string Version = "v1";
            var videoList = this.GetVideoSettings(Version);
            var firstVideo = videoList.FirstOrDefault(v => v.Order == 1);
            if (firstVideo == null)
            {
                throw new Exception("Video not found");
            }

            var videoId = firstVideo.Id;
            return this.RedirectToRoute("VideoRoute", new { version = Version, videoId });
        }

        public ActionResult Video(string version, string videoId)
        {
            this.ViewData["version"] = version;
            var viewModel = this.GetVideoViewModel(videoId);
            return this.View(viewModel);
        }

        private User CreateNewUser()
        {
            var user = new User { FirstOpenDate = DateTime.Now };

            var cookie = new HttpCookie(this.cookieId)
                             {
                                 [this.openDateCookieId] =
                                 user.FirstOpenDate.Ticks.ToString(),
                                 Expires = DateTime.Now.AddYears(1)
                             };

            this.Response.Cookies.Add(cookie);
            return user;
        }

        private User GetCurrentUserInfo()
        {
            User user = null;
            var cookieReq = this.Request.Cookies[this.cookieId];
            var openDate = cookieReq?[this.openDateCookieId];
            if (!string.IsNullOrWhiteSpace(openDate))
            {
                user = new User { FirstOpenDate = new DateTime(Convert.ToInt64(openDate)) };
            }

            return user;
        }

        private User GetOrCreateUser()
        {
            var user = this.GetCurrentUserInfo() ?? this.CreateNewUser();
            return user;
        }

        private List<VideoPlayListItem> GetPlayListViewModel(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                version = "v1";
            }
            var videoSettingsViewModels = new List<VideoPlayListItem>();
            var user = this.GetOrCreateUser();
            var videSettingsFromFile = this.GetVideoSettings(version);
            foreach (var videItem in videSettingsFromFile)
            {
                var newVideoViewModel =
                    new VideoPlayListItem
                        {
                            Id = videItem.Id,
                            LockedThumbnailUrl = videItem.LockedThumbnailUrl,
                            UnLockedThumbnailUrl = videItem.UnLockedThumbnailUrl,
                            Title = videItem.Title,
                            IsUnlocked = videItem.IsUnlockedForDate(user.FirstOpenDate),
                            ShortDescription = videItem.ShortDescription,
                            Version = version
                        };
               

                videoSettingsViewModels.Add(newVideoViewModel);
            }

            return videoSettingsViewModels;
        }

        private List<VideoSettings> GetVideoSettings(string version)
        {
            var file = $"~/App_Data/shine-config.{version}.json";
            var filePath = this.Server.MapPath(file);
            var json = System.IO.File.ReadAllText(filePath);
            var serializer = new JavaScriptSerializer();
            var videoSettings = serializer.Deserialize<List<VideoSettings>>(json);
            return videoSettings;
        }

        private VideoViewModel GetVideoViewModel(string id)
        {
            var version = this.ViewData["version"] as string;
            if (string.IsNullOrWhiteSpace(version))
            {
                version = "v1";
            }

            var user = this.GetOrCreateUser();
            var videoList = this.GetVideoSettings(version);
            var selectedVideo = string.IsNullOrWhiteSpace(id)
                                    ? videoList.FirstOrDefault(v => v.Order == 1)
                                    : videoList.FirstOrDefault(v => string.Equals(v.Id, id));
            
            if (selectedVideo == null)
            {
                throw new Exception("Video not found");
            }
            var videoViewModel = new VideoViewModel
                                     {
                                         CurrentUser = user,
                                         VimeoId = selectedVideo.VimeoId,
                                         VideoDescription = selectedVideo.Description,
                                         VideoTitle = selectedVideo.Title,
                                         ShowBookingBlockAfter = selectedVideo.ShowBookingBlockAfter
                                     };
            var playlist = this.GetPlayListViewModel(version);

            videoViewModel.VideoPlayList = playlist;
            return videoViewModel;
        }
    }
}