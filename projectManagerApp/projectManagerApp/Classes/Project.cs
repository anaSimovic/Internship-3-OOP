using System;

namespace ProjectManagerApp
{
    public enum ProjectStatus { Active, Pending, Completed }

    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
