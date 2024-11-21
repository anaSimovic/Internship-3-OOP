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
            MainMenu();


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
        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Main Menu ===");
                Console.WriteLine("1. View All Projects");
                Console.WriteLine("2. Add New Project");
                Console.WriteLine("3. Delete Project");
                Console.WriteLine("4. View Tasks Due in 7 Days");
                Console.WriteLine("5. Filter Projects by Status");
                Console.WriteLine("6. Manage Project");
                Console.WriteLine("0. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewAllProjects(); break;
                    case "2": AddNewProject(); break;
                    case "3": DeleteProject(); break;
                    case "4": ViewTasksDueIn7Days(); break;
                    case "5": FilterProjectsByStatus(); break;
                    case "6": ManageProject(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option. Try again!"); break;
                }
            }
        }
        static void ViewAllProjects()
        {
            Console.Clear();
            
        }

        static void AddNewProject()
        {
            Console.Clear();
            
        }

        static void DeleteProject()
        {
            Console.Clear();
           
        }

        static void ViewTasksDueIn7Days()
        {
            
        }

        static void FilterProjectsByStatus()
        {
            Console.Clear();
           
        }

        static void ManageProject()
        {
            
        }

       





    }
}
