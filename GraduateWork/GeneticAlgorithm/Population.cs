using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    struct IndividualDesc
    {
        public int InputLayerSize;
        public int OutputLayerSize;
        public List<int> HiddenLayersSizes;
        public int TotalWeights;
        public int MaxLayerSize;
        public bool Updated;

        public int ForHidden;
        public int ForOutput;
        public int[] ActivationFunctions;

        public IndividualDesc(int inputLayerSize, int outputLayerSize)
        {
            InputLayerSize = inputLayerSize;
            OutputLayerSize = outputLayerSize;
            HiddenLayersSizes = new List<int>();
            TotalWeights = 0;
            MaxLayerSize = 0;
            Updated = false;

            ForHidden = 0;
            ForOutput = 0;
            ActivationFunctions = null;
        }

        public void UpdateProperties()
        {
            TotalWeights = 0;
            MaxLayerSize = Math.Max(InputLayerSize, OutputLayerSize);
            int prevLayerSize = InputLayerSize;
            foreach (int layerSize in HiddenLayersSizes)
            {
                TotalWeights += (prevLayerSize + 1) * layerSize;
                MaxLayerSize = (MaxLayerSize < layerSize ? layerSize : MaxLayerSize);
                prevLayerSize = layerSize;
            }
            TotalWeights += (prevLayerSize + 1) * OutputLayerSize;
            ++MaxLayerSize;
            Updated = true;

            ActivationFunctions = new int[HiddenLayersSizes.Count + 1];
            for (int i = 0; i < HiddenLayersSizes.Count; ++i)
            {
                ActivationFunctions[i] = ForHidden;
            }
            ActivationFunctions[HiddenLayersSizes.Count] = ForOutput;
        }
    }
    struct Individual
    {
        public int Offset;
        public float Fitness;
    }

    class IndividualComparer : Comparer<Individual>
    {
        public override int Compare(Individual x, Individual y)
        {
            return y.Fitness.CompareTo(x.Fitness);
        }
    }

    class Population
    {
        private int             populationSize;
        private Random          randomGenerator;
        private IndividualDesc  indDesc;
        private int             genomeLength;
        private float[]         genotypes;
        private Individual[]    phenotypes;
        private float eliteRatio;
        private float mutationRatio;
        private float avgFitness;
        private float maxFitness;

        bool usingTimeSeries;
        private float[] inputData;
        private float[] outputData;

        private ComputeDevice computeDevice;

        public Population(int size, IndividualDesc desc, ComputeDevice device, float[] dataSet, bool isTimeSeries, int seed = 0)
        {
            populationSize = size;
            if (seed == 0) {
                randomGenerator = new Random();
            }
            else {
                randomGenerator = new Random(seed);
            }
            if (desc.Updated == false)
            {
                desc.UpdateProperties();
            }
            indDesc = desc;
            usingTimeSeries = isTimeSeries;
            genomeLength = indDesc.TotalWeights;

            genotypes = new float[populationSize * genomeLength];
            for (int i = 0; i < genotypes.Length; ++i)
            {
                genotypes[i] = Convert.ToSingle(randomGenerator.NextDouble()) * 2 - 1;
            }
            phenotypes = new Individual[populationSize];
            for (int i = 0; i < phenotypes.Length; ++i)
            {
                phenotypes[i].Fitness = 0;
                phenotypes[i].Offset = i * genomeLength;
            }

            inputData = dataSet;
            
            computeDevice = device;

            maxFitness = -float.MaxValue;
            avgFitness = 0.0f;

            eliteRatio = 0.1f;
            mutationRatio = 0.05f;
        }

        public float[] GetOutputTimeSeries()
        {
            return outputData;
        }
        public float GetMaxFitness()
        {
            return maxFitness;
        }

        public void Epoch()
        {
            float[] errors;

            // Evaluation
            if (usingTimeSeries)
            {
                computeDevice.ProcessTimeSeries(genotypes, indDesc, inputData, out outputData, out errors);
            }
            else
            {
                computeDevice.ProcessDataSet(genotypes, indDesc, inputData, out outputData, out errors);
            }

            // Computing fitness
            float sumFitness = 0.0f;
            for (int i = 0; i < populationSize; ++i)
            {
                phenotypes[i].Offset = i * genomeLength;
                phenotypes[i].Fitness = -errors[i];
                sumFitness += phenotypes[i].Fitness;
            }
            avgFitness = sumFitness / populationSize;

            // Ranking by fitness
            Array.Sort(phenotypes, new IndividualComparer());
            maxFitness = Math.Max(maxFitness, phenotypes[0].Fitness);

            // Crossingover and mutation of non-elite individuals
            int elites = Convert.ToInt32(populationSize * eliteRatio);
            for (int parentA = elites; parentA < populationSize; ++parentA)
            {
                int parentB = randomGenerator.Next() % elites;
                //int crossPoint = randomGenerator.Next() % (genomeLength - 1) + 1;
                int crossPoint1 = randomGenerator.Next() % (genomeLength - 2) + 1;
                int crossPoint2 = randomGenerator.Next() % (genomeLength - crossPoint1 - 1) + crossPoint1 + 1;
                if (randomGenerator.Next() % 2 == 0)
                {
                    for (int i = 0; i < crossPoint1; ++i)
                    {
                        genotypes[phenotypes[parentA].Offset + i] = genotypes[phenotypes[parentB].Offset + i];
                    }
                    for (int i = crossPoint2; i < genomeLength; ++i)
                    {
                        genotypes[phenotypes[parentA].Offset + i] = genotypes[phenotypes[parentB].Offset + i];
                    }
                }
                else
                {
                    for (int i = crossPoint1; i < crossPoint2; ++i)
                    {
                        genotypes[phenotypes[parentA].Offset + i] = genotypes[phenotypes[parentB].Offset + i];
                    }
                }
                int mutations = Convert.ToInt32(genomeLength * mutationRatio);
                for (int i = 0; i < mutations; ++i)
                {
                    int index = randomGenerator.Next() % genomeLength;
                    genotypes[phenotypes[parentA].Offset + index] = Convert.ToSingle(randomGenerator.NextDouble()) * 2 - 1;
                }
            }
        }

        public void PredictTimeSeries(int size)
        {
            float[] genome = new float[genomeLength];
            Array.Copy(genotypes, phenotypes[0].Offset, genome, 0, genome.Length);
            float[] tmpInputTimeSeries = new float[inputData.Length + size];
            Array.Copy(inputData, tmpInputTimeSeries, inputData.Length);
            float[] errors;
            computeDevice.ProcessTimeSeries(genome, indDesc, tmpInputTimeSeries, out outputData, out errors);
        }

        public void PredictDataSet(float[] predictedTimeSeries)
        {
            float[] genome = new float[genomeLength];
            Array.Copy(genotypes, phenotypes[0].Offset, genome, 0, genome.Length);
            float[] tmpInputTimeSeries = new float[inputData.Length + predictedTimeSeries.Length];
            Array.Copy(inputData, tmpInputTimeSeries, inputData.Length);
            Array.Copy(predictedTimeSeries, 0, tmpInputTimeSeries, inputData.Length, predictedTimeSeries.Length);
            float[] errors;
            computeDevice.ProcessDataSet(genome, indDesc, tmpInputTimeSeries, out outputData, out errors);
        }
    }
}
