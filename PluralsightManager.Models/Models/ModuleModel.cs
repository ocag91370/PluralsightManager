﻿using System;

namespace PluralsightManager.Models.Models
{
    public class ModuleModel
    {
        public double Id { get; set; }

        public double Index { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }

        public string CourseName { get; set; }
    }
}