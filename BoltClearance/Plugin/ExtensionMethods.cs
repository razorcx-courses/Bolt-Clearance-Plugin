using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace BoltClearance.Plugin
{
    public static class ExtensionMethods
    {
        public static double GetLength(this BoltGroup boltGroup)
        {
            var boltLength = 0.0;
            boltGroup.GetReportProperty("LENGTH", ref boltLength);
            return boltLength;
        }

        public static List<Point> GetPositions(this BoltGroup boltGroup)
        {
            return boltGroup.BoltPositions.OfType<Point>().ToList();
        }

        public static List<T> ToAList<T>(this IEnumerator enumerator)
        {
            var list = new List<T>();

            while (enumerator.MoveNext())
            {
                try
                {
                    var current = (T)enumerator.Current;

                    if (current != null)
                        list.Add(current);
                }
                catch (Exception ex)
                {

                }
            }
            return list;
        }
    }
}