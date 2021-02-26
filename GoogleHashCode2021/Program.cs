using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoogleHashCode2021
{
    public class Program
    {
        static void Main(string[] args)
        {
            StartProblem("a");
            StartProblem("b");
        }

        static void StartProblem(string problem)
        {
            Console.WriteLine($"Problem: {problem}");

            Problem p = new Problem(problem);

            var results = OneForAll(p);

            p.WriteToFile(results);
        }

        static List<Schedule> OneEach(Problem p)
        {
            var schedules = new List<Schedule>();

            foreach (var item in p.Intersections)
            {
                var schedule = new Schedule
                {
                    NrIncomingStreets = item.EndStreets.Count,
                };

                var total = 0;
                var val = 1;

                foreach (var streetID in item.EndStreets)
                {
                    var street = p.Streets.Where(x => x.ID == streetID).FirstOrDefault();
                    schedule.StreetLightSchedule.Add(street.Name, val);

                    total += val;

                    if (total >= p.Period)
                    {
                        break;
                    }
                }

                schedules.Add(schedule);
            }

            return schedules;
        }

        static List<Schedule> OneForAll(Problem p)
        {
            var schedules = new List<Schedule>();

            foreach (var item in p.Intersections)
            {
                var schedule = new Schedule
                {
                    NrIncomingStreets = item.EndStreets.Count,
                };

                schedule.StreetLightSchedule.Add(p.Streets.Where(x => x.ID == item.EndStreets[0]).FirstOrDefault().Name, p.Period);

                schedules.Add(schedule);
            }

            return schedules;
        }

        static void Linear()
        {
            // GLOP Linear Solver
            // SCIP Integer solver
            Solver solver = Solver.CreateSolver("GLOP");

            Variable x = solver.MakeBoolVar("x");
            Variable y = solver.MakeNumVar(0.0, double.PositiveInfinity, "y");

            solver.Add(x + 2 * y <= 14.0);
            solver.Add(3 * x - y >= 0.0);

            solver.Maximize(3 * x + 4 * y);
            // Time needed = i1 + i2
            // Steps <= period
            Solver.ResultStatus resultStatus = solver.Solve();

            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                Console.WriteLine("No optimal solution");
                return;
            }

            Console.WriteLine("Solution:", solver.Objective().Value());
        }
    }
}
