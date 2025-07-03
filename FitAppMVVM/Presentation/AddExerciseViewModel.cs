using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FitAppMVVM.Models;
using FitAppMVVM.Services;
namespace FitAppMVVM.Presentation
{
    public partial class AddExerciseViewModel:ObservableObject
    {
        [ObservableProperty]
        private string exerciseName;

        [ObservableProperty]
        private int exerciseSets;

        [ObservableProperty]
        private int exerciseReps;

        [ObservableProperty]
        private int exerciseWeight;

        
        public int workoutId;

        //public ObservableCollection<WorkoutExercise> WorkoutExercises { get; } = new();



        [RelayCommand]
        public async Task AddExercise()
        {
           

            await DatabaseService.InitAsync(); // Ensure DB is ready

            var exercise = new WorkoutExercise
            {
                ExerciseName = ExerciseName,
                Sets = ExerciseSets,
                Reps = ExerciseReps,
                Weight = ExerciseWeight,
                WorkoutId = this.workoutId
            };
 Console.WriteLine($"Adding exercise with WorkoutId={exercise.WorkoutId}");
            Console.WriteLine($"Adding exercise with WorkoutId={exercise.ExerciseName}");

            await DatabaseService.AddExerciseAsync(exercise);

            

            //ExerciseName = string.Empty;
            ExerciseSets = 0;
            ExerciseWeight =0;
            ExerciseReps = 0;

            

        }



    }
}
