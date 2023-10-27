using BoltClearance.Plugin;

namespace BoltClearance.Models
{
    public static class Factory
    {
        private static BoltBuilder BoltBuilder => new BoltBuilder();

        private static ContourPlateBuilder ContourPlateBuilder => 
            new ContourPlateBuilder();

        private static WrenchBuilder WrenchBuilder => 
            new WrenchBuilder(BoltBuilder, ContourPlateBuilder, CoordinateSystemHelper);

        private static CoordinateSystemHelper CoordinateSystemHelper => 
            new CoordinateSystemHelper();

        private static ClearanceBoltBuilder ClearanceBoltBuilder => 
            new ClearanceBoltBuilder(BoltBuilder, CoordinateSystemHelper);

        public static PluginLogic GetPluginLogic() => 
            new PluginLogic(WrenchBuilder, ClearanceBoltBuilder);
    }
}