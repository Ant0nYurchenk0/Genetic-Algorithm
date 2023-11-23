namespace Genetic_Algorithm
{
    internal class Program
    {
        static void Main()
        {
            var data = new Data();
            data.CreateRandomPopulation(9);
            var geneticAlg = new GeneticAlg(data, eliteSchedules: 3, tournamentSelectionSize:5, mutationRate: 0.1);
            var goodSchedule = geneticAlg.FindSolution();
            ScheduleWriter.Write(goodSchedule);
        }
    }
}