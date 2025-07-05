using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using FitAppMVVM.Services;
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
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class DayDetailsPage : Page
{
    private DayDetailsViewModel _viewModel;
    //private Day _day;

    public DayDetailsPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is Day day)
        {
            //_day = day; // store it if needed later
            _viewModel = new DayDetailsViewModel(day);
            //_viewModel.LoadFromDay(day); 
            this.DataContext = _viewModel;

            Console.WriteLine($"Navigated to {day.Date} with {day.Workouts?.Count ?? 0} workouts");
            
        }
    }
    private void WorkoutListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (e.ClickedItem is Workout selectedWorkout)
        {
            Frame.Navigate(typeof(WorkoutDetailsPage), selectedWorkout);
        }
    }
    private void GoToHomePage_Click(object sender, RoutedEventArgs e)
    {
        this.Frame.Navigate(typeof(HomePage));
    }
    private void GoToCalendarView_Click(object sender, RoutedEventArgs e)
    {
        this.Frame.Navigate(typeof(CalendarPage));
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
            _viewModel.Workouts.Remove(workout);
            var vm = this.DataContext as HomePageViewModel;
            if (vm != null)
            {
                vm.Workouts.Remove(workout);
            }

        }
    }

}
