using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana2.Library.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<ToDo> ToDos { get; } = new List<ToDo>();
        public double CompletePercent
        {
            get
            {
                if (ToDos.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return 100.0 * ToDos.Count(t => t.IsCompleted == true) / ToDos.Count;
                }
            }
        }   





        public override string ToString()
        {
            return $"[{Id}] {Name} - {CompletePercent:0.#}% complete ({ToDos.Count} tasks)";
        }
    }
}
