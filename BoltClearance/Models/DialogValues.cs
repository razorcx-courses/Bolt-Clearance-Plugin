using System;
using BoltClearance.Plugin;

namespace BoltClearance.Models
{
    public class DialogValues
    {
        public string ModelNumber { get; set; }
        public double RotationAngle { get; set; }
        public int BoltPosition { get; set; }

        public double DimA { get; set; }

        public double DimB { get; set; }

        public double DimC { get; set; }

        public double DimD { get; set; }

        public double DimE { get; set; }

        public double DimF { get; set; }

        public string ShowWrench { get; set; }

        public string ShowBolts { get; set; }


        public void GetValuesFromDialog(Func<object, bool> function, StructuresData data)
        {
            ModelNumber = !function(data.ModelNumber) ? data.ModelNumber: "TN-22EZ HEX";
            BoltPosition = !function(data.BoltPosition) ? Convert.ToInt32(data.BoltPosition) : 0;
            RotationAngle = !function(data.RotationAngle) ? double.Parse(data.RotationAngle) : 0.0;
            DimF = !function(data.DimF) ? data.DimF : 76.2;
            DimE = !function(data.DimE) ? data.DimE : 127;
            DimD = !function(data.DimD) ? data.DimD : 174.625;
            DimC = !function(data.DimC) ? data.DimC : 82.55;
            DimB = !function(data.DimB) ? data.DimB : 38.1;
            DimA = !function(data.DimA) ? data.DimA : 250.825;
            ShowWrench = !function(data.ShowWrench) ? data.ShowWrench : "Yes";
            ShowBolts = !function(data.ShowBolts) ? data.ShowBolts : "Yes";
        }
    }
}