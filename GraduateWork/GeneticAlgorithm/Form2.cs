using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticAlgorithm
{
    public partial class Form2 : Form
    {
        Form1 mainForm;
        string hiddenLayersTBDefault = "10";
        int activationForHiddenCBDefault = 2;
        int activationForOutputCBDefault = 2;
        decimal normalizationMinNUDDefault;
        decimal normalizationMaxNUDDefault;
        public Form2(Form1 form)
        {
            InitializeComponent();
            mainForm = form;
            normalizationMinNUDDefault = normalizationMinNUD.Value;
            normalizationMaxNUDDefault = normalizationMaxNUD.Value;
            setDefaults();
        }

        private void setDefaults()
        {
            hiddenLayersTB.Text = hiddenLayersTBDefault;
            activationForHiddenCB.SelectedIndex = activationForHiddenCBDefault;
            activationForOutputCB.SelectedIndex = activationForOutputCBDefault;
            normalizationMinNUD.Value = normalizationMinNUDDefault;
            normalizationMaxNUD.Value = normalizationMaxNUDDefault;
        }

        public void GetStructure(out List<int> hiddenLayers, out int forHidden, out int forOutput)
        {
            hiddenLayers = hiddenLayersTB.Text.Split(' ').Select(int.Parse).ToList();
            forHidden = activationForHiddenCB.SelectedIndex;
            forOutput = activationForOutputCB.SelectedIndex;
        }

        public void GetDataOptions(out float normalizationMin, out float normalizationMax)
        {
            normalizationMin = Convert.ToSingle(normalizationMinNUD.Value);
            normalizationMax = Convert.ToSingle(normalizationMaxNUD.Value);
        }

        private void saveB_Click(object sender, EventArgs e)
        {
            try
            {
                hiddenLayersTB.Text.Split(' ').Select(int.Parse);
            }
            catch
            {
                MessageBox.Show("Введите числа через пробел - размеры скрытых слоев", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ActiveControl = hiddenLayersTB;
                hiddenLayersTB.SelectAll();
                return;
            }
            hiddenLayersTBDefault = hiddenLayersTB.Text;
            activationForHiddenCBDefault = activationForHiddenCB.SelectedIndex;
            activationForOutputCBDefault = activationForOutputCB.SelectedIndex;
            normalizationMinNUDDefault = normalizationMinNUD.Value;
            normalizationMaxNUDDefault = normalizationMaxNUD.Value;
            Hide();
        }

        private void cancelB_Click(object sender, EventArgs e)
        {
            setDefaults();
            Hide();
        }
    }
}
