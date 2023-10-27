using System.Collections.Generic;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace BoltClearance.Models
{
    public class ContourPlateBuilder
    {
        public void Create(double width, List<Point> points, string name)
        {
            var contourPlate = new ContourPlate
            {
                Class = "6",
                Name = name,
                PartNumber = {StartNumber = -1, Prefix = "xxx"},
                AssemblyNumber = {StartNumber = -1, Prefix = "XXX"},
                Position =
                {
                    Depth = Position.DepthEnum.MIDDLE,
                    DepthOffset = 0,
                    Rotation = Position.RotationEnum.TOP,
                    Plane = Position.PlaneEnum.MIDDLE,
                },
                Material = {MaterialString = "Steel_Undefined"},
                Finish = "",
                Profile = { ProfileString = "PL" + width}
            };

            points.ForEach(p =>
            {
                contourPlate.AddContourPoint(new ContourPoint(p, new Chamfer()));

            });

            contourPlate.Insert();
        }
    }
}