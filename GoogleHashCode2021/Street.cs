using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleHashCode2021
{
    public class Street
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int StartIntersection { get; set; }
        public int EndIntersection { get; set; }

        public static List<int> GetStreetID(List<Street> streets, List<string> names)
        {
            List<int> IDs = new List<int>();

            foreach (var name in names)
            {
                var id = streets
                .Where( x => x.Name == name )
                .Select(x =>
                {
                    return x.ID;
                }).FirstOrDefault();

                IDs.Add(id);
            }

            return IDs;
        }

        public static List<int> GetStartIntersection(List<Street> streets, int id)
        {
            var intersectionID = streets
                .Where(x => x.StartIntersection == id)
                .Select(x =>
                {
                    return x.ID;
                }).ToList();

            return intersectionID;
        }

        public static List<int> GetEndIntersection(List<Street> streets, int id)
        {
            var intersectionID = streets
                .Where(x => x.EndIntersection == id)
                .Select(x =>
                {
                    return x.ID;
                }).ToList();

            return intersectionID;
        }
    }
}
