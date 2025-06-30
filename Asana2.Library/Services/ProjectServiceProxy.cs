using Asana2.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana2.Library.Services
{
    public class ProjectServiceProxy
    {
        // Internal list to store Project objects
        public List<Project> Projects;

        // Private constructor to enforce Singleton pattern
        private ProjectServiceProxy()
        {
            Projects = new List<Project>();
        }

        // Static instance of the service proxy (Singleton)
        private static ProjectServiceProxy? instance;

        // Property to get the next available ID for a new project
        private int nextKey
        {
            get
            {
                if (Projects.Any())
                {
                    // If projects exist, return the max ID + 1
                    return Projects.Select(p => p.Id).Max() + 1;
                }
                // Otherwise, start with 1
                return 1;
            }
        }

        // Public static property to access the singleton instance
        public static ProjectServiceProxy Current
        {
            get
            {
                // Create instance if it doesn't exist
                if (instance == null)
                {
                    instance = new ProjectServiceProxy();
                }
                return instance;
            }
        }

        // Adds a new project or updates an existing one
        public void AddOrUpdate(Project? project)
        {
            if (project != null)
            {
                // If it's a new project (Id == 0), assign a new ID and add it
                if (project.Id == 0)
                {
                    project.Id = nextKey;
                    Projects.Add(project);
                }
                else
                {
                    // If it's an existing project, remove the old one and add the updated one
                    // This is a simple update mechanism for in-memory lists
                    var existingProject = GetById(project.Id);
                    if (existingProject != null)
                    {
                        Projects.Remove(existingProject);
                        Projects.Add(project);
                    }
                    else
                    {
                        // If the project with the given ID doesn't exist, add it as a new one
                        project.Id = nextKey; // Assign a new ID to avoid conflicts
                        Projects.Add(project);
                    }
                }
            }
        }

        // Displays all projects to the console
        public void DisplayProjects()
        {
            if (!Projects.Any())
            {
                Console.WriteLine("No projects to display.");
                return;
            }
            Console.WriteLine("\n--- Projects ---");
            Projects.ForEach(Console.WriteLine);
            Console.WriteLine("----------------\n");
        }
        
        // Deletes a project from the list
        public void DeleteProject(Project? project)
        {
            if (project == null)
            {
                return;
            }
            Projects.Remove(project);
        }

        // Retrieves a project by its ID
        public Project? GetById(int id)
        {
            return Projects.FirstOrDefault(p => p.Id == id);
        }
    }
}