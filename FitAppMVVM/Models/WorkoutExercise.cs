using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FitAppMVVM.Models
{
    public class WorkoutExercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int WorkoutId { get; set; } // Foreign Key to Workout

        public string ExerciseName { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }

        public double Weight { get; set; }

        public WorkoutExercise(int workoutId ,string exerciseName, int sets, int reps, double weight)
        {
            WorkoutId = workoutId;
            ExerciseName = exerciseName;
            Sets = sets;
            Reps = reps;
            Weight = weight;
        }

        public WorkoutExercise()
        {
        }
    }
}
