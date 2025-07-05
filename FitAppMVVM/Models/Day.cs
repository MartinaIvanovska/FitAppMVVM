using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAppMVVM.Models
{
    public class Day
    {
        public DateTime Date { get; set; }
        public List<Workout> Workouts { get; set; } = new List<Workout>();

        public string Summary => string.Join(",", Workouts.Select(e => e.Name));
        public bool HasWorkouts => Workouts.Any();
    }
}

