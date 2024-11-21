using System;

namespace ProjectManagerApp
{
    public enum TaskStatus { Active, Completed, Postponed }
    public enum TaskPriority { High, Medium, Low }

    public class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public TaskStatus Status { get; set; }
        public int ExpectedDuration { get; set; } 
        public TaskPriority Priority { get; set; }
    }
}
