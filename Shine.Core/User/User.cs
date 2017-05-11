#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace Shine.Core.User
{
    public class User
    {
        public User()
        {
            this.LastUnlockedVideosInfo = new Dictionary<string, string>();
            this.FirstOpenDate = DateTime.Now;
        }

        public DateTime FirstOpenDate { get; set; }

        public int Id { get; set; }

        public Dictionary<string, string> LastUnlockedVideosInfo { get; set; }
    }
}