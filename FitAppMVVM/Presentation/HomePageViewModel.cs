using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using FitAppMVVM.Models;
using FitAppMVVM.Services;

namespace FitAppMVVM.Presentation
{
    public partial class HomePageViewModel : ObservableObject
    {


        public HomePageViewModel()
        {
            // Call async method but safely from constructor
            _ = LoadWorkoutsAsync();
        }
        public ObservableCollection<Workout> Workouts { get;  } = new();
        public async Task LoadWorkoutsAsync()
        {
            await DatabaseService.InitAsync(); // Ensure DB is ready

            var workoutsFromDb = await DatabaseService.GetWorkoutsAsync();

            Workouts.Clear();
            foreach (var workout in workoutsFromDb)
            {
                Workouts.Add(workout);
            }
        }

        //public async Task DeleteAsync(Workout workout)
        //{
        //    await DatabaseService.InitAsync(); // Ensure DB is ready
        //    Console.WriteLine("saka");
        //    await DatabaseService.DeleteWorkoutAsync(workout.Id);

        //    Workouts.Remove(workout);
          
        //}

        //public void Delete(Workout w)
        //{
        //    Console.WriteLine("prv klik");
        //    DeleteAsync(w);
        //}




    }
}


