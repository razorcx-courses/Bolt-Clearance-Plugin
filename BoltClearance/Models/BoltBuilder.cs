using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace BoltClearance.Models
{
    public class BoltBuilder
    {
        public void Create(string profile, string name, Point startPoint, Point endPoint)
        {
            var beam = new Beam(startPoint, endPoint)
            {
                Class = "6",
                Name = name,
                PartNumber = {StartNumber = -1, Prefix = "xxx"},
                AssemblyNumber = {StartNumber = -1, Prefix = "XXX"},
                Position =
                {
                    Depth = Position.DepthEnum.MIDDLE,
                    Rotation = Position.RotationEnum.TOP,
                    Plane = Position.PlaneEnum.MIDDLE
                },
                Material = {MaterialString = "Steel_Undefined"},
                Profile = {ProfileString = profile},
                Finish = ""
            };
            beam.Insert();
        }
    }
}