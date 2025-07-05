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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FitAppMVVM.Presentation
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
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

        //private void GoToWorkoutDetailsPage_Click(object sender, RoutedEventArgs e)
        //{

        //    var data = new Workout
        //    {
        //        Name = TextBoxName.Text,
        //        Notes = TextBoxNote.Text
        //    };

        //    this.Frame.Navigate(typeof(WorkoutDetailsPage), data);
        //}

        private void GoToWorkoutDetailsPage_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Workout workout)
            {
                
                this.Frame.Navigate(typeof(WorkoutDetailsPage), workout);
            }
        }


    }
}
