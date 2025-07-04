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

namespace FitAppMVVM.Presentation
{
    public sealed partial class AddExercisePage : Page
    {
        private readonly AddExerciseViewModel ViewModel;

        public AddExercisePage()
        {
            this.InitializeComponent();
            ViewModel = new AddExerciseViewModel();
            this.DataContext = ViewModel;
            Console.WriteLine($"Page created with ViewModel: {ViewModel.GetHashCode()}");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is int workoutId)
            {
                ViewModel.WorkoutId = workoutId;
                Console.WriteLine($"CONFIRMED: ViewModel {ViewModel.GetHashCode()} received WorkoutId: {workoutId}");
            }
        }


        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}

