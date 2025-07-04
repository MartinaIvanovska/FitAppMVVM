using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using FitAppMVVM.Models;

namespace FitAppMVVM.Presentation
{
    public sealed partial class WorkoutDetailsPage : Page
    {
        public WorkoutDetailsViewModel ViewModel { get; set; }

        public WorkoutDetailsPage()
        {
            this.InitializeComponent();
            ViewModel = new WorkoutDetailsViewModel();
            this.DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Workout workout)
            {
                Console.WriteLine($"DEBUG: Received workout with ID: {workout.Id}"); 
                ViewModel.SetWorkout(workout);
            }
        }

        private void GoToHomePage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

        private void AddExercise_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"DEBUG: Current workout ID: {ViewModel.Workout?.Id}");
            if (ViewModel.Workout != null)
            {
                this.Frame.Navigate(typeof(AddExercisePage), ViewModel.Workout.Id);
            }
            else
            {
                Console.WriteLine("ERROR: Workout is null!");
            }

        }
    }
}
