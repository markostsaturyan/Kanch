﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.DataManagnment.DataAccesLayer.Models
{
    public class PhotographerFull:UserFull
    {
        public Camera Camera { get; set; }

        public string KnowledgeOfLanguages { get; set; }

        public string Profession { get; set; }

        public string WorkExperience { get; set; }

        public double Raiting { get; set; }

        public bool HasDron { get; set; }

        public bool HasCameraStabilizator { get; set; }

        public bool HasGopro { get; set; }
    }
}