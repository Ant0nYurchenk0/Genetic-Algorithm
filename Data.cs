using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm
{
    internal class Data
    {
        public Data()
        {
            Times = new();
            Groups = new();
            Courses = new();
            Teachers = new();
            Population = new();

            createTimes();
            createGroups();
            createTeachers();
            createCourses();
        }
        internal List<Time> Times { get; private set; }
        internal List<Teacher> Teachers { get; private set; }
        internal List<Group> Groups { get; private set; }
        internal List<Course> Courses { get; private set; }
        internal List<Schedule> Population { get; set; }
        public IEnumerable<Schedule> CreateRandomPopulation(int populationSize)
        {
            var rnd = new Random();

            for (int i = 0; i < populationSize; i++)
                Population.Add(GenerateRandomSchedule(rnd));

            return Population;
        }
        public Schedule GenerateRandomSchedule(Random rnd)
        {

            var classes = new List<Class>();
            foreach (var group in Groups)
            {
                foreach (var course in Courses)
                {
                    var time = Times.ElementAt(rnd.Next(0, Times.Count));
                    var teacher = Teachers.ElementAt(rnd.Next(0, Teachers.Count));
                    classes.Add(new Class(time, group, teacher, course));
                }
            }
            return new Schedule(classes);
        }
        private void createTimes()
        {
            Times.Add(new Time("09.00-10.30"));
            Times.Add(new Time("10.30-12.00"));
            Times.Add(new Time("12.00-13.30"));
            Times.Add(new Time("13.30-15.00"));
            Times.Add(new Time("15.00-16.30"));
        }
        private void createGroups()
        {
            Groups.Add(new Group("K-14"));
            Groups.Add(new Group("K-24"));
            Groups.Add(new Group("MI-3"));
            Groups.Add(new Group("MI-4"));
        }
        private void createTeachers()
        {
            Teachers.Add(new Teacher("Molodtsov"));
            Teachers.Add(new Teacher("Rubliov"));
            Teachers.Add(new Teacher("Stavrovskyi"));
            Teachers.Add(new Teacher("Rabanovych"));
            Teachers.Add(new Teacher("Rybalka"));
            Teachers.Add(new Teacher("Yakymiv"));
            Teachers.Add(new Teacher("Marynych"));
            Teachers.Add(new Teacher("Koval"));
        }
        private void createCourses()
        {
            Courses.Add(new Course("Mat. analysis", Teachers.Where(t => t.Name == "Molodtsov" || t.Name == "Rubliov").ToList()));
            Courses.Add(new Course("Operations Research", Teachers.Where(t=>t.Name == "Yakymiv" || t.Name == "Marynych").ToList()));
            Courses.Add(new Course("Linear algebra", Teachers.Where(t => t.Name == "Rabanovych" || t.Name == "Yakymiv" || t.Name == "Rybalka").ToList()));
            Courses.Add(new Course("Programming", Teachers.Where(t => t.Name == "Koval" || t.Name == "Stavrovskyi").ToList()));
        }
    }
}
