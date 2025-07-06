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


namespace FitAppMVVM.Presentation
{
   
    public sealed partial class AddExercisePage : Page
    {
        private AddExerciseViewModel _viewModel;
        public AddExercisePage()
        {
            this.InitializeComponent();
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
            base.OnNavigatedTo(e);

            Console.WriteLine($"Received WorkoutId: {e.Parameter}");
            if (e.Parameter is int workoutId)
            {
                _viewModel = new AddExerciseViewModel(workoutId);
                this.DataContext = _viewModel;
                Console.WriteLine($"Received WorkoutId: {workoutId}");
            }
        }
        private void GoToDetails_click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
        
    }


}
