using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BoltClearance.Models;
using Newtonsoft.Json;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Plugins;

namespace BoltClearance.Plugin
{
    [Plugin("Bolt Clearance")]
    [PluginUserInterface("BoltClearance.Forms.MainForm")]
    [PluginDescription("enu", "Bolt Clearance")]
    public class BoltClearancePlugin : PluginBase
    {
        private readonly DialogValues _dialogValues;
        private readonly StructuresData _data;
        private readonly PluginLogic _pluginLogic;

        public BoltClearancePlugin(StructuresData data)
        {
            try
            {
                PluginSharedData.Identifier = Identifier;

                _data = data;
                _dialogValues = new DialogValues();
                _pluginLogic = Factory.GetPluginLogic();

                _dialogValues.GetValuesFromDialog(IsDefault, _data);

            }
            catch (Exception ex)
            {
                Operation.DisplayPrompt(ex.Message.ToString());
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        public override List<InputDefinition> DefineInput()
        {
            try
            {
                return PluginInput.PickOneBoltGroup();
            }
            catch (Exception ex)
            {
                Operation.DisplayPrompt(ex.Message);
                return new List<InputDefinition>();
            }
        }


        public override bool Run(List<InputDefinition> input)
        {
            var current = PluginHelper.Model.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            try
            {
                if (input == null || !input.Any())
                {
                    Operation.DisplayPrompt("Input not valid. Try again.");
                    return false;
                }

                PluginSharedData.Identifier = Identifier;


                if (Identifier != null)
                {
                    PluginSharedData.Component?.SetUserProperty("GUID", 
                        PluginSharedData.Component.Identifier.GUID.ToString());
                }

                //var json = JsonConvert.SerializeObject(_data);
                //MessageBox.Show(json);

                _dialogValues.GetValuesFromDialog(IsDefault, _data);


                //debugging
                //var json = JsonConvert.SerializeObject(_dialogValues, Formatting.Indented);
                //MessageBox.Show(json);



                var boltGroup = PluginHelper.Model.SelectModelObject((Identifier) input[0].GetInput()) as BoltGroup;
                if (boltGroup == null)
                {
                    Operation.DisplayPrompt("BoltGroup not valid.");
                    return false;
                }


                //PluginSharedData.BoltPositions.Clear();
                //for (int i = 0; i < boltGroup.BoltPositions.Count; i++)
                //{
                //    PluginSharedData.BoltPositions.Add(i);
                //}

                _pluginLogic.Run(_dialogValues, boltGroup, PluginSharedData.CurrentPosition);

            }
            catch (Exception ex)
            {
                Operation.DisplayPrompt(ex.Message.ToString());

                MessageBox.Show(ex.Message + ex.StackTrace);

                return false;
            }
            finally
            {
                PluginHelper.CommitChanges();
                PluginHelper.Model.GetWorkPlaneHandler().SetCurrentTransformationPlane(current);
            }

            return true;

        }


        private bool IsDefault(object value)
        {

            if (value == null) return true;

            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                    return IsDefaultValue((int)value);
                case TypeCode.Double:
                    return IsDefaultValue((double)value);
                default:
                    return IsDefaultValue(value.ToString());
            }
        }
    }
}