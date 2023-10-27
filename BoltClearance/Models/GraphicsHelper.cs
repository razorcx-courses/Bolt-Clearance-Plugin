using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;

namespace BoltClearance.Models
{
    public static class GraphicsHelper
    {
        private static readonly GraphicsDrawer _graphicsDrawer = new GraphicsDrawer();

        public static void DrawText(Point origin, string message)
        {
            _graphicsDrawer.DrawText(origin, message, Colors.Red);
        }

    }
}