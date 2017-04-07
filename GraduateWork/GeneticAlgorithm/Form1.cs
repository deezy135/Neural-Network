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
    public partial class Form1 : Form
    {

        ComputeDevice device;
        Population population;

        public Form1()
        {
            InitializeComponent();
            device = new ComputeDevice();



            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Выберите файл";
            openFileDialog1.Filter = "Файл Excel (*.xls, *.xlsm, *.xlsx)|*.xls;*.xlsm;*.xlsx";
        }

        private void openFileB_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadData(OpenFileD.FileName);
            }
        }
    }
}
