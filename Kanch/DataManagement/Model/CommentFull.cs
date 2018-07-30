﻿using Kanch.DataManagement.Model.Users;
using Kanch.DataManagement.Model.UsersDAL;

namespace Kanch.DataManagement.Model
{
    public class CommentFull
    {
        private UsersDal usersDal;
        public CommentFull(Comment comment)
        {
            this.usersDal = new UsersDal();
            this.Id = comment.Id;
            this.Text = comment.Text;
            this.User = usersDal.GetUser(comment.UserId);
            this.CampingTripID = comment.CampingTripId;
        }

        public string Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public string CampingTripID { get; set; }
    }
}