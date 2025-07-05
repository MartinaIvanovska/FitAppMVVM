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
     
    public partial class CalendarViewModel : ObservableObject
    {
        private DateTime _currentMonth = DateTime.Today;

        private string _monthYearLabel;
        public string MonthYearLabel
        {
            get => _monthYearLabel;
            set => SetProperty(ref _monthYearLabel, value);
        }
        public CalendarViewModel()
        {
            _ = LoadMonthAsync(DateTime.Today);
        }

        private ObservableCollection<Day> _days = new ObservableCollection<Day>();
        public ObservableCollection<Day> Days
        {
            get => _days;
            set => SetProperty(ref _days, value);
        }

        public DateTime CurrentMonth
        {
            get => _currentMonth;
            set => SetProperty(ref _currentMonth, value);
        }
        public async Task LoadMonthAsync(DateTime month)
        {
            CurrentMonth = month;
            MonthYearLabel = month.ToString("MMMM yyyy");

            await DatabaseService.InitAsync();
            var start = new DateTime(month.Year, month.Month, 1);
            var end = start.AddMonths(1).AddDays(-1);

            var WorkoutsFromDb = await DatabaseService.GetWorkoutsAsync();

            var newDays = new ObservableCollection<Day>();

            for (int i = 0; i < end.Day; i++)
            {
                var date = start.AddDays(i);
                var workouts = WorkoutsFromDb
                    .Where(e => e.Date.Date == date)
                    .ToList();

                newDays.Add(new Day
                {
                    Date = date,
                    Workouts = workouts
                });
            }

            Days = newDays;
        }

        public async Task LoadNextMonthAsync()
        {
            CurrentMonth = CurrentMonth.AddMonths(1);
            await LoadMonthAsync(CurrentMonth);
        }

        public async Task LoadPreviousMonthAsync()
        {
            CurrentMonth = CurrentMonth.AddMonths(-1);
            await LoadMonthAsync(CurrentMonth);
        }

        
    }

}
