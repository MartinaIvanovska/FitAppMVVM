using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SQLite;

using FitAppMVVM.Models;        
using FitAppMVVM.Services;

namespace FitAppMVVM.Presentation
{
    public partial class WorkoutViewModel:ObservableObject
    {
        
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string notes;


        public ObservableCollection<Workout> Workouts { get; } = new();



        [RelayCommand]
        public async Task AddWorkout()
        {

            await DatabaseService.InitAsync(); // Ensure DB is ready

            var workout = new Workout
            {
                Name = Name,
                Date = DateTime.Now,
                Notes = Notes
            };


            await DatabaseService.AddWorkoutAsync(workout);

          


            Workouts.Add(workout);

            Name = string.Empty;
            Notes = string.Empty;

        }




    }

  

}


