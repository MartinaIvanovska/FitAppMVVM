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
using System.Collections.ObjectModel;


using FitAppMVVM.Services;
namespace FitAppMVVM.Presentation
{

	public sealed partial class WorkoutPage : Page
	{
		public WorkoutPage()
		{
			this.InitializeComponent();
            this.DataContext = new WorkoutViewModel();
        }

        private void GoToHomePage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

        private void GoToWorkoutDetailsPage_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Workout workout)
            {
                
                this.Frame.Navigate(typeof(WorkoutDetailsPage), workout);
            }
        }
        private async void AddWorkoutAndNavigate_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is WorkoutViewModel viewModel)
            {
              
                var newWorkout = await viewModel.AddWorkoutAsync();

                if (newWorkout != null)
                {
                    this.Frame.Navigate(typeof(WorkoutDetailsPage), newWorkout);
                }
            }
        }


    }
}
