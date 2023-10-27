using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BoltClearance.Plugin;
using Tekla.Structures;
using Tekla.Structures.Dialog;
using Tekla.Structures.Model;


namespace BoltClearance.Forms
{
    public partial class PluginMainForm : PluginFormBase
    {
        public PluginMainForm()
        {
            MessageBox.Show("PluginMainForm - InitializeComponent");

            InitializeComponent();

        }

        private static void GetBoltPositions()
        {
            if (PluginSharedData.Identifier != null)
            {
                var inputItems = GetInputItems(PluginSharedData.Identifier);

                var boltGroup = inputItems.Count > 0 ? inputItems[0].GetData() as BoltGroup : null;

                PluginSharedData.BoltPositions.Clear();
                for (int i = 0; i < boltGroup.BoltPositions.Count; i++)
                {
                    PluginSharedData.BoltPositions.Add(i);
                }

                PluginSharedData.CurrentPosition = 0;
            }
        }

        private static List<InputItem> GetInputItems(Identifier identifier)
        {
            var component = PluginHelper.Model.SelectModelObject(identifier) as Component;

            var componentInput = component?.GetComponentInput();

            var inputItems = componentInput?.OfType<InputItem>().ToList();
            return inputItems;
        }


        private void OkApplyModifyGetOnOffCancel_OkClicked(object sender, EventArgs e)
        {
            this.Apply();
            this.Close();
        }

        private void OkApplyModifyGetOnOffCancel_ApplyClicked(object sender, EventArgs e)
        {
            this.Apply();
        }

        private void OkApplyModifyGetOnOffCancel_ModifyClicked(object sender, EventArgs e)
        {
            this.Modify();
        }

        private void OkApplyModifyGetOnOffCancel_GetClicked(object sender, EventArgs e)
        {
            UpdateComponentData();

            this.Get();
        }

        private void OkApplyModifyGetOnOffCancel_OnOffClicked(object sender, EventArgs e)
        {
            this.ToggleSelection();
        }

        private void OkApplyModifyGetOnOffCancel_CancelClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxBoltPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (ComboBox) sender;

            PluginSharedData.CurrentPosition = comboBox.SelectedIndex;

            this.Modify();
        }

        private void buttonLoadPositions_Click(object sender, EventArgs e)
        {
            UpdateComponentData();
        }

        private void UpdateComponentData()
        {
            var component =
                new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects().ToAList<Component>().FirstOrDefault();

            if (component == null) return;

            PluginSharedData.Identifier = component.Identifier;
            PluginSharedData.Component = component;

            GetBoltPositions();

            comboBoxBoltPosition.DataSource = null;
            comboBoxBoltPosition.DataSource = PluginSharedData.BoltPositions;
            comboBoxBoltPosition.SelectedIndex = PluginSharedData.CurrentPosition;

        }

        private void button0_Click(object sender, EventArgs e)
        {
            SetAttributeValue(textBoxRotationAngle, "0");
            this.Modify();
        }

        private void button90_Click(object sender, EventArgs e)
        {
            SetAttributeValue(textBoxRotationAngle, "90");
            this.Modify();
        }

        private void button180_Click(object sender, EventArgs e)
        {
            SetAttributeValue(textBoxRotationAngle, "180");
            this.Modify();
        }

        private void button270_Click(object sender, EventArgs e)
        {
            SetAttributeValue(textBoxRotationAngle, "270");
            this.Modify();
        }

        private void radioButtonWrench_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonBolts_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxModelNumber_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
