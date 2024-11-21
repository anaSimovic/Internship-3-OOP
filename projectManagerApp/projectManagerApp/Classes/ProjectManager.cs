using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagerApp
{
    public class ProjectManager
    {
        public Dictionary<Project, List<Task>> ProjectDictionary { get; private set; } = new();

       
    }
}
