﻿using MyApp.Data;

namespace MyApp.Training.Entities
{
    public class Topic : IEntity<int>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    } 
}
