using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq.Extensions;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace BoltClearance.Models
{
    public class ClearanceBoltBuilder
    {
        private readonly BoltBuilder _boltBuilder;
        private readonly List<BoltAttribute> _boltAttributes;
        private readonly CoordinateSystemHelper _coordinateSystemHelper;

        public ClearanceBoltBuilder(BoltBuilder boltBuilder, CoordinateSystemHelper coordinateSystemHelper)
        {
            _boltBuilder = boltBuilder;
            _boltAttributes = BoltAttribute.ReadAttributesInfo();
            _coordinateSystemHelper = coordinateSystemHelper;
        }

        public void Create(BoltGroup boltGroup)
        {
            var boltCs = _coordinateSystemHelper.SetCoordinateSystemToBoltGroup(boltGroup, 0);
            boltGroup.Select();

            var boltInformation = GetBoltInformation(boltGroup.BoltSize);

            CreateBolts(boltGroup, boltInformation);
        }

        private void CreateBolts(BoltGroup boltGroup, BoltAttribute boltInformation)
        {
            var boltLength = GetBoltLength(boltGroup);

            var profile1 = GetBoltProfile(boltGroup);

            var pointUnderBoltHead = BoltPoints.GetXPointUnderBoltHead(boltGroup, boltLength);

            var boltStandard = boltGroup.BoltStandard;

            boltGroup.BoltPositions.OfType<Point>().ForEach(p =>
            {
                var endPoint1 = CreateBoltShaft(pointUnderBoltHead, boltLength, profile1, boltStandard, boltInformation, p);

                CreateBolthead(boltInformation, endPoint1);
            });
        }

        private static string GetBoltProfile(BoltGroup boltGroup)
        {
            var profile1 = "D" + boltGroup.BoltSize;
            return profile1;
        }

        private static double GetBoltLength(BoltGroup boltGroup)
        {
            var boltLength = 0.0;
            boltGroup.GetReportProperty("LENGTH", ref boltLength);
            return boltLength;
        }

        private Point CreateBoltShaft(Point pointUnderBoltHead, double boltLength, string profile1, string boltStandard, 
            BoltAttribute boltInformation, Point p)
        {
            var startPoint = new Point(pointUnderBoltHead.X, p.Y, p.Z);
            var endPoint = startPoint;

            if (boltStandard.ToUpper().Contains("TC"))
            {
                var profile = "D" + boltInformation.SplineDiameter;
                endPoint = startPoint + new Point(boltInformation.SplineLength, 0.0, 0.0);
                _boltBuilder.Create(profile, "BOLT CLEARANCE", startPoint, endPoint);
            }

            var endPoint2 = endPoint + new Point(boltLength, 0.0, 0.0);
            _boltBuilder.Create(profile1, "BOLT CLEARANCE", endPoint, endPoint2);
            return endPoint2;
        }

        private void CreateBolthead(BoltAttribute boltInformation, Point endPoint1)
        {
            var startPoint2 = endPoint1;
            var profile2 = "D" + boltInformation.HeadDiameter;
            var endPoint2 = startPoint2 + new Point(boltInformation.HeadLength, 0.0, 0.0);
            _boltBuilder.Create(profile2, "BOLT CLEARANCE", startPoint2, endPoint2);
        }

        private BoltAttribute GetBoltInformation(double boltSize)
        {
            return _boltAttributes.FirstOrDefault(a => Math.Abs(a.Diameter - boltSize) < 0.02);
        }

    }
}