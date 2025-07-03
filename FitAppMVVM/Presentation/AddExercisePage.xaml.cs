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
    public sealed partial class AddExercisePage : Page
    {
        private AddExerciseViewModel _viewModel;

        public AddExercisePage()
        {
            this.InitializeComponent();
            _viewModel = new AddExerciseViewModel();
            this.DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is int workoutId)
            {
                _viewModel.workoutId = workoutId;  // Pass workoutId to ViewModel
            }
        }

        private async void AddExerciseOn_click(object sender, RoutedEventArgs e)
        {
            // Call AddExercise command in ViewModel
            await _viewModel.AddExercise();

            // Navigate back or elsewhere after adding
            this.Frame.Navigate(typeof(WorkoutDetailsPage), _viewModel.workoutId);
        }
    }





}
