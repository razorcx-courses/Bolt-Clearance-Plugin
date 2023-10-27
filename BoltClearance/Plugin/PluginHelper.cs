using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace BoltClearance.Plugin
{
    public static class PluginHelper
    {
        public static readonly Model Model = new Model();

        public static readonly Picker Picker = new Picker();

        public static bool CommitChanges(string message = "") => Model.CommitChanges(message);
    }
}