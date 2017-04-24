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
        public IndividualDesc(int inputLayerSize, int outputLayerSize)
        {
            InputLayerSize = inputLayerSize;
            OutputLayerSize = outputLayerSize;
            HiddenLayersSizes = new List<int>();
            TotalWeights = 0;
            MaxLayerSize = 0;
            Updated = false;
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
        private float[] inputDataSet;
        private float[] outputDataSet;

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
            for (int i = 0; i < genotypes.Length; ++i) {
                genotypes[i] = Convert.ToSingle(randomGenerator.NextDouble()) * 2 - 1;
            }
            phenotypes = new Individual[populationSize];
            for (int i = 0; i < phenotypes.Length; ++i) {
                phenotypes[i].Fitness = 0;
                phenotypes[i].Offset = i * genomeLength;
            }

            inputDataSet = dataSet;
            
            computeDevice = device;

            maxFitness = -float.MaxValue;
            avgFitness = 0.0f;

            eliteRatio = 0.1f;
            mutationRatio = 0.05f;
        }

        public float[] GetOutputTimeSeries()
        {
            return outputDataSet;
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
                computeDevice.ProcessPopulation(genotypes, indDesc, inputDataSet, out errors);
            }
            else
            {
                computeDevice.ProcessDataSet(genotypes, indDesc, inputDataSet, out outputDataSet, out errors);
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

            // Get output timeseries with low error
            if (maxFitness < phenotypes[0].Fitness)
            {
                maxFitness = phenotypes[0].Fitness;
                //float[] genome = new float[genomeLength];
                //Array.Copy(genotypes, phenotypes[0].Offset, genome, 0, genome.Length);
                //computeDevice.ProcessIndividual(genome, indDesc, inputTimeSeries, out outputTimeSeries);
                
            }

            // Crossingover and mutation of non-elite individuals
            int elites = Convert.ToInt32(populationSize * eliteRatio);
            for (int parentA = elites; parentA < populationSize; ++parentA)
            {
                int parentB = randomGenerator.Next() % elites;
                int crossPoint = randomGenerator.Next() % (genomeLength - 1) + 1;
                if (randomGenerator.Next() % 2 == 0)
                {
                    for (int i = 0; i < crossPoint; ++i)
                    {
                        genotypes[phenotypes[parentA].Offset + i] = genotypes[phenotypes[parentB].Offset + i];
                    }
                }
                else
                {
                    for (int i = crossPoint; i < genomeLength; ++i)
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
            float[] tmpInputTimeSeries = new float[inputDataSet.Length + size];
            Array.Copy(inputDataSet, tmpInputTimeSeries, inputDataSet.Length);
            computeDevice.ProcessIndividual(genome, indDesc, tmpInputTimeSeries, out outputDataSet);
        }

        public void PredictDataSet(float[] predictedTimeSeries)
        {
            float[] genome = new float[genomeLength];
            Array.Copy(genotypes, phenotypes[0].Offset, genome, 0, genome.Length);
            float[] tmpInputTimeSeries = new float[inputDataSet.Length + predictedTimeSeries.Length];
            Array.Copy(inputDataSet, tmpInputTimeSeries, inputDataSet.Length);
            Array.Copy(predictedTimeSeries, 0, tmpInputTimeSeries, inputDataSet.Length, predictedTimeSeries.Length);
            float[] errors;
            computeDevice.ProcessDataSet(genome, indDesc, tmpInputTimeSeries, out outputDataSet, out errors);
        }
    }
}
