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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FitAppMVVM.Presentation
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class WorkoutDetailsPage : Page
	{
        private WorkoutDetailsViewModel _viewModel;
        private int id;

        public WorkoutDetailsPage()
        {
            this.InitializeComponent();
            // Removed DataContext assignment here
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {



            if (e.Parameter is Workout workout)
            {
                _viewModel = new WorkoutDetailsViewModel(workout.Id);
                this.DataContext = workout;
                id = workout.Id;
            }

            //base.OnNavigatedTo(e);

            //if (e.Parameter is Workout workout)
            //{
            //    this.DataContext = workout; 
            //}
        }


        private void GoToHomePage_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to WorkoutPage
            this.Frame.Navigate(typeof(HomePage));
        }

        private void AddExercise_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(AddExercisePage), id);
        }

    }
}
