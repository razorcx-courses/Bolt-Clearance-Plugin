using System;
using System.Collections.Generic;
using System.Linq;
using BoltClearance.Plugin;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace BoltClearance.Models
{
    public class WrenchBuilder
    {
        private readonly BoltBuilder _boltBuilder;
        private readonly ContourPlateBuilder _contourPlateBuilder;
        private readonly CoordinateSystemHelper _coordinateSystemHelper;


        public WrenchBuilder(BoltBuilder boltBuilder, ContourPlateBuilder contourPlateBuilder, 
            CoordinateSystemHelper coordinateSystemHelper)
        {
            _boltBuilder = boltBuilder;
            _contourPlateBuilder = contourPlateBuilder;
            _coordinateSystemHelper = coordinateSystemHelper;
        }

        public void Create(DialogValues dialogValues, BoltGroup boltGroup, int index)
        {
            _coordinateSystemHelper.SetCoordinateSystemToBoltGroup(boltGroup, index);
            boltGroup.Select();

            //wrench shaft length
            var endPoint = new Point(dialogValues.DimE * -1, 0.0, 0.0);

            var profile = "D" + dialogValues.DimF;

            var endpoint1 = CreateWrenchShaft(boltGroup, endPoint, profile, index);

            //if (dialogValues.CreateBackOfWrench.Equals("Yes"))
            //{
                CreateBackOfWrench(dialogValues, endpoint1);
            //}
        }


        private void CreateBackOfWrench(DialogValues dialogValues, Point startPoint)
        {
            var points = GetContourPoints(dialogValues, startPoint);

            var rotatedPoints = GetRotatedPoints(dialogValues.RotationAngle, points);

            _contourPlateBuilder.Create(dialogValues.DimC, rotatedPoints, "WRENCH CLEARANCE");
        }

        private static List<Point> GetContourPoints(DialogValues dialogValues, Point startPoint)
        {
            var height = dialogValues.DimD;
            var length = dialogValues.DimA + dialogValues.DimB;
            var offset = dialogValues.DimB;

            var p1 = startPoint - new Point(0.0, 0.0, offset);
            var p2 = startPoint - new Point(0.0, 0.0, -length - offset);
            var p3 = startPoint - new Point(height, 0.0, -length - offset);
            var p4 = startPoint - new Point(height, 0.0, offset);

            var points = new List<Point> { p1, p2, p3, p4 };

            return points;
        }

        private static List<Point> GetRotatedPoints(double angle, List<Point> points)
        {
            var radians = angle / 180.0 * Math.PI;

            var matrix = MatrixFactory.Rotate(radians, new Vector(1.0, 0.0, 0.0));

            var newPoints = points.Select(p => matrix.Transform(p)).ToList();

            return newPoints;
        }

        private Point CreateWrenchShaft(BoltGroup boltGroup, Point endPoint, string profile, int index)
        {
            var boltLength = boltGroup.GetLength();

            var pointUnderBoltHead = BoltPoints.GetXPointUnderWasher(boltGroup, boltLength);

            var positions = boltGroup.GetPositions();

            var boltPosition = positions.Count >= index ? positions[index] : positions.First();
            boltPosition.X = pointUnderBoltHead.X;

            var endPoint1 = boltPosition + endPoint;

            _boltBuilder.Create(profile, "WRENCH CLEARANCE", boltPosition, endPoint1);

            return endPoint1;
        }
    }
}