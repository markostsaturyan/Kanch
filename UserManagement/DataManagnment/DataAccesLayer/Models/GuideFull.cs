﻿using System.Collections.Generic;

namespace UserManagement.DataManagnment.DataAccesLayer.Models
{
    public class GuideFull:UserInfo
    {
        public string Profession { get; set; }

        public string EducationGrade { get; set; }

        public string KnowledgeOfLanguages { get; set; }

        public string WorkExperience { get; set; }

        public List<string> Places { get; set; }

        public string Password { get; set; }
    }
}
