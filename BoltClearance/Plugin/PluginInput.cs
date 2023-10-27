using System.Collections.Generic;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;

namespace BoltClearance.Plugin
{
    public class PluginInput
    {
        public static List<PluginBase.InputDefinition> PickOneBoltGroup()
        {
            var inputDefinitionList = new List<PluginBase.InputDefinition>();

            var modelObject = PluginHelper.Picker.PickObject(Picker.PickObjectEnum.PICK_ONE_BOLTGROUP, 
                "Pick Bolt");

            inputDefinitionList.Add(new PluginBase.InputDefinition(modelObject.Identifier));

            return inputDefinitionList;
        }
    }
}