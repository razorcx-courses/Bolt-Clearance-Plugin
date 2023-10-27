using Tekla.Structures.Plugins;

namespace BoltClearance.Plugin
{
    public class StructuresData
    {
        [StructuresField("ModelNumber")]
        public string ModelNumber;

        [StructuresField("RotationAngle")] 
        public string RotationAngle;

        [StructuresField("BoltPosition")]
        public string BoltPosition;

        [StructuresField("DimA")] 
        public double DimA;

        [StructuresField("DimB")] 
        public double DimB;

        [StructuresField("DimC")] 
        public double DimC;

        [StructuresField("DimD")] 
        public double DimD;

        [StructuresField("DimE")] 
        public double DimE;

        [StructuresField("DimF")] 
        public double DimF;

        [StructuresField("ShowWrench")]
        public string ShowWrench;

        [StructuresField("ShowBolts")]
        public string ShowBolts;
    }
}