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
        //Population population;
        DataSet fileData;

        int[] selectedInputColumns;
        int[] selectedInputRows;
        int[] selectedOutputColumns;
        int[] selectedOutputRows;

        List<float[]> dataFromFile;
        List<float[]> dataFromNetwork;

        public Form1()
        {
            InitializeComponent();
            device = new ComputeDevice();
            fileData = new DataSet();

            dataFromFile = new List<float[]>();
            dataFromNetwork = new List<float[]>();

            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Выберите файл";
            openFileDialog1.Filter = "Файл Excel (*.xls, *.xlsm, *.xlsx)|*.xls;*.xlsm;*.xlsx";
        }

        private void openFileB_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadData(openFileDialog1.FileName);
            }
        }

        private bool LoadData(string filename)
        {
            fileData.Tables.Clear();
            tablesCB.Items.Clear();
            string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            using (System.Data.OleDb.OleDbConnection con = new System.Data.OleDb.OleDbConnection(constr))
            {
                con.Open();

                DataTable schemaTable = con.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                //new object[] { null, null, null, "TABLE" });
                foreach (DataRow dr in schemaTable.Rows)
                {
                    string sheetName = dr["TABLE_NAME"].ToString();
                    //if (!sheetName.EndsWith("$"))
                    //continue;

                    string select = "SELECT * FROM [" + sheetName + "]";
                    System.Data.OleDb.OleDbDataAdapter ad = new System.Data.OleDb.OleDbDataAdapter(select, con);
                    DataTable dt = new DataTable();
                    dt.TableName = sheetName;
                    ad.Fill(dt);
                    fileData.Tables.Add(dt);
                }

                con.Close();
            }

            foreach (DataTable dt in fileData.Tables)
            {
                tablesCB.Items.Add(dt.TableName);
            }

            tablesCB.SelectedIndex = 0;
            return true;
        }

        private void tablesCB_SelectedIndexChanged(object sender, EventArgs e)
        {

            //inputOutputDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            inputOutputDGV.DataSource = fileData.Tables[tablesCB.SelectedIndex];
        }

        private void setInputDataB_Click(object sender, EventArgs e)
        {
            if (checkSelectedCells(out selectedInputColumns, out selectedInputRows))
                MessageBox.Show("Выделено столбцов: " + selectedInputColumns.Length + "\nВыделено строк: " + selectedInputRows.Length);
        }

        private void setOutputDataB_Click(object sender, EventArgs e)
        {
            if (checkSelectedCells(out selectedOutputColumns, out selectedOutputRows))
                MessageBox.Show("Выделено столбцов: " + selectedOutputColumns.Length + "\nВыделено строк: " + selectedOutputRows.Length);
        }


        /// <summary>
        /// Проверяет выбранные ячейки на валидность (ячейки должны быть выровнены).
        /// </summary>
        /// <param name="selectedColumns"> Массив индексов столбцов </param>
        /// <param name="selectedRows"> Массив индексов строк </param>
        /// <returns></returns>
        private bool checkSelectedCells(out int[] selectedColumns, out int[] selectedRows)
        {
            // Количество клеток Value в столбце Key
            SortedDictionary<int, int> columnCounts = new SortedDictionary<int, int>();
            // Количество клеток Value в строке Key
            SortedDictionary<int, int> rowCounts = new SortedDictionary<int, int>();

            selectedColumns = null;
            selectedRows = null;

            if (inputOutputDGV.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не выделены ячейки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Подсчет выделенных ячеек каждого столбца и строки
            foreach (DataGridViewCell cell in inputOutputDGV.SelectedCells)
            {
                if (columnCounts.ContainsKey(cell.ColumnIndex))
                {
                    columnCounts[cell.ColumnIndex]++;
                }
                else
                {
                    columnCounts.Add(cell.ColumnIndex, 1);
                }
                if (rowCounts.ContainsKey(cell.RowIndex))
                {
                    rowCounts[cell.RowIndex]++;
                }
                else
                {
                    rowCounts.Add(cell.RowIndex, 1);
                }
            }

            // Сравнение количества выделенных ячеек в столбцах
            bool allColumnCountsAreEqual = true;
            int lastColumnCount = 0;
            foreach (KeyValuePair<int, int> entry in columnCounts)
            {
                if (entry.Value == lastColumnCount || lastColumnCount == 0)
                {
                    lastColumnCount = entry.Value;
                }
                else
                {
                    allColumnCountsAreEqual = false;
                    break;
                }
            }

            // Сравнение количества выделенных ячеек в строках
            bool allRowCountsAreEqual = true;
            int lastRowCount = 0;
            foreach (KeyValuePair<int, int> entry in rowCounts)
            {
                if (entry.Value == lastRowCount || lastRowCount == 0)
                {
                    lastRowCount = entry.Value;
                }
                else
                {
                    allRowCountsAreEqual = false;
                    break;
                }
            }

            if (!(allColumnCountsAreEqual && allRowCountsAreEqual))
            {
                MessageBox.Show("Не выровнены выделенные ячейки в таблице.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Сохранение индексов выделенных столбцов и строк
            selectedColumns = columnCounts.Keys.ToArray();
            selectedRows = rowCounts.Keys.ToArray();

            return true;
        }

        private bool checkSelectedInputOutputCells()
        {
            if (selectedInputColumns == null || selectedInputRows == null)
            {
                MessageBox.Show("Не выбраны входные значения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (selectedOutputColumns == null || selectedOutputRows == null)
            {
                MessageBox.Show("Не выбраны выходные значения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (selectedInputRows.Length != selectedOutputRows.Length)
            {
                MessageBox.Show("Размеры выборок не совпадают. Пожалуйста, выделите одинаковое количество строк для входных и выходных данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void copyAndNormalizeDataFromDGV(float minCap, float maxCap, out float[][] table, out float[] minValues, out float[] maxValues)
        {
            int setSize = selectedInputColumns.Length + selectedOutputColumns.Length;
            table = new float[setSize][];
            minValues = new float[setSize];
            maxValues = new float[setSize];
            for (int iInputCol = 0; iInputCol < selectedInputColumns.Length; ++iInputCol)
            {
                int iTableCol = iInputCol + 0;
                table[iTableCol] = new float[selectedInputRows.Length];
                minValues[iTableCol] = float.MaxValue;
                maxValues[iTableCol] = float.MinValue;

                for (int iRow = 0; iRow < selectedInputRows.Length; ++iRow)
                {
                    table[iTableCol][iRow] = Convert.ToSingle(inputOutputDGV[selectedInputColumns[iInputCol], selectedInputRows[iRow]].Value);
                    minValues[iTableCol] = Math.Min(minValues[iTableCol], table[iTableCol][iRow]);
                    maxValues[iTableCol] = Math.Max(maxValues[iTableCol], table[iTableCol][iRow]);
                }
            }
            for (int iOutputCol = 0; iOutputCol < selectedOutputColumns.Length; ++iOutputCol)
            {
                int iTableCol = iOutputCol + selectedInputColumns.Length;
                table[iTableCol] = new float[selectedOutputRows.Length];
                minValues[iTableCol] = float.MaxValue;
                maxValues[iTableCol] = float.MinValue;

                for (int iRow = 0; iRow < selectedOutputRows.Length; ++iRow)
                {
                    table[iTableCol][iRow] = Convert.ToSingle(inputOutputDGV[selectedOutputColumns[iOutputCol], selectedOutputRows[iRow]].Value);
                    minValues[iTableCol] = Math.Min(minValues[iTableCol], table[iTableCol][iRow]);
                    maxValues[iTableCol] = Math.Max(maxValues[iTableCol], table[iTableCol][iRow]);
                }
            }
            for (int iCol = 0; iCol < table.Length; ++iCol)
            {
                for (int iRow = 0; iRow < table[iCol].Length; ++iRow)
                {
                    table[iCol][iRow] = (table[iCol][iRow] - minValues[iCol]) / (maxValues[iCol] - minValues[iCol]) * (maxCap - minCap) + minCap;
                }
            }
        }

        private void denormalizeAndCopyDataToDGV(float[][] table, float minCap, float maxCap, float[] minValues, float[] maxValues)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < selectedInputColumns.Length; ++i)
            {
                dt.Columns.Add(inputOutputDGV.Columns[selectedInputColumns[i]].HeaderText);
            }
            for (int i = 0; i < selectedOutputColumns.Length; ++i)
            {
                dt.Columns.Add(inputOutputDGV.Columns[selectedOutputColumns[i]].HeaderText);
            }

            float[][] transposedTable = new float[table[0].Length][];
            for (int i = 0; i < transposedTable.Length; ++i)
            {
                transposedTable[i] = new float[table.Length];
            }
            for (int iCol = 0; iCol < table.Length; ++iCol)
            {
                float min = minValues[iCol];
                float max = maxValues[iCol];
                for (int iRow = 0; iRow < table[iCol].Length; ++iRow)
                {
                    transposedTable[iRow][iCol] = (table[iCol][iRow] - minCap) / (maxCap - minCap) * (max - min) + min;
                }
            }
            for (int iRow = 0; iRow < table[0].Length; ++iRow)
            {
                //float[] datarowf = data.GetRow(iRow);
                //object[] datarowo = new object[datarowf.Length];
                //for (int i = 0; i < datarowf.Length; ++i)
                //{
                //    datarowo[i] = datarowf[i];
                //}
                dt.Rows.Add(transposedTable[iRow].Cast<object>().ToArray());
            }
            predictionDGV.DataSource = dt;
        }

        private void buildNetworkB_Click(object sender, EventArgs e)
        {
            if (!checkSelectedInputOutputCells())
            {
                return;
            }

            IndividualDesc indDescTS = new IndividualDesc(selectedInputRows.Length / 2, 1);
            indDescTS.HiddenLayersSizes.Add(4);
            indDescTS.HiddenLayersSizes.Add(2);
            indDescTS.UpdateProperties();
            IndividualDesc indDescDS = new IndividualDesc(selectedInputColumns.Length, selectedOutputColumns.Length);
            indDescDS.HiddenLayersSizes.Add(4);
            indDescDS.HiddenLayersSizes.Add(2);
            indDescDS.UpdateProperties();

            float[][] table;
            float[][] predictionTable;
            float[] minValues;
            float[] maxValues;
            float[] dataSet;

            copyAndNormalizeDataFromDGV(0.2f, 0.8f, out table, out minValues, out maxValues);
            int setSize = selectedInputColumns.Length + selectedOutputColumns.Length;
            int totalSets = selectedInputRows.Length;
            dataSet = new float[setSize * totalSets];
            for (int iRow = 0; iRow < totalSets; ++iRow)
            {
                for (int iCol = 0; iCol < setSize; ++iCol)
                {
                    dataSet[iRow * setSize + iCol] = table[iCol][iRow];
                }
            }

            dataFromFile.Clear();
            dataFromNetwork.Clear();
            graphCB.Items.Clear();

            float[] errors = new float[selectedInputColumns.Length + 1];
            predictionTable = new float[setSize][];
            float[] predictedDataSet = null;

            Population[] populations = new Population[selectedInputColumns.Length + 1];

            for (int iPop = 0; iPop < populations.Length; ++iPop)
            {

                bool stopCondition = false;
                float maxFitness = -float.MaxValue;
                int epochs = 0;
                int lastImprovement = 0;
                int stagnationValue = Convert.ToInt32(stagnationTSNUD.Value);
                float errorThreshold = Convert.ToInt32(errorThresholdTSNUD.Value);

                if (iPop < selectedInputColumns.Length)
                {
                    populations[iPop] = new Population(Convert.ToInt32(populationSizeNUD.Value), indDescTS, device, table[iPop], true);
                }
                else
                {
                    populations[iPop] = new Population(Convert.ToInt32(populationSizeNUD.Value), indDescDS, device, dataSet, false);
                    stagnationValue = Convert.ToInt32(stagnationDSNUD.Value);
                    errorThreshold = Convert.ToInt32(errorThresholdDSNUD.Value);
                }

                while (!stopCondition)
                {
                    ++epochs;
                    populations[iPop].Epoch();
                    float curFitness = populations[iPop].GetMaxFitness();
                    if (maxFitness < curFitness)
                    {
                        maxFitness = curFitness;
                        lastImprovement = epochs;
                    }
                    if (epochs > lastImprovement + stagnationValue || -curFitness < errorThreshold)
                    {
                        stopCondition = true;
                    }
                }

                if (iPop < selectedInputColumns.Length)
                {
                    populations[iPop].PredictTimeSeries(Convert.ToInt32(predictionSizeNUD.Value));
                    predictionTable[iPop] = populations[iPop].GetOutputTimeSeries();
                    graphCB.Items.Add(inputOutputDGV.Columns[selectedInputColumns[iPop]].HeaderText);
                    dataFromFile.Add(table[iPop]);
                    dataFromNetwork.Add(predictionTable[iPop]);
                }
                else
                {
                    if (predictedDataSet == null)
                    {
                        predictedDataSet = new float[Convert.ToInt32(predictionSizeNUD.Value) * setSize];
                        for (int iCol = 0; iCol < selectedInputColumns.Length; ++iCol)
                        {
                            for (int iRow = totalSets; iRow < predictionTable[iCol].Length; ++iRow)
                            {
                                predictedDataSet[(iRow - totalSets) * setSize + iCol] = predictionTable[iCol][iRow];
                            }
                        }
                    }
                    populations[iPop].PredictDataSet(predictedDataSet);
                    float[] predictedOutput = populations[iPop].GetOutputTimeSeries();
                    int preditedOutputColumnSize = predictedOutput.Length / selectedOutputColumns.Length;
                    for (int iCol = 0; iCol < selectedOutputColumns.Length; ++iCol)
                    {
                        float[] predictedOutputColumn = new float[preditedOutputColumnSize];
                        Array.Copy(predictedOutput, iCol * preditedOutputColumnSize, predictedOutputColumn, 0, preditedOutputColumnSize);
                        predictionTable[iPop + iCol] = predictedOutputColumn;
                        graphCB.Items.Add(inputOutputDGV.Columns[selectedOutputColumns[iCol]].HeaderText);
                        dataFromFile.Add(table[iPop + iCol]);
                        dataFromNetwork.Add(predictionTable[iPop + iCol]);
                    }
                }

                errors[iPop] = -maxFitness;
            }

            // Записываем прогнозные данные в таблицу "Результат"
            denormalizeAndCopyDataToDGV(predictionTable, 0.2f, 0.8f, minValues, maxValues);
            //prediction.Denormalize(mins, maxs);
            //SetResultTable(prediction);

            // Текущий график - первый главный показатель
            graphCB.SelectedIndex = 0;

            // Переход на вкладку графиков
            tabControl1.SelectedIndex = 1;

            string strout = "Ошибки факторов: \n";
            for (int i = 0; i < errors.Length; ++i)
            {
                strout += errors[i].ToString() + "\n";
            }
            MessageBox.Show(strout, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //MessageBox.Show("Building network!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void graphCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            graphBuilder1.Clear();
            graphBuilder1.SetIntervals(0.0f, dataFromNetwork[graphCB.SelectedIndex].Length, 1.0f, 0.0f, 1.0f, 0.1f);
            //if (_rowHeaders != null) graphBuilder1.SetDelimeters(_rowHeaders, _rowHeadersStep);
            graphBuilder1.AddGraph(dataFromNetwork[graphCB.SelectedIndex], new Pen(Color.Red));
            graphBuilder1.AddGraph(dataFromFile[graphCB.SelectedIndex], new Pen(Color.Blue));
            if (graphCB.SelectedIndex > 0)
            {
                //graphBuilder1.AddGraph(_timeRowResults[graphCB.SelectedIndex - 1], new Pen(Color.Violet));
            }
            graphBuilder1.Invalidate();
        }
    }
}
