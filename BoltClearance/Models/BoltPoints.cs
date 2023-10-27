using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace BoltClearance.Models
{
    public class BoltPoints
    {
        public static Point GetXPointUnderBoltHead(BoltGroup boltGroup, double boltLength)
        {
            var parts = GetParts(boltGroup);

            var boltPositions = boltGroup.BoltPositions.OfType<Point>();

            var pStart = boltPositions.FirstOrDefault();
            var pEnd = pStart + new Point(boltLength, 0.0, 0.0);

            var centerLine = new LineSegment(pStart, pEnd);

            var intersectionPoints = GetIntersectionPoints(parts, centerLine);

            var point = intersectionPoints.OrderBy(p => p).LastOrDefault();

            return point;
        }

        public static Point GetXPointUnderWasher(BoltGroup boltGroup, double boltLength)
        {
            var parts = GetParts(boltGroup);

            var boltPositions = boltGroup.BoltPositions.OfType<Point>();

            var pStart = boltPositions.FirstOrDefault();
            var pEnd = pStart - new Point(boltLength, 0.0, 0.0);

            var centerLine = new LineSegment(pStart, pEnd);

            var intersectionPoints = GetIntersectionPoints(parts, centerLine);

            var point = intersectionPoints.OrderBy(p => p).FirstOrDefault();

            return point;
        }

        private static List<Part> GetParts(BoltGroup boltGroup)
        {
            var parts = new List<Part>(boltGroup.OtherPartsToBolt.OfType<Part>())
                {boltGroup.PartToBoltTo, boltGroup.PartToBeBolted}.Where(p => p != null).ToList();
            return parts;
        }

        private static List<Point> GetIntersectionPoints(List<Part> parts, LineSegment centerLine)
        {
            var intersectionPoints = parts.SelectMany(part => part.GetSolid().Intersect(centerLine).OfType<Point>()).ToList();
            return intersectionPoints;
        }
    }
}