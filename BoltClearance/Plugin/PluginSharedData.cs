using System.Collections.Generic;
using Tekla.Structures;
using Tekla.Structures.Model;

namespace BoltClearance.Plugin
{
    public static class PluginSharedData
    {
        public static List<int> BoltPositions { get; set; } = new List<int>();

        public static int CurrentPosition { get; set; } = 0;

        public static Identifier Identifier { get; set; }

        public static Component Component { get; set; }

        public static string ShowWrench { get; set; }

        public static string ShowBolts { get; set; }
    }
}