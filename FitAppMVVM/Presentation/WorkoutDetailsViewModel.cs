using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FitAppMVVM.Models;
using FitAppMVVM.Services;

namespace FitAppMVVM.Presentation
{
    public partial class WorkoutDetailsViewModel : ObservableObject
    {
        private int _workoutId;
        public ObservableCollection<WorkoutExercise> Exercises { get; } = new();

        public WorkoutDetailsViewModel(int workoutId)
        {
            _workoutId = workoutId;
            LoadExercises();
        }

        private async void LoadExercises()
        {
            await DatabaseService.InitAsync();
            var exercises = await DatabaseService.GetExercisesByWorkoutIdAsync(_workoutId);

            Exercises.Clear();
            foreach (var exercise in exercises)
            {
                Exercises.Add(exercise);
            }
        }

      

    }

 

}

