using System;
using BoltClearance.Plugin;
using Tekla.Structures;
using Tekla.Structures.Model;

namespace BoltClearanceConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Bolt Clearance App Started Successfully");

                var structuresData = GetStructuresData();

                var boltClearance = new BoltClearancePlugin(structuresData);

                Console.WriteLine("Select Bolt Group");

                var input = boltClearance.DefineInput();

                var boltGroup = PluginHelper.Model.SelectModelObject((Identifier)input[0].GetInput()) as BoltGroup;

                Console.WriteLine($"Bolt Group: {Math.Round(boltGroup.BoltSize, 2)}-{boltGroup.BoltStandard}-Qty({boltGroup.BoltPositions.Count})");


                Console.WriteLine("Enter wrench rotation angle: ");
                var angle = Console.ReadLine();

                structuresData.RotationAngle = angle;

                var result = boltClearance.Run(input);

                if(result)
                    Console.WriteLine($"Check bolts inserted successfully");

                Console.WriteLine("Press any key ...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}, {ex.StackTrace}");
                Console.WriteLine("Press any key ...");
                Console.ReadKey();
            }
        }

        private static StructuresData GetStructuresData()
        {
            var structuresData = new StructuresData
            {
                RotationAngle = "0",
                DimA = 209.55,
                DimB = 38.1,
                DimC = 82.55,
                DimD = 146.05,
                DimE = 136.525,
                DimF = 76.2,
                //CreateBackOfWrench = "Yes",
                //ClearanceType = "Wrench Clearance",
                //BoltAlignment = "By Points"
            };
            return structuresData;
        }
    }
}
