using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagerApp
{
    public class ProjectManager
    {
        public Dictionary<Project, List<Task>> ProjectDictionary { get; private set; } = new();
        public void AddProject(Project project)
        {
            if (ProjectDictionary.ContainsKey(project))
                throw new ArgumentException("A project with the same name already exists.");
            ProjectDictionary[project] = new List<Task>();
        }

        public void AddTask(Project project, Task task)
        {
            if (!ProjectDictionary.ContainsKey(project))
                throw new KeyNotFoundException("Project not found.");

            if (ProjectDictionary[project].Any(t => t.Name.Equals(task.Name, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("A task with the same name already exists in this project.");

            ProjectDictionary[project].Add(task);
        }

        public void DeleteProject(Project project)
        {
            if (!ProjectDictionary.Remove(project))
                throw new KeyNotFoundException("Project not found.");
        }

        public List<Task> GetTasksDueIn7Days()
        {
            return ProjectDictionary.Values.SelectMany(tasks => tasks)
                .Where(task => task.Deadline <= DateTime.Now.AddDays(7) && task.Deadline >= DateTime.Now)
                .ToList();
        }

        public List<Project> FilterProjectsByStatus(ProjectStatus status)
        {
            return ProjectDictionary.Keys.Where(p => p.Status == status).ToList();
        }

        public void DeleteTask(Project project, Task task)
        {
            if (!ProjectDictionary.ContainsKey(project) || !ProjectDictionary[project].Remove(task))
                throw new KeyNotFoundException("Task not found in the specified project.");
        }





    }
}
