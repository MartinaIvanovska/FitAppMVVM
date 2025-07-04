using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitAppMVVM.Models;
using FitAppMVVM.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FitAppMVVM.Presentation
{
    public partial class WorkoutDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        public Workout workout;

        [ObservableProperty]
        private ObservableCollection<WorkoutExercise> exercises = new();

        public int workoutId;

        public WorkoutDetailsViewModel()
        {
            _ = LoadExercisesAsync();
        }

        public void SetWorkout(Workout workout)
        {
            Workout = workout;
            workoutId = workout.Id;
            _ = LoadExercisesAsync();
        }

        private async Task LoadExercisesAsync()
        {
            if (workoutId == 0) return;

            await DatabaseService.InitAsync();
            var exercisesFromDb = await DatabaseService.GetExercisesForWorkoutAsync(workoutId);

            Exercises.Clear();
            foreach (var exercise in exercisesFromDb)
            {
                Exercises.Add(exercise);
            }
        }

        [RelayCommand]
        private async Task AddExercise()
        {
            // No navigation logic here - this will be handled in the code-behind
            await Task.CompletedTask;
        }
    }
}
