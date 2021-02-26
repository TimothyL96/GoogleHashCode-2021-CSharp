using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoogleHashCode2021
{
    public class Problem
    {
        public int ID { get; set; }
        public int Period { get; set; }
        public int NrIntersections { get; set; }
        public int NrStreets { get; set; }
        public int NrCars { get; set; }
        public int ScorePerCar { get; set; }

        public List<Street> Streets { get; set; } = new List<Street>();
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Intersection> Intersections{ get; set; } = new List<Intersection>();

        private string _outputFileName { get; set; }

        public Problem(string problemSet)
        {
            var dataFile = @$"D:\Timothy\Desktop\answer\{problemSet}.txt";
            _outputFileName = @$"D:\Timothy\Desktop\answer\answer_{problemSet}.txt";

            try
            {
                List<string> lines = File.ReadAllLines(dataFile).ToList();

                var index = 0;
                foreach (var line in lines)
                {
                    if (index == 0)
                    {
                        var firstLine = line.Split(' ');
                        Period = int.Parse(firstLine[0]);
                        NrIntersections = int.Parse(firstLine[1]);
                        NrStreets = int.Parse(firstLine[2]);
                        NrCars = int.Parse(firstLine[3]);
                        ScorePerCar = int.Parse(firstLine[4]);
                    }
                    else if (index <= NrStreets)
                    {
                        var streetLine = line.Split(' ');
                        var street = new Street
                        {
                            ID = index - 1,
                            StartIntersection = int.Parse(streetLine[0]),
                            EndIntersection= int.Parse(streetLine[1]),
                            Name = streetLine[2],
                            Duration = int.Parse(streetLine[3]),
                        };

                        Streets.Add(street);
                    }
                    else
                    {
                        var carLine = line.Split(' ');
                        var car = new Car
                        {
                            ID = Cars.Count,
                            Paths = Street.GetStreetID(Streets, carLine.Skip(1).ToList()),
                        };

                        Cars.Add(car);
                    }
   
                    index++;
                }

                for (int i = 0; i < NrIntersections; i++)
                {
                    var intersection = new Intersection
                    {
                        ID = i,
                        StartStreets = Street.GetStartIntersection(Streets, i),
                        EndStreets = Street.GetEndIntersection(Streets, i),
                    };

                    Intersections.Add(intersection);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void WriteToFile(List<Schedule> schedules)
        {
            List<string> answer = new List<string>()
            {
                schedules.Count().ToString(),
            };

            var key = 0;
            foreach (var item in schedules)
            {
                answer.Add(key.ToString());

                List<string> dictStrings = new List<string>();
                foreach (KeyValuePair<string, int> keyValues in item.StreetLightSchedule)
                {
                    var dictString = keyValues.Key + " " + keyValues.Value;
                    dictStrings.Add(dictString);
                }

                answer.Add(dictStrings.Count.ToString());
                answer.AddRange(dictStrings);

                key++;
            }

            try
            {
                File.WriteAllLines(_outputFileName, answer);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
