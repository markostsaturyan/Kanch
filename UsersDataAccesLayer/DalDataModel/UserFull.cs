﻿using System;
using Veldrid.ImageSharp;

namespace UsersDataAccesLayer
{
    public class UserFull
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DataOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public ImageSharpTexture Image { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; }
    }
}