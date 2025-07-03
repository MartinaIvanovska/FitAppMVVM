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
		public WorkoutDetailsPage()
		{
			this.InitializeComponent();
            this.DataContext = new WorkoutDetailsViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Workout data)
            {
                string name = data.Name;
                string note = data.Notes;

                TextBlockName.Text = name;
                TextBlockNote.Text = note;

            }
        }

        private void GoToHomePage_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to WorkoutPage
            this.Frame.Navigate(typeof(HomePage));
        }

    }
}
