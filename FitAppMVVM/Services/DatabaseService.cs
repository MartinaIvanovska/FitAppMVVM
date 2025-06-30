using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using Microsoft.UI.Xaml.Shapes;


namespace FitAppMVVM.Services
{
    public static class DatabaseService
    {
        private static SQLiteAsyncConnection _database;

        public static async Task InitAsync()
        {
            if (_database != null)
                return;

            string dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workouts.db");

            Console.WriteLine($"📁 DB Path: {dbPath}");

            _database = new SQLiteAsyncConnection(dbPath);
            await _database.CreateTableAsync<Workout>();
            await _database.CreateTableAsync<WorkoutExercise>();

        }

        public static Task<int> AddWorkoutAsync(Workout workout)
        {
            return _database.InsertAsync(workout);
        }

        public static Task<List<Workout>> GetWorkoutsAsync()
        {
            return _database.Table<Workout>().ToListAsync();
        }
    }
}
