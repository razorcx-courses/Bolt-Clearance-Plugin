using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace BoltClearance.Models
{
    public class BoltAttribute
    {
        public BoltAttribute(
            double diameter,
            double headDiameter,
            double headLength,
            double splineDiameter,
            double splineLength)
        {
            Diameter = diameter;
            HeadDiameter = headDiameter;
            HeadLength = headLength;
            SplineDiameter = splineDiameter;
            SplineLength = splineLength;
        }

        public double Diameter { get; }

        public double HeadDiameter { get; }

        public double HeadLength { get; }

        public double SplineDiameter { get; }

        public double SplineLength { get; }

        public static List<BoltAttribute> ReadAttributesInfo()
        {

            //todo: embed as resource
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "boltInfo.json");

            var file = File.ReadAllText(path);

            var json = JsonConvert.DeserializeObject<List<BoltAttribute>>(file);

            return json;

        }
    }
}