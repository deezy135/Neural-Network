using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using OpenCLNet;
using System.IO;
using System.Runtime.InteropServices;

namespace GeneticAlgorithm
{
    class ComputeDevice
    {
        private Context         context;
        private CommandQueue    queue;
        private Kernel          kernelForTimeSeries;
        private Kernel          kernelForDataSets;
        private List<Device>    devices;

        private string processDataSetFile = @"
__kernel void KernelFunction(
    __global const float* weights,

    int weightsCount,

    int weightsPerNet,

    int inputLayerSize,

    int outputLayerSize,

    int layersCount,
    __global const int* layers,

    int dataSetSize,
    __global const float* inputDS,
    __global float* outputDS,
    __global float* errors,
    __local float* bufferInput,
    __local float* bufferOutput
) {

    int inputDSPassed = 0;
        int iJob = get_global_id(0);
	if (iJob >= weightsCount / weightsPerNet) return;

	int setSize = inputLayerSize + outputLayerSize;
        int totalSets = dataSetSize / setSize;

        float tmpError = 0.0f;
	for (int iSet = 0; iSet<totalSets; ++iSet) {

		for (int i = 0; i<inputLayerSize; ++i) {
			bufferInput[i] = inputDS[inputDSPassed + i];
		}

    int prevLayerSize = inputLayerSize;
    int weightsPassed = iJob * weightsPerNet;
    bufferInput[prevLayerSize] = 1.0f;
		++prevLayerSize;
		for (int i = 0; i<layersCount; ++i) {

			int layerSize = layers[i];
			for (int iNode = 0; iNode<layerSize; ++iNode) {
				float acc = 0.0f;
				for (int iInput = 0; iInput<prevLayerSize; ++iInput) {
					acc += weights[weightsPassed + iNode * prevLayerSize + iInput] * bufferInput[iInput];
}
bufferOutput[iNode] = acc;
			}

			for (int iNode = 0; iNode<layerSize; ++iNode) {
				bufferInput[iNode] = bufferOutput[iNode];
			}

			weightsPassed += prevLayerSize* layerSize;
prevLayerSize = layerSize;
			bufferInput[prevLayerSize] = 1.0f;
			++prevLayerSize;
		}

		for (int iNode = 0; iNode<outputLayerSize; ++iNode) {
			float acc = 0.0f;
			for (int iInput = 0; iInput<prevLayerSize; ++iInput) {
				acc += weights[weightsPassed + iNode * prevLayerSize + iInput] * bufferInput[iInput];
			}
			acc = tanh(acc);
outputDS[iNode * totalSets + iSet] = acc;
			float curError = (inputDS[inputDSPassed + inputLayerSize + iNode] - acc);
tmpError += curError* curError;
		}
		weightsPassed += prevLayerSize* outputLayerSize;

inputDSPassed += setSize;
	}
	errors[iJob] = tmpError / outputLayerSize;

}
            ";
        private string processPopulationFile = @"
__kernel void KernelFunction(
	__global const float* weights,
	int weightsCount,
	int weightsPerNet,
	int inputLayerSize,
	int outputLayerSize,
	int layersCount,
	__global const int* layers, 
	int timeSeriesSize,
	__global const float* inputTS, 
	__global float* outputTS,
	__global float* errors,
	__local float* bufferTS,
	__local float* bufferInput, 
	__local float* bufferOutput
) {
	int inputTSPassed = 0;
	int iJob = get_global_id(0);
	if (iJob >= weightsCount / weightsPerNet) return;
	for (int iSeries = 0; iSeries < inputLayerSize; ++iSeries) {
		bufferTS[iSeries] = inputTS[iSeries];
	}
	for (int iSeries = inputLayerSize; iSeries < timeSeriesSize; ++iSeries) {

		for (int i = 0; i < inputLayerSize; ++i) {
			bufferInput[i] = bufferTS[inputTSPassed + i];
		}

		int prevLayerSize = inputLayerSize;
		int weightsPassed = iJob * weightsPerNet;
		bufferInput[prevLayerSize] = 1.0f;
		++prevLayerSize;
		for (int i = 0; i < layersCount; ++i) {

			int layerSize = layers[i];
			for (int iNode = 0; iNode < layerSize; ++iNode) {
				float acc = 0.0f;
				for (int iInput = 0; iInput < prevLayerSize; ++iInput) {
					acc += weights[weightsPassed + iNode * prevLayerSize + iInput] * bufferInput[iInput];
				}
				bufferOutput[iNode] = acc;
			}

			for (int iNode = 0; iNode < layerSize; ++iNode) {
				bufferInput[iNode] = bufferOutput[iNode];
			}
			weightsPassed += prevLayerSize * layerSize;
			prevLayerSize = layerSize;
			bufferInput[prevLayerSize] = 1.0f;
			++prevLayerSize;
		}

		for (int iNode = 0; iNode < outputLayerSize; ++iNode) {
			float acc = 0.0f;
			for (int iInput = 0; iInput < prevLayerSize; ++iInput) {
				acc += weights[weightsPassed + iNode * prevLayerSize + iInput] * bufferInput[iInput];
			}
			bufferTS[iSeries + iNode] = tanh(acc);
		}
		weightsPassed += prevLayerSize * outputLayerSize;

		++inputTSPassed;
	}
	float tmpError = 0.0f;
	for (int iSeries = 0; iSeries < timeSeriesSize; ++iSeries) {
		float curError = (inputTS[iSeries] - bufferTS[iSeries]);
		tmpError += curError * curError;
		outputTS[iSeries] = bufferTS[iSeries];
	}
	errors[iJob] = tmpError;

}
";

        public ComputeDevice()
        {
            if (OpenCL.NumberOfPlatforms == 0)
                throw new Exception("OpenCL not available");

            devices = new List<Device>();
            for (int i = 0; i < OpenCL.NumberOfPlatforms; ++i)
            {
                devices.AddRange(OpenCL.GetPlatform(i).QueryDevices(DeviceType.ALL));
            }
            

            //Console.WriteLine("MaxWorkGroupSize: " + oclDevices[0].Max);
            //Console.WriteLine("MaxWorkItemDimensions: " + oclDevices[0].MaxWorkItemDimensions);
            //Console.WriteLine("MaxWorkItemSizes: " + oclDevices[0].MaxWorkItemSizes[0].ToString());
            //Console.WriteLine("MaxWorkItemSizes: " + oclDevices[0].MaxWorkItemSizes[1].ToString());
            //Console.WriteLine("MaxWorkItemSizes: " + oclDevices[0].MaxWorkItemSizes[2].ToString());
            // Set kernel arguments
            // kernel.SetArg(0, 1L);
            // kernel.SetArg(1, 3.141592654f);
            // Set argument with platform specific size
            // kernel.SetSizeTArg(1, 34);
            // Enqueue the kernel on a 10x10 block
            // IntPtr[] globalWorkSize = new IntPtr[2];
            // globalWorkSize[0] = (IntPtr)10;
            // globalWorkSize[1] = (IntPtr)10;
            // queue.EnqueueNDRangeKernel(kernel, 2, null, globalWorkSize, null);
            // Wait for all pending operations to complete
            // queue.Finish();
        }

        public void Initialize(int index)
        {
            // Query Devices, create a Context+Command Queue and compile a program
            context = devices[index].Platform.CreateDefaultContext();
            queue = context.CreateCommandQueue(devices[index], CommandQueueProperties.PROFILING_ENABLE);
            Console.WriteLine(devices[index].Vendor + " " + devices[index].Name);
            // Load and build source+create a kernel
            OpenCLNet.Program program = context.CreateProgramWithSource(processPopulationFile);
            //OpenCLNet.Program program = context.CreateProgramWithSource(File.ReadAllText(@"C:\Users\deezy\Source\Repos\Neural-Network\GraduateWork\GeneticAlgorithm\ProcessPopulation.cl"));
            try
            {
                program.Build();
            }
            catch
            {
                Console.WriteLine(program.GetBuildLog(devices[index]));
            }
            kernelForTimeSeries = program.CreateKernel("KernelFunction");
            
            OpenCLNet.Program programForDataSets = context.CreateProgramWithSource(processDataSetFile);
            //OpenCLNet.Program programForDataSets = context.CreateProgramWithSource(File.ReadAllText(@"C:\Users\deezy\Source\Repos\Neural-Network\GraduateWork\GeneticAlgorithm\ProcessDataSet.cl"));
            try
            {
                programForDataSets.Build();
            }
            catch
            {
                Console.WriteLine(programForDataSets.GetBuildLog(devices[index]));
            }
            kernelForDataSets = programForDataSets.CreateKernel("KernelFunction");
        }

        public void GetDevices(out string[] devicesNames)
        {
            devicesNames = new string[devices.Count];
            for (int i = 0; i < devicesNames.Length; ++i)
            {
                devicesNames[i] = devices[i].Name;
            }
        }

        public void ProcessDataSet(float[] weights, IndividualDesc desc, float[] inputDS, out float[] outputDS, out float[] errors)
        {
            int populationSize = weights.Length / desc.TotalWeights;
            int setSize = desc.InputLayerSize + desc.OutputLayerSize;
            int totalSets = inputDS.Length / setSize;
            outputDS = new float[totalSets * desc.OutputLayerSize];
            GCHandle weightsCHandle = GCHandle.Alloc(weights, GCHandleType.Pinned);
            int[] hiddenLayers = desc.HiddenLayersSizes.ToArray();
            GCHandle hiddenLayersCHandle = GCHandle.Alloc(hiddenLayers, GCHandleType.Pinned);
            GCHandle inputTimeSeriesCHandle = GCHandle.Alloc(inputDS, GCHandleType.Pinned);
            GCHandle outputTimeSeriesCHandle = GCHandle.Alloc(outputDS, GCHandleType.Pinned);
            errors = new float[populationSize];
            GCHandle errorsCHandle = GCHandle.Alloc(errors, GCHandleType.Pinned);
            Mem memWeights = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, weights.Length * sizeof(float), weightsCHandle.AddrOfPinnedObject());
            Mem memHiddenLayers = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, hiddenLayers.Length * sizeof(int), hiddenLayersCHandle.AddrOfPinnedObject());
            Mem memInputTS = context.CreateBuffer(MemFlags.READ_WRITE | MemFlags.COPY_HOST_PTR, inputDS.Length * sizeof(float), inputTimeSeriesCHandle.AddrOfPinnedObject());
            Mem memOutputTS = context.CreateBuffer(MemFlags.READ_WRITE, outputDS.Length * sizeof(float), outputTimeSeriesCHandle.AddrOfPinnedObject());
            Mem memErrors = context.CreateBuffer(MemFlags.READ_WRITE, populationSize * sizeof(float));

            int argIndex = 0;
            kernelForDataSets.SetArg(argIndex++, memWeights);
            kernelForDataSets.SetArg(argIndex++, weights.Length);
            kernelForDataSets.SetArg(argIndex++, desc.TotalWeights);
            kernelForDataSets.SetArg(argIndex++, desc.InputLayerSize);
            kernelForDataSets.SetArg(argIndex++, desc.OutputLayerSize);
            kernelForDataSets.SetArg(argIndex++, hiddenLayers.Length);
            kernelForDataSets.SetArg(argIndex++, memHiddenLayers);
            kernelForDataSets.SetArg(argIndex++, inputDS.Length);
            kernelForDataSets.SetArg(argIndex++, memInputTS);
            kernelForDataSets.SetArg(argIndex++, memOutputTS);
            kernelForDataSets.SetArg(argIndex++, memErrors);
            kernelForDataSets.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);
            kernelForDataSets.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);


            IntPtr[] globalWorkSize = new IntPtr[1];
            globalWorkSize[0] = (IntPtr)populationSize;
            IntPtr[] localWorkSize = new IntPtr[1];
            localWorkSize[0] = (IntPtr)1;
            queue.EnqueueNDRangeKernel(kernelForDataSets, 1, null, globalWorkSize, localWorkSize);
            queue.Finish();
            queue.EnqueueReadBuffer(memOutputTS, true, 0, outputDS.Length * sizeof(float), outputTimeSeriesCHandle.AddrOfPinnedObject());
            queue.EnqueueReadBuffer(memErrors, true, 0, errors.Length * sizeof(float), errorsCHandle.AddrOfPinnedObject());
        }
        public void ProcessIndividual(float[] weights, IndividualDesc desc, float[] inputTS, out float[] outputTS)
        {
            int populationSize = weights.Length / desc.TotalWeights;
            outputTS = new float[inputTS.Length];
            GCHandle weightsCHandle = GCHandle.Alloc(weights, GCHandleType.Pinned);
            int[] hiddenLayers = desc.HiddenLayersSizes.ToArray();
            GCHandle hiddenLayersCHandle = GCHandle.Alloc(hiddenLayers, GCHandleType.Pinned);
            GCHandle inputTimeSeriesCHandle = GCHandle.Alloc(inputTS, GCHandleType.Pinned);
            GCHandle outputTimeSeriesCHandle = GCHandle.Alloc(outputTS, GCHandleType.Pinned);
            Mem memWeights = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, weights.Length * sizeof(float), weightsCHandle.AddrOfPinnedObject());
            Mem memHiddenLayers = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, hiddenLayers.Length * sizeof(int), hiddenLayersCHandle.AddrOfPinnedObject());
            Mem memInputTS = context.CreateBuffer(MemFlags.READ_WRITE | MemFlags.COPY_HOST_PTR, inputTS.Length * sizeof(float), inputTimeSeriesCHandle.AddrOfPinnedObject());
            Mem memOutputTS = context.CreateBuffer(MemFlags.READ_WRITE, inputTS.Length * sizeof(float), outputTimeSeriesCHandle.AddrOfPinnedObject());
            Mem memErrors = context.CreateBuffer(MemFlags.READ_WRITE, populationSize * sizeof(float));
            
            int argIndex = 0;
            kernelForTimeSeries.SetArg(argIndex++, memWeights);
            kernelForTimeSeries.SetArg(argIndex++, weights.Length);
            kernelForTimeSeries.SetArg(argIndex++, desc.TotalWeights);
            kernelForTimeSeries.SetArg(argIndex++, desc.InputLayerSize);
            kernelForTimeSeries.SetArg(argIndex++, desc.OutputLayerSize);
            kernelForTimeSeries.SetArg(argIndex++, hiddenLayers.Length);
            kernelForTimeSeries.SetArg(argIndex++, memHiddenLayers);
            kernelForTimeSeries.SetArg(argIndex++, inputTS.Length);
            kernelForTimeSeries.SetArg(argIndex++, memInputTS);
            kernelForTimeSeries.SetArg(argIndex++, memOutputTS);
            kernelForTimeSeries.SetArg(argIndex++, memErrors);
            kernelForTimeSeries.SetArg(argIndex++, (IntPtr)(inputTS.Length * sizeof(float)), (IntPtr)null);
            kernelForTimeSeries.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);
            kernelForTimeSeries.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);


            IntPtr[] globalWorkSize = new IntPtr[1];
            globalWorkSize[0] = (IntPtr)1;
            IntPtr[] localWorkSize = new IntPtr[1];
            localWorkSize[0] = (IntPtr)1;
            queue.EnqueueNDRangeKernel(kernelForTimeSeries, 1, null, globalWorkSize, localWorkSize);
            queue.Finish();
            float[] errors = new float[populationSize];
            GCHandle errorsCHandle = GCHandle.Alloc(errors, GCHandleType.Pinned);
            queue.EnqueueReadBuffer(memOutputTS, true, 0, outputTS.Length * sizeof(float), outputTimeSeriesCHandle.AddrOfPinnedObject());
            queue.EnqueueReadBuffer(memErrors, true, 0, errors.Length * sizeof(float), errorsCHandle.AddrOfPinnedObject());
        }

        public void ProcessPopulation(float[] weights, IndividualDesc desc, float[] inputTS, out float[] errors)
        {
            //ErrorCode errorCode;
            int populationSize = weights.Length / desc.TotalWeights;
            GCHandle weightsCHandle = GCHandle.Alloc(weights, GCHandleType.Pinned);
            int[] hiddenLayers = desc.HiddenLayersSizes.ToArray();
            GCHandle hiddenLayersCHandle = GCHandle.Alloc(hiddenLayers, GCHandleType.Pinned);
            GCHandle timeSeriesCHandle = GCHandle.Alloc(inputTS, GCHandleType.Pinned);
            errors = new float[populationSize];
            GCHandle errorsCHandle = GCHandle.Alloc(errors, GCHandleType.Pinned);
            Mem memWeights = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, weights.Length * sizeof(float), weightsCHandle.AddrOfPinnedObject());
            Mem memHiddenLayers = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, hiddenLayers.Length * sizeof(int), hiddenLayersCHandle.AddrOfPinnedObject());
            Mem memInputTS = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, inputTS.Length * sizeof(float), timeSeriesCHandle.AddrOfPinnedObject());
            Mem memOutputTS = context.CreateBuffer(MemFlags.READ_WRITE, inputTS.Length * sizeof(float));
            Mem memErrors = context.CreateBuffer(MemFlags.READ_WRITE, errors.Length * sizeof(float));

            int argIndex = 0;
            kernelForTimeSeries.SetArg(argIndex++, memWeights);
            kernelForTimeSeries.SetArg(argIndex++, weights.Length);
            kernelForTimeSeries.SetArg(argIndex++, desc.TotalWeights);
            kernelForTimeSeries.SetArg(argIndex++, desc.InputLayerSize);
            kernelForTimeSeries.SetArg(argIndex++, desc.OutputLayerSize);
            kernelForTimeSeries.SetArg(argIndex++, hiddenLayers.Length);
            kernelForTimeSeries.SetArg(argIndex++, memHiddenLayers);
            kernelForTimeSeries.SetArg(argIndex++, inputTS.Length);
            kernelForTimeSeries.SetArg(argIndex++, memInputTS);
            kernelForTimeSeries.SetArg(argIndex++, memOutputTS);
            kernelForTimeSeries.SetArg(argIndex++, memErrors);
            kernelForTimeSeries.SetArg(argIndex++, (IntPtr)(inputTS.Length * sizeof(float)), (IntPtr)null);
            kernelForTimeSeries.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);
            kernelForTimeSeries.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);
            

            IntPtr[] globalWorkSize = new IntPtr[1];
            globalWorkSize[0] = (IntPtr)populationSize;
            IntPtr[] localWorkSize = new IntPtr[1];
            localWorkSize[0] = (IntPtr)1;
            queue.EnqueueNDRangeKernel(kernelForTimeSeries, 1, null, globalWorkSize, localWorkSize);
            queue.Finish();

            //float[] outputTS = new float[inputTS.Length];
            //GCHandle outputTSCHandle = GCHandle.Alloc(outputTS, GCHandleType.Pinned);


            //queue.EnqueueReadBuffer(memOutputTS, true, 0, inputTS.Length * sizeof(float), outputTSCHandle.AddrOfPinnedObject());
            //queue.Finish();
            queue.EnqueueReadBuffer(memErrors, true, 0, errors.Length * sizeof(float), errorsCHandle.AddrOfPinnedObject());
            queue.Finish();
        }
    }
}
