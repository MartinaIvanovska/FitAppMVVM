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

        [ObservableProperty]
        public int workoutId;

        public AddExerciseViewModel(int w)
        {
            WorkoutId = w;
            Console.WriteLine($"DEBUG: ViewModel created. Current WorkoutId: {WorkoutId}");
        }

        [RelayCommand]
        public async Task AddExercise()
        {
            if (string.IsNullOrWhiteSpace(ExerciseName))
            {
                
                return;
            }

            await DatabaseService.InitAsync();

            Console.WriteLine(WorkoutId);
            

            var exercise = new WorkoutExercise
            {
                ExerciseName = ExerciseName,
                Sets = ExerciseSets,
                Reps = ExerciseReps,
                Weight = ExerciseWeight,
                WorkoutId = WorkoutId
            };

            Console.WriteLine(exercise.WorkoutId);
            Console.WriteLine(exercise.ExerciseName);

            await DatabaseService.AddExerciseAsync(exercise);

            ExerciseName = string.Empty;
            ExerciseSets = 0;
            ExerciseReps = 0;
            ExerciseWeight = 0;
        }
    }

   }

