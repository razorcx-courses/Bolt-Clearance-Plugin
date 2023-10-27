using BoltClearance.Models;
using Tekla.Structures.Model;

namespace BoltClearance.Plugin
{
    public class PluginLogic
    {
        private readonly WrenchBuilder _wrenchBuilder;
        private readonly ClearanceBoltBuilder _clearanceBoltBuilder;

        public PluginLogic(WrenchBuilder wrenchBuilder, ClearanceBoltBuilder clearanceBoltBuilder)
        {
            _wrenchBuilder = wrenchBuilder;
            _clearanceBoltBuilder = clearanceBoltBuilder;
        }

        public void Run(DialogValues dialogValues, BoltGroup boltGroup, int boltPosition)
        {
            if(dialogValues.ShowBolts == "Yes")
            //if (PluginSharedData.ShowBolts == "Yes")
                _clearanceBoltBuilder.Create(boltGroup);

            if(dialogValues.ShowWrench == "Yes")
            //if (PluginSharedData.ShowWrench == "Yes")
                _wrenchBuilder.Create(dialogValues, boltGroup, boltPosition);
        }
    }
}