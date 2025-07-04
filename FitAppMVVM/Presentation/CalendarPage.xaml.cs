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
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class CalendarPage : Page
{
    public CalendarViewModel ViewModel { get; private set; } = new CalendarViewModel();

    public CalendarPage()
    {
        this.InitializeComponent();
        ViewModel = new CalendarViewModel();
        this.DataContext = ViewModel;
        
    }
    private void GoToHomePage_Click(object sender, RoutedEventArgs e)
    {
        this.Frame.Navigate(typeof(HomePage));
    }
    
    private async void PreviousMonth_Click(object sender, RoutedEventArgs e)
    {
        await ViewModel.LoadPreviousMonthAsync();
    }

    private async void NextMonth_Click(object sender, RoutedEventArgs e)
    {
        await ViewModel.LoadNextMonthAsync();
    }

    private void DayClicked(object sender, ItemClickEventArgs e)
    {
        if (e.ClickedItem is Day selectedDay)
        {
            Frame.Navigate(typeof(DayDetailsPage), selectedDay);
        }
    }

}
