using System;
using System.Linq;

namespace ProjectManagerApp
{
    class Program
    {
        static ProjectManager projectManager = new();

        static void Main(string[] args)
        {
            SeedData();


        }
        static void SeedData()
        {
            var project1 = new Project
            {
                Name = "Project A",
                Description = "Description for Project A",
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now.AddDays(20),
                Status = ProjectStatus.Active
            };

            var project2 = new Project
            {
                Name = "Project B",
                Description = "Description for Project B",
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now.AddDays(-10),
                Status = ProjectStatus.Completed
            };

            projectManager.AddProject(project1);
            projectManager.AddProject(project2);

            var task1 = new Task
            {
                Name = "Task 1",
                Description = "Description for Task 1",
                Deadline = DateTime.Now.AddDays(5),
                Status = TaskStatus.Active,
                ExpectedDuration = 120,
                Priority = TaskPriority.High
            };

            projectManager.AddTask(project1, task1);
        }




    }
}
