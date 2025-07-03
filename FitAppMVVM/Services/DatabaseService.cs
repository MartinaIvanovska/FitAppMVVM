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

        public static Task<int> AddExerciseAsync(WorkoutExercise exercise)
        {
            return _database.InsertAsync(exercise);
        }

        public static async Task DeleteWorkoutAsync(int id)
        {
            int rez= await _database.DeleteAsync<Workout>(id);
            Console.WriteLine(rez);
        }

        public static Task<List<Workout>> GetWorkoutsAsync()
        {
            return _database.Table<Workout>().ToListAsync();
        }

        public static async Task<List<WorkoutExercise>> GetExercisesByWorkoutIdAsync(int workoutId)
        {
            await InitAsync(); // just in case
            Console.WriteLine($"Getting exercises for WorkoutId: {workoutId}");

            var result = await _database.Table<WorkoutExercise>()
                                        .Where(ex => ex.WorkoutId == workoutId)
                                        .ToListAsync();

            Console.WriteLine($"Found {result.Count} exercises");
            return result;
        }




    }
}
