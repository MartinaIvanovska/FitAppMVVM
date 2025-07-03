using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FitAppMVVM.Models;
using FitAppMVVM.Services;

namespace FitAppMVVM.Presentation
{
    public partial class WorkoutDetailsViewModel:ObservableObject
    {
        [ObservableProperty]
        private int workoutId;
        public WorkoutDetailsViewModel(int workoutId)
        {
            WorkoutId = workoutId;
        }
        public ObservableCollection<WorkoutExercise> Exercises { get; } = new();



        partial void OnWorkoutIdChanged(int value)
        {
            _ = LoadExercisesAsync();
        }

        [RelayCommand]
        public async Task LoadExercisesAsync()
        {
            if (WorkoutId == 0) return; // guard clause

            await DatabaseService.InitAsync();

            var exercisesFromDb = await DatabaseService.GetExercisesByWorkoutIdAsync(WorkoutId);

            Exercises.Clear();
            foreach (var exercise in exercisesFromDb)
            {
                Exercises.Add(exercise);
            }
        }

        //[RelayCommand]
        //public async Task LoadExercisesAsync()
        //{
        //    await DatabaseService.InitAsync(); // Ensure DB is ready

        //    int id = workoutId;

        //    var exercisesFromDb = await DatabaseService.GetExercisesByWorkoutIdAsync(id);

        //    Exercises.Clear();
        //    foreach (var exercise in exercisesFromDb)
        //    {
        //        Exercises.Add(exercise);
        //    }
        //}

    }
}
