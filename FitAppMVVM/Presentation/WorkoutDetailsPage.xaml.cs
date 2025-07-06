using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using FitAppMVVM.Services;
using Uno.Extensions;
namespace FitAppMVVM.Presentation
{
    public sealed partial class WorkoutDetailsPage : Page
    {
        private WorkoutDetailsViewModel _viewModel;
        private int _workoutId;

        public WorkoutDetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Workout workout)
            {
                _workoutId = workout.Id; // Store the ID
                _viewModel = new WorkoutDetailsViewModel(workout.Id);
                TextBlockName.Text = workout.Name;
                TextBlockNote.Text = workout.Notes;
                this.DataContext = _viewModel;
            }
        }

        private void GoToHomePage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

        private void AddExercise_Click(object sender, RoutedEventArgs e)
        {
           Console.WriteLine($"Passing WorkoutId: {_workoutId}");
            // Pass the stored workout ID
            this.Frame.Navigate(typeof(AddExercisePage), _workoutId);
        }

        
       private async void DeleteExercise_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is WorkoutExercise exercise)
            {
                Console.WriteLine("Exercise to delete: " + exercise.Id);

                await DatabaseService.InitAsync();
                await DatabaseService.DeleteExerciseAsync(exercise.Id);

                // Remove from the bound collection (and update UI)
                _viewModel.Exercises.Remove(exercise);


            }
        }


    }
}
