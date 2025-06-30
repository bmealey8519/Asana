using Asana2.Library.Models;
using Asana2.Library.Services;
using System;

namespace Asana
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var toDoSvc = ToDoServiceProxy.Current;
            var projectSvc = ProjectServiceProxy.Current;
            int choiceInt;
            do
            {
                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("1. Create a ToDo");
                Console.WriteLine("2. List all ToDos");
                Console.WriteLine("3. List all outstanding ToDos");
                Console.WriteLine("4. Delete a ToDo");
                Console.WriteLine("5. Update a ToDo");
                Console.WriteLine("--- Project Operations ---");
                Console.WriteLine("6. Create a Project");
                Console.WriteLine("7. Delete a Project");
                Console.WriteLine("8. Update a Project");
                Console.WriteLine("9. List all Projects");
                Console.WriteLine("10. List all ToDos in a Given Project");
                Console.WriteLine("11. Exit");

                var choice = Console.ReadLine() ?? "11";

                if (int.TryParse(choice, out choiceInt))
                {
                    switch (choiceInt)
                    {
                        case 1:
                            Console.Write("Name:");
                            var name = Console.ReadLine();
                            Console.Write("Description:");
                            var description = Console.ReadLine();

                            toDoSvc.AddOrUpdate(new ToDo
                            {
                                Name = name,
                                Description = description,
                                IsCompleted = false,
                                Id = 0
                            });
                            break;
                        case 2:
                            toDoSvc.DisplayToDos(true);
                            break;
                        case 3:
                            toDoSvc.DisplayToDos();
                            break;
                        case 4:
                            toDoSvc.DisplayToDos(true);
                            Console.Write("ToDo to Delete: ");
                            var toDoChoice4 = int.Parse(Console.ReadLine() ?? "0");

                            var reference = toDoSvc.GetById(toDoChoice4);
                            toDoSvc.DeleteToDo(reference);
                            break;
                        case 5:
                            toDoSvc.DisplayToDos(true);
                            Console.Write("ToDo to Update: ");
                            var toDoChoice5 = int.Parse(Console.ReadLine() ?? "0");
                            var updateReference = toDoSvc.GetById(toDoChoice5);

                            if (updateReference != null)
                            {
                                Console.Write("Name:");
                                updateReference.Name = Console.ReadLine();
                                Console.Write("Description:");
                                updateReference.Description = Console.ReadLine();
                            }
                            toDoSvc.AddOrUpdate(updateReference);
                            break;
                        case 6: // Create a Project
                            Console.Write("Project Name: ");
                            var projectName = Console.ReadLine();
                            Console.Write("Project Description: ");
                            var projectDescription = Console.ReadLine();
                            projectSvc.AddOrUpdate(new Project
                            {
                                Name = projectName,
                                Description = projectDescription,
                                Id = 0 // Assign 0 for new project, service proxy will assign unique ID
                            });
                            Console.WriteLine("Project created successfully!");
                            break;
                        case 7: // Delete a Project
                            projectSvc.DisplayProjects();
                            Console.Write("Enter Project ID to Delete: ");
                            if (int.TryParse(Console.ReadLine(), out int projectIdToDelete))
                            {
                                var projectToDelete = projectSvc.GetById(projectIdToDelete);
                                if (projectToDelete != null)
                                {
                                    projectSvc.DeleteProject(projectToDelete);
                                    Console.WriteLine($"Project [{projectIdToDelete}] deleted.");
                                }
                                else
                                {
                                    Console.WriteLine("Project not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Project ID.");
                            }
                            break;
                        case 8: // Update a Project
                            projectSvc.DisplayProjects();
                            Console.Write("Enter Project ID to Update: ");
                            if (int.TryParse(Console.ReadLine(), out int projectIdToUpdate))
                            {
                                var projectToUpdate = projectSvc.GetById(projectIdToUpdate);
                                if (projectToUpdate != null)
                                {
                                    Console.Write($"Current Name: {projectToUpdate.Name}. New Name (leave blank to keep): ");
                                    var newProjectName = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newProjectName))
                                    {
                                        projectToUpdate.Name = newProjectName;
                                    }

                                    Console.Write($"Current Description: {projectToUpdate.Description}. New Description (leave blank to keep): ");
                                    var newProjectDescription = Console.ReadLine();
                                    if (!string.IsNullOrWhiteSpace(newProjectDescription))
                                    {
                                        projectToUpdate.Description = newProjectDescription;
                                    }
                                    projectSvc.AddOrUpdate(projectToUpdate); // Update existing project
                                    Console.WriteLine($"Project [{projectIdToUpdate}] updated successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Project not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Project ID.");
                            }
                            break;
                        case 9: // List all Projects
                            projectSvc.DisplayProjects();
                            break;
                        case 10: // List all ToDos in a Given Project
                            projectSvc.DisplayProjects();
                            Console.Write("Enter Project ID to list its ToDos: ");
                            if (int.TryParse(Console.ReadLine(), out int projectIdToListToDos))
                            {
                                var targetProject = projectSvc.GetById(projectIdToListToDos);
                                if (targetProject != null)
                                {
                                    if (targetProject.ToDos.Any())
                                    {
                                        Console.WriteLine($"\n--- ToDos in Project '{targetProject.Name}' ---");
                                        targetProject.ToDos.ForEach(Console.WriteLine);
                                        Console.WriteLine("-------------------------------------------\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Project '{targetProject.Name}' has no ToDos.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Project not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Project ID.");
                            }
                            break;
                        case 11:
                            break;
                        default:
                            Console.WriteLine("ERROR: Unknown menu selection");
                            break;
                    }
                } else
                {
                    Console.WriteLine($"ERROR: {choice} is not a valid menu selection");
                }

            } while (choiceInt != 11);

        }
    }
}