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
            foreach (var project in projectManager.ProjectDictionary.Keys)
            {
                Console.WriteLine($"Project: {project.Name} | Status: {project.Status}");
                Console.WriteLine($"Description: {project.Description}");
                Console.WriteLine($"Start Date: {project.StartDate.ToShortDateString()} | End Date: {project.EndDate.ToShortDateString()}");
                Console.WriteLine("Tasks:");
                foreach (var task in projectManager.ProjectDictionary[project])
                {
                    Console.WriteLine($"  - {task.Name} | Status: {task.Status} | Priority: {task.Priority}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();

        }

        static void AddNewProject()
        {
            Console.Clear();
            string name = Utility.GetValidInput("Enter the project name:", input =>
            {
                bool isValid = !string.IsNullOrWhiteSpace(input) &&
                               !projectManager.ProjectDictionary.Keys.Any(p => p.Name.Equals(input, StringComparison.OrdinalIgnoreCase));
                return (isValid, input);
            });

            string description = Utility.GetValidInput("Enter the project description:", input =>
                (!string.IsNullOrWhiteSpace(input), input));

            DateTime startDate = Utility.GetValidInput("Enter start date (yyyy-MM-dd):", input =>
            {
                return DateTime.TryParse(input, out DateTime date) ? (true, date) : (false, default);
            });

            DateTime endDate = Utility.GetValidInput("Enter end date (yyyy-MM-dd):", input =>
            {
                bool isValid = DateTime.TryParse(input, out DateTime date) && date >= startDate;
                return (isValid, date);
            });

            var newProject = new Project
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                Status = ProjectStatus.Active
            };

            projectManager.AddProject(newProject);
            Console.WriteLine("Project added successfully! Press any key to return to the main menu.");
            Console.ReadKey();


        }

        static void DeleteProject()
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the project to delete:");
            string name = Console.ReadLine();

            var project = projectManager.ProjectDictionary.Keys.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (project == null)
            {
                Console.WriteLine("Project not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Are you sure you want to delete the project '{project.Name}'? (yes/no)");
            if (Console.ReadLine()?.ToLower() == "yes")
            {
                projectManager.DeleteProject(project);
                Console.WriteLine("Project deleted successfully. Press any key to return.");
            }
            else
            {
                Console.WriteLine("Deletion canceled. Press any key to return.");
            }

            Console.ReadKey();


        }

        static void ViewTasksDueIn7Days()
        {
            Console.Clear();
            var upcomingTasks = projectManager.GetTasksDueIn7Days();

            if (!upcomingTasks.Any())
            {
                Console.WriteLine("No tasks due in the next 7 days.");
            }
            else
            {
                Console.WriteLine("Tasks due in the next 7 days:");
                foreach (var task in upcomingTasks)
                {
                    Console.WriteLine($"- {task.Name} | Deadline: {task.Deadline.ToShortDateString()} | Priority: {task.Priority}");
                }
            }

            Console.WriteLine("Press any key to return.");
            Console.ReadKey();


        }

        static void FilterProjectsByStatus()
        {
            Console.Clear();
            Console.WriteLine("Select status to filter by:");
            Console.WriteLine("1. Active");
            Console.WriteLine("2. Pending");
            Console.WriteLine("3. Completed");

            if (int.TryParse(Console.ReadLine(), out int statusChoice) && Enum.IsDefined(typeof(ProjectStatus), statusChoice - 1))
            {
                var status = (ProjectStatus)(statusChoice - 1);
                var filteredProjects = projectManager.FilterProjectsByStatus(status);

                if (!filteredProjects.Any())
                {
                    Console.WriteLine($"No projects with status: {status}");
                }
                else
                {
                    Console.WriteLine($"Projects with status: {status}");
                    foreach (var project in filteredProjects)
                    {
                        Console.WriteLine($"- {project.Name}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

            Console.WriteLine("Press any key to return.");
            Console.ReadKey();


        }

        static void ManageProject()
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the project to manage:");
            string projectName = Console.ReadLine();

            var project = projectManager.ProjectDictionary.Keys.FirstOrDefault(p => p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase));
            if (project == null)
            {
                Console.WriteLine("Project not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Managing Project: {project.Name}");
                Console.WriteLine("1. View All Tasks");
                Console.WriteLine("2. View Project Details");
                Console.WriteLine("3. Edit Project Status");
                Console.WriteLine("4. Add Task");
                Console.WriteLine("5. Delete Task");
                Console.WriteLine("6. View Total Expected Time for Active Tasks");
                Console.WriteLine("7. View Tasks Sorted by Duration");
                Console.WriteLine("0. Return to Main Menu");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewAllTasks(project); break;
                    case "2": ViewProjectDetails(project); break;
                    case "3": EditProjectStatus(project); break;
                    case "4": AddTask(project); break;
                    case "5": DeleteTask(project); break;
                    case "6": ViewTotalExpectedTime(project); break;
                    case "7": SortTasksByDuration(project); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option. Try again!"); break;
                }
            }


        }

        static void ViewAllTasks(Project project)
        {
            Console.Clear();
            if (!projectManager.ProjectDictionary[project].Any())
            {
                Console.WriteLine("No tasks in this project.");
            }
            else
            {
                Console.WriteLine($"Tasks in Project: {project.Name}");
                foreach (var task in projectManager.ProjectDictionary[project])
                {
                    Console.WriteLine($"- {task.Name} | Status: {task.Status} | Deadline: {task.Deadline.ToShortDateString()} | Priority: {task.Priority}");
                }
            }
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();


        }

        static void ViewProjectDetails(Project project)
        {
            Console.Clear();
            Console.WriteLine($"Project Details: {project.Name}");
            Console.WriteLine($"Description: {project.Description}");
            Console.WriteLine($"Start Date: {project.StartDate.ToShortDateString()}");
            Console.WriteLine($"End Date: {project.EndDate.ToShortDateString()}");
            Console.WriteLine($"Status: {project.Status}");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();


        }

        static void EditProjectStatus(Project project)
        {
            if (project.Status == ProjectStatus.Completed)
            {
                Console.WriteLine("Cannot edit status of a completed project. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("Select new status:");
            Console.WriteLine("1. Active");
            Console.WriteLine("2. Pending");
            Console.WriteLine("3. Completed");

            if (int.TryParse(Console.ReadLine(), out int statusChoice) && Enum.IsDefined(typeof(ProjectStatus), statusChoice - 1))
            {
                project.Status = (ProjectStatus)(statusChoice - 1);
                Console.WriteLine("Project status updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();


        }

        static void AddTask(Project project)
        {
            if (project.Status == ProjectStatus.Completed)
            {
                Console.WriteLine("Cannot add tasks to a completed project. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            string name = Utility.GetValidInput("Enter the task name:", input =>
            {
                bool isValid = !string.IsNullOrWhiteSpace(input) &&
                               !projectManager.ProjectDictionary[project].Any(t => t.Name.Equals(input, StringComparison.OrdinalIgnoreCase));
                return (isValid, input);
            });

            string description = Utility.GetValidInput("Enter the task description:", input =>
                (!string.IsNullOrWhiteSpace(input), input));

            DateTime deadline = Utility.GetValidInput("Enter deadline (yyyy-MM-dd):", input =>
            {
                return DateTime.TryParse(input, out DateTime date) ? (true, date) : (false, default);
            });

            int expectedDuration = Utility.GetValidInput("Enter expected duration in minutes:", input =>
            {
                return int.TryParse(input, out int duration) && duration > 0 ? (true, duration) : (false, default);
            });

            TaskPriority priority = Utility.GetValidInput("Select priority (1. High, 2. Medium, 3. Low):", input =>
            {
                bool isValid = int.TryParse(input, out int choice) && Enum.IsDefined(typeof(TaskPriority), choice - 1);
                return isValid ? (true, (TaskPriority)(choice - 1)) : (false, default);
            });

            var newTask = new Task
            {
                Name = name,
                Description = description,
                Deadline = deadline,
                Status = TaskStatus.Active,
                ExpectedDuration = expectedDuration,
                Priority = priority
            };

            projectManager.AddTask(project, newTask);
            Console.WriteLine("Task added successfully! Press any key to return.");
            Console.ReadKey();


        }

        static void DeleteTask(Project project)
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the task to delete:");
            string taskName = Console.ReadLine();

            var task = projectManager.ProjectDictionary[project].FirstOrDefault(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));
            if (task == null)
            {
                Console.WriteLine("Task not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Are you sure you want to delete the task '{task.Name}'? (yes/no)");
            if (Console.ReadLine()?.ToLower() == "yes")
            {
                projectManager.DeleteTask(project, task);
                Console.WriteLine("Task deleted successfully. Press any key to return.");
            }
            else
            {
                Console.WriteLine("Deletion canceled. Press any key to return.");
            }
            Console.ReadKey();


        }

        static void ViewTotalExpectedTime(Project project)
        {
           
        }
        static void SortTasksByDuration(Project project)
        {
            Console.Clear();
           
        }







    }
}
