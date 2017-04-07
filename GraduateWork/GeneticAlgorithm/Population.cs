using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    struct IndividualDesc
    {
        public int InputLayerSize;
        public int OutputLayerSize;
        public List<int> HiddenLayersSizes;
        public int TotalWeights;
        public int MaxLayerSize;
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

        private float[] inputTimeSeries;
        private float[] outputTimeSeries;

        private ComputeDevice computeDevice;

        public Population(int size, IndividualDesc desc, ComputeDevice device, float[] timeSeries, int seed = 0)
        {
            populationSize = size;
            if (seed == 0) {
                randomGenerator = new Random();
            }
            else {
                randomGenerator = new Random(seed);
            }
            indDesc = desc;

            int totalWeights = 0;
            int maxLayerSize = Math.Max(desc.InputLayerSize, desc.OutputLayerSize);
            int prevLayerSize = indDesc.InputLayerSize;
            foreach (int layerSize in indDesc.HiddenLayersSizes) {
                totalWeights += (prevLayerSize + 1) * layerSize;
                maxLayerSize = (maxLayerSize < layerSize ? layerSize : maxLayerSize);
                prevLayerSize = layerSize;
            }
            totalWeights += (prevLayerSize + 1) * indDesc.OutputLayerSize;
            genomeLength = totalWeights;
            indDesc.TotalWeights = genomeLength;
            indDesc.MaxLayerSize = maxLayerSize + 1;

            genotypes = new float[populationSize * genomeLength];
            for (int i = 0; i < genotypes.Length; ++i) {
                genotypes[i] = Convert.ToSingle(randomGenerator.NextDouble()) * 2 - 1;
            }
            phenotypes = new Individual[populationSize];
            for (int i = 0; i < phenotypes.Length; ++i) {
                phenotypes[i].Fitness = 0;
                phenotypes[i].Offset = i * genomeLength;
            }

            inputTimeSeries = timeSeries;
            computeDevice = device;

            maxFitness = -float.MaxValue;
            avgFitness = 0.0f;

            eliteRatio = 0.1f;
            mutationRatio = 0.05f;
        }

        public float[] GetOutputTimeSeries()
        {
            return outputTimeSeries;
        }
        public float GetMaxFitness()
        {
            return maxFitness;
        }

        public void Epoch()
        {
            float[] errors;//= new float[populationSize];

            // Evaluation
            computeDevice.ProcessPopulation(genotypes, indDesc, inputTimeSeries, out errors);

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
                float[] genome = new float[genomeLength];
                Array.Copy(genotypes, phenotypes[0].Offset, genome, 0, genome.Length);
                computeDevice.ProcessIndividual(genome, indDesc, inputTimeSeries, out outputTimeSeries);
                
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
    }
}
