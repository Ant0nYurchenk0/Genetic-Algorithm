using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm
{
    internal class GeneticAlg
    {
        public Data Data{ get; set; }
        private int _eliteSchedules;
        private int _tournamentSelectionSize;
        private double _mutationRate;
        private Random _rnd;
        public GeneticAlg(Data data, int eliteSchedules, int tournamentSelectionSize, double mutationRate)
        {
            Data = data;
            _eliteSchedules = eliteSchedules;
            _tournamentSelectionSize = tournamentSelectionSize;
            _mutationRate = mutationRate;   
            _rnd = new Random();
        }
        public Schedule FindSolution()
        {
            var population = Data.Population.OrderByDescending(s => getFitness(s)).ToList();
            var generation = 0;
            while(!population.Any(s=>getFitness(s) == 1.0))
            {
                Console.WriteLine("Generation: {0}", generation++);
                population = evolve(population).ToList();
                population = population.OrderByDescending(s => getFitness(s)).ToList();
                foreach(var s in population)
                {
                    Console.WriteLine(" {0:N2} ", getFitness(s));
                }
                
            }
            return population.First(s => getFitness(s) == 1.0);
        }
        private double getFitness(Schedule schedule)
        {
            var conflicts = 0;
            foreach(var clas in schedule.Classes)
            {
                if (schedule.Classes.Any(c => c.Id != clas.Id && c.Teacher.Id == clas.Teacher.Id && c.Time.Id == clas.Time.Id)) conflicts++;
                if (schedule.Classes.Any(c => c.Id != clas.Id && c.Group.Id == clas.Group.Id && c.Time.Id == clas.Time.Id)) conflicts++;
                if (!Data.Courses.Single(c=>c.Id == clas.Course.Id).Teachers.Contains(clas.Teacher)) conflicts++;
            }
            return 1 / (1.0 * conflicts + 1);
        }

        private IEnumerable<Schedule> evolve(IEnumerable<Schedule> population)
        {
            return mutatePopulation(crossoverPopulation(population));
        }

        private IEnumerable<Schedule> mutatePopulation(IEnumerable<Schedule> population)
        {
            var result = new List<Schedule>();
            for (var i = 0; i < _eliteSchedules; i++)
            {
                result.Add(population.ElementAt(i));
            }
            for (var i = _eliteSchedules; i < population.Count(); i++)
            {
                var schedule = population.ElementAt(i);
                result.Add(mutateSchedule(schedule));
            }
            return result;
        }

        private Schedule mutateSchedule(Schedule schedule)
        {
            var result = new List<Class>();
            var mutation = Data.GenerateRandomSchedule(_rnd);

            for( var i = 0; i < schedule.Classes.Count(); i++)
            {
                if (_mutationRate > _rnd.NextDouble())
                    result.Add(mutation.Classes.ElementAt(i));
                else
                    result.Add(schedule.Classes.ElementAt(i));
            }
            return new Schedule(result);
        }

        private IEnumerable<Schedule> crossoverPopulation(IEnumerable<Schedule> population)
        {
            var result = new List<Schedule>();
            for (var i = 0; i < _eliteSchedules; i++)
            {
                result.Add(population.ElementAt(i));
            }
            for (var i = _eliteSchedules; i < population.Count(); i++)
            {
                var schedule1 = selectTournamentSelection(population).First();
                var schedule2 = selectTournamentSelection(population).First();
                result.Add(crossoverSchedules(schedule1, schedule2));
            }
            return result;
        }

        private Schedule crossoverSchedules(Schedule schedule1, Schedule schedule2)
        {
            var classes1 = new List<Class>();
            var classes2 = new List<Class>();
            var size = schedule1.Classes.Count();
            for (var i = 0; i < size; i++)
            {
                if(i> size / 2)
                {

                    classes1.Add(schedule1.Classes.ElementAt(i));
                    classes2.Add(schedule2.Classes.ElementAt(i));
                }
                else
                {
                    classes1.Add(schedule2.Classes.ElementAt(i));
                    classes2.Add(schedule1.Classes.ElementAt(i));
                }
            }
            var newSchedule1 = new Schedule(classes1);
            var newSchedule2 = new Schedule(classes2);
            if (getFitness(newSchedule2) > getFitness(newSchedule1)) return newSchedule2;
            return newSchedule1;
        }

        private IEnumerable<Schedule> selectTournamentSelection(IEnumerable<Schedule> population)
        {
            var result = new List<Schedule>();
            for (var i = 0;i < _tournamentSelectionSize; i++)
            {
                result.Add(population.ElementAt(_rnd.Next(0, population.Count())));
            }
            return result.OrderByDescending(s=> getFitness(s));
        }
    }
}
