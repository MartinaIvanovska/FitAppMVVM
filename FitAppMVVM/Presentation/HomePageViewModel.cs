using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using FitAppMVVM.Models;
using FitAppMVVM.Services;
using Microsoft.UI;

namespace FitAppMVVM.Presentation
{
    public partial class HomePageViewModel : ObservableObject
    {

        public SolidColorBrush GradientColor => new SolidColorBrush(Colors.MediumPurple);
        public HomePageViewModel()
        {
            
            _ = LoadWorkoutsAsync();
        }
        public ObservableCollection<Workout> Workouts { get;  } = new();
        public async Task LoadWorkoutsAsync()
        {
            await DatabaseService.InitAsync(); 

            var workoutsFromDb = await DatabaseService.GetWorkoutsAsync();

           
            var latestSeven = workoutsFromDb.OrderByDescending(w => w.Date).Take(7).ToList();

             Workouts.Clear();

            foreach(var workout in latestSeven)
            {
                Workouts.Add(workout);
            }

          
        }

    



    }
}


