using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FitAppMVVM.Presentation;
using FitAppMVVM.Services;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class HomePage : Page
{
    public WorkoutViewModel ViewModel { get; set; }
    public HomePage()
    {
        this.InitializeComponent();
        this.DataContext = new HomePageViewModel();
        ViewModel = new WorkoutViewModel();
    }

    private void GoToWorkoutsPage_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to WorkoutPage
        this.Frame.Navigate(typeof(WorkoutPage));
    }

    
    private void GoToCalendarPage_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to CalendarPage
        this.Frame.Navigate(typeof(CalendarPage));
    }

    private void WorkoutListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (e.ClickedItem is Workout selectedWorkout)
        {
            Frame.Navigate(typeof(WorkoutDetailsPage), selectedWorkout);
        }
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        // Get the clicked workout
        if (sender is Button button && button.DataContext is Workout workout)
        {
            Console.WriteLine("Workout to delete: " + workout.Id);

            await DatabaseService.InitAsync();
            await DatabaseService.DeleteWorkoutAsync(workout.Id);

            // Remove from the bound collection (and update UI)
            ViewModel.Workouts.Remove(workout);
            var vm = this.DataContext as HomePageViewModel;
            if (vm != null)
            {
                vm.Workouts.Remove(workout);
            }


        }
    }




}
