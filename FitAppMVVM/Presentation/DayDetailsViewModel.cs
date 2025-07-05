using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitAppMVVM.Models;
using FitAppMVVM.Services;
using SQLite;


namespace FitAppMVVM.Presentation
{
    public partial class DayDetailsViewModel : ObservableObject
    {
        private ObservableCollection<Workout> workouts;
        public ObservableCollection<Workout> Workouts
        {
            get => workouts;
            set => SetProperty(ref workouts, value);
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
        }

        public DayDetailsViewModel(Day day)
        {
            LoadFromDay(day);
        }
        public void LoadFromDay(Day day)
        {
            Workouts = new ObservableCollection<Workout> (day.Workouts);
            SelectedDate = day.Date;
        }

    }
}

