using System.Linq;
using BoltClearance.Plugin;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace BoltClearance.Models
{
    public class CoordinateSystemHelper
    {
        public CoordinateSystem SetCoordinateSystemToBoltGroup(BoltGroup boltGroup, int index)
        {
            var boltCs = boltGroup.GetCoordinateSystem();
            var xVector = boltCs.AxisX.Cross(boltCs.AxisY).GetNormal();

            var positions = boltGroup.GetPositions();

            var boltPosition = positions.Count > index ? positions[index] : positions.FirstOrDefault();

            PluginHelper.Model.GetWorkPlaneHandler()
                .SetCurrentTransformationPlane(new TransformationPlane(boltPosition, xVector, boltCs.AxisY));
            return boltCs;
        }
    }
}