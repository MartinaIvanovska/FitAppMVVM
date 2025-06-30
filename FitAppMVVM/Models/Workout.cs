using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace FitAppMVVM.Models
{
    public class Workout
    {
       

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }
       
         public Workout( string Name, string Notes)
        {
            this.Name = Name;
            this.Date = DateTime.Now;
            this.Notes = Notes;
        }

        public Workout()
        {
        }
    }
}
