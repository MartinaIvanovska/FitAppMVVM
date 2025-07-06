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

namespace FitAppMVVM.Presentation;
using FitAppMVVM.Services;
using Microsoft.UI;


public sealed partial class HomePage : Page
{
    public WorkoutViewModel ViewModel { get; set; }
    private bool isDarkTheme = true;

    public HomePage()
    {
        this.InitializeComponent();
        this.DataContext = new HomePageViewModel();
        ViewModel = new WorkoutViewModel();
    }

    private void GoToWorkoutsPage_Click(object sender, RoutedEventArgs e)
    {
      
        this.Frame.Navigate(typeof(WorkoutPage));
    }

    
    private void GoToCalendarPage_Click(object sender, RoutedEventArgs e)
    {
        
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
        
        if (sender is Button button && button.DataContext is Workout workout)
        {
            Console.WriteLine("Workout to delete: " + workout.Id);

            var dialog = new ContentDialog
            {
                Title = "Confirmation",
                Content = $"Do you want to delete workout: {workout.Name}?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = (sender as FrameworkElement)?.XamlRoot  
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {

                await DatabaseService.InitAsync();
                await DatabaseService.DeleteWorkoutAsync(workout.Id);

                ViewModel.Workouts.Remove(workout);
                var vm = this.DataContext as HomePageViewModel;
                if (vm != null)
                {
                    vm.Workouts.Remove(workout);
                }

            }
            else
            {
                return;
            }


        }
    }

    

}
