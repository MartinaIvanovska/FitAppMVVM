using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitAppMVVM.Models;
using FitAppMVVM.Services;
using System.Threading.Tasks;

namespace FitAppMVVM.Presentation
{
    public partial class AddExerciseViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _workoutId; // This will auto-generate WorkoutId property

        [ObservableProperty]
        private string _exerciseName;

        [ObservableProperty]
        private int _sets;

        [ObservableProperty]
        private int _reps;

        [ObservableProperty]
        private double _weight;
        public AddExerciseViewModel()
        {

        }


        [RelayCommand]
        private async Task AddExercise()
        {
            Console.WriteLine($"Attempting to save exercise to WorkoutId: {WorkoutId}");

            if (WorkoutId <= 0)
            {
                Console.WriteLine("ERROR: Invalid WorkoutId!");
                return;
            }

            await DatabaseService.InitAsync();

            var exercise = new WorkoutExercise
            {
                WorkoutId = this.WorkoutId, // Use the generated property
                ExerciseName = this.ExerciseName,
                Sets = this.Sets,
                Reps = this.Reps,
                Weight = this.Weight
            };

            await DatabaseService.AddExerciseAsync(exercise);

            ExerciseName = string.Empty;
            Sets = 0;
            Reps = 0;
            Weight = 0;

            Console.WriteLine($"Successfully added exercise to workout {WorkoutId}");
        }
    }
}
