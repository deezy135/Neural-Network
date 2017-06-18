using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using OpenCLNet;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace GeneticAlgorithm
{
    class ComputeDevice
    {

        private string processDataSetSource = @"
__kernel void KernelFunction(
	__global const float* weights,
	int weightsCount,
	int weightsPerNet,
	int inputLayerSize,
	int outputLayerSize,
	int layersCount,
	__global const int* layers,
    __global const int* actFuncs,
	int dataSetSize,
	__global const float* inputDS,
	__global float* outputDS,
	__global float* errors,
	__local float* bufferInput,
	__local float* bufferOutput
) {
//	int iJob = get_global_id(0);
//	if (iJob >= weightsCount / weightsPerNet) return;

int jobs = weightsCount / weightsPerNet;
for (int iJob = 0; iJob < jobs; ++iJob) {

	int setSize = inputLayerSize + outputLayerSize;
	int totalSets = dataSetSize / setSize;
	int inputDSPassed = 0;

	float tmpError = 0.0f;
	for (int iSet = 0; iSet < totalSets; ++iSet) {

		for (int i = 0; i < inputLayerSize; ++i) {
			bufferInput[i] = inputDS[inputDSPassed + i];
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
                switch (actFuncs[i]) {
                case 1:
                    bufferOutput[iNode] = 1.0f / (1.0f + exp(-acc));
                    break;
                case 2:
                    bufferOutput[iNode] = tanh(acc);
                    break;
                default:
				    bufferOutput[iNode] = acc;
                    break;
                }
				//bufferOutput[iNode] = acc;
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
            switch (actFuncs[layersCount]) {
            case 1:
                acc = 1.0f / (1.0f + exp(-acc));
                break;
            case 2:
                acc = tanh(acc);
                break;
            }
			//acc = tanh(acc);
			outputDS[iNode * totalSets + iSet] = acc;
			float curError = (inputDS[inputDSPassed + inputLayerSize + iNode] - acc);
			tmpError += curError * curError;
		}
		weightsPassed += prevLayerSize * outputLayerSize;

		inputDSPassed += setSize;
	}
	errors[iJob] = tmpError / outputLayerSize / totalSets;
//
}

}
";
        private string processTimeSeriesSource = @"
__kernel void KernelFunction(
	__global const float* weights,
	int weightsCount,
	int weightsPerNet,
	int inputLayerSize,
	int outputLayerSize,
	int layersCount,
	__global const int* layers, 
    __global const int* actFuncs,
	int timeSeriesSize,
	__global const float* inputTS, 
	__global float* outputTS,
	__global float* errors,
	__local float* bufferTS,
	__local float* bufferInput, 
	__local float* bufferOutput
) {
//	int iJob = get_global_id(0);
//	if (iJob >= weightsCount / weightsPerNet) return;
    
int jobs = weightsCount / weightsPerNet;
for (int iJob = 0; iJob < jobs; ++iJob) {

	int inputTSPassed = 0;
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
                switch (actFuncs[i]) {
                case 1:
                    bufferOutput[iNode] = 1.0f / (1.0f + exp(-acc));
                    break;
                case 2:
                    bufferOutput[iNode] = tanh(acc);
                    break;
                default:
				    bufferOutput[iNode] = acc;
                    break;
                }
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
            switch (actFuncs[layersCount]) {
            case 1:
                bufferTS[iSeries + iNode] = 1.0f / (1.0f + exp(-acc));
                break;
            case 2:
                bufferTS[iSeries + iNode] = tanh(acc);
                break;
            default:
				bufferTS[iSeries + iNode] = acc;
                break;
            }
			//bufferTS[iSeries + iNode] = tanh(acc);
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
	errors[iJob] = tmpError / timeSeriesSize;
//
}

}
";
        private Context context;
        private CommandQueue queue;
        private Kernel kernelForTimeSeries;
        private Kernel kernelForDataSets;
        private List<Device> devices;
        private int selectedDevice;
        private int workGroupSize;
        private float lastTimeResult;

        public ComputeDevice()
        {
            if (OpenCL.NumberOfPlatforms == 0)
                throw new Exception("OpenCL not available");
            devices = new List<Device>();
            for (int i = 0; i < OpenCL.NumberOfPlatforms; ++i)
            {
                devices.AddRange(OpenCL.GetPlatform(i).QueryDevices(DeviceType.ALL));
            }
            workGroupSize = 0;
            lastTimeResult = 0.0f;
        }

        public void Initialize(int index)
        {
            // Query Devices, create a Context+Command Queue and compile a program
            context = devices[index].Platform.CreateDefaultContext();
            queue = context.CreateCommandQueue(devices[index], CommandQueueProperties.PROFILING_ENABLE);
            Console.WriteLine(devices[index].Vendor + " " + devices[index].Name);
            // Load and build source+create a kernel
            OpenCLNet.Program programForTimeSeries = context.CreateProgramWithSource(processTimeSeriesSource);
            try
            {
                programForTimeSeries.Build();
            }
            catch
            {
                Console.WriteLine(programForTimeSeries.GetBuildLog(devices[index]));
            }
            kernelForTimeSeries = programForTimeSeries.CreateKernel("KernelFunction");
            
            OpenCLNet.Program programForDataSets = context.CreateProgramWithSource(processDataSetSource);
            try
            {
                programForDataSets.Build();
            }
            catch
            {
                Console.WriteLine(programForDataSets.GetBuildLog(devices[index]));
            }
            kernelForDataSets = programForDataSets.CreateKernel("KernelFunction");
            selectedDevice = index;
            workGroupSize = (int)devices[index].MaxWorkGroupSize;
        }

        public int GetMaxWorkGroupSize(int index)
        {
            return (int)devices[index].MaxWorkGroupSize;
        }
        public void SetWorkGroupSize(int size)
        {
            workGroupSize = Math.Min(size, workGroupSize);
        }
        public void GetDevices(out string[] devicesNames)
        {
            devicesNames = new string[devices.Count];
            for (int i = 0; i < devicesNames.Length; ++i)
            {
                devicesNames[i] = devices[i].Name.Trim(' ');
            }
        }

        public float GetLastTimeResult()
        {
            return lastTimeResult;
        }

        public void ProcessDataSet(float[] weights, IndividualDesc desc, float[] inputDS, out float[] outputDS, out float[] errors)
        {
            int populationSize                  = weights.Length / desc.TotalWeights;
            int setSize                         = desc.InputLayerSize + desc.OutputLayerSize;
            int totalSets                       = inputDS.Length / setSize;
            outputDS                            = new float[totalSets * desc.OutputLayerSize];
            GCHandle weightsCHandle             = GCHandle.Alloc(weights, GCHandleType.Pinned);
            int[] hiddenLayers                  = desc.HiddenLayersSizes.ToArray();
            GCHandle hiddenLayersCHandle        = GCHandle.Alloc(hiddenLayers, GCHandleType.Pinned);
            GCHandle actFuncsCHandle            = GCHandle.Alloc(desc.ActivationFunctions, GCHandleType.Pinned);
            GCHandle inputDataSetCHandle        = GCHandle.Alloc(inputDS, GCHandleType.Pinned);
            GCHandle outputDataSetCHandle       = GCHandle.Alloc(outputDS, GCHandleType.Pinned);
            errors                              = new float[populationSize];
            GCHandle errorsCHandle              = GCHandle.Alloc(errors, GCHandleType.Pinned);
            Mem memWeights      = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, weights.Length * sizeof(float), weightsCHandle.AddrOfPinnedObject());
            Mem memHiddenLayers = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, hiddenLayers.Length * sizeof(int), hiddenLayersCHandle.AddrOfPinnedObject());
            Mem memActFuncs     = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, desc.ActivationFunctions.Length * sizeof(int), actFuncsCHandle.AddrOfPinnedObject());
            Mem memInputDS      = context.CreateBuffer(MemFlags.READ_WRITE | MemFlags.COPY_HOST_PTR, inputDS.Length * sizeof(float), inputDataSetCHandle.AddrOfPinnedObject());
            Mem memOutputDS     = context.CreateBuffer(MemFlags.READ_WRITE, outputDS.Length * sizeof(float)/*, outputDataSetCHandle.AddrOfPinnedObject()*/);
            Mem memErrors       = context.CreateBuffer(MemFlags.READ_WRITE, populationSize * sizeof(float));

            int argIndex = 0;
            kernelForDataSets.SetArg(argIndex++, memWeights);
            kernelForDataSets.SetArg(argIndex++, weights.Length);
            kernelForDataSets.SetArg(argIndex++, desc.TotalWeights);
            kernelForDataSets.SetArg(argIndex++, desc.InputLayerSize);
            kernelForDataSets.SetArg(argIndex++, desc.OutputLayerSize);
            kernelForDataSets.SetArg(argIndex++, hiddenLayers.Length);
            kernelForDataSets.SetArg(argIndex++, memHiddenLayers);
            kernelForDataSets.SetArg(argIndex++, memActFuncs);
            kernelForDataSets.SetArg(argIndex++, inputDS.Length);
            kernelForDataSets.SetArg(argIndex++, memInputDS);
            kernelForDataSets.SetArg(argIndex++, memOutputDS);
            kernelForDataSets.SetArg(argIndex++, memErrors);
            kernelForDataSets.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);
            kernelForDataSets.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);


            IntPtr[] globalWorkSize     = new IntPtr[1];
            globalWorkSize[0] = (IntPtr)1;// populationSize;
            IntPtr[] localWorkSize      = new IntPtr[1];
            localWorkSize[0]            = (IntPtr)1;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            queue.EnqueueNDRangeKernel(kernelForDataSets, 1, null, globalWorkSize, localWorkSize);
            queue.Finish();
            lastTimeResult = sw.ElapsedMilliseconds / 1000.0f;
            sw.Stop();
            queue.EnqueueReadBuffer(memOutputDS, true, 0, outputDS.Length * sizeof(float), outputDataSetCHandle.AddrOfPinnedObject());
            queue.EnqueueReadBuffer(memErrors, true, 0, errors.Length * sizeof(float), errorsCHandle.AddrOfPinnedObject());
        }
        //public void ProcessIndividual(float[] weights, IndividualDesc desc, float[] inputTS, out float[] outputTS)
        //{
        //    int populationSize = weights.Length / desc.TotalWeights;
        //    outputTS = new float[inputTS.Length];
        //    GCHandle weightsCHandle = GCHandle.Alloc(weights, GCHandleType.Pinned);
        //    int[] hiddenLayers = desc.HiddenLayersSizes.ToArray();
        //    GCHandle hiddenLayersCHandle = GCHandle.Alloc(hiddenLayers, GCHandleType.Pinned);
        //    GCHandle inputTimeSeriesCHandle = GCHandle.Alloc(inputTS, GCHandleType.Pinned);
        //    GCHandle outputTimeSeriesCHandle = GCHandle.Alloc(outputTS, GCHandleType.Pinned);
        //    Mem memWeights = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, weights.Length * sizeof(float), weightsCHandle.AddrOfPinnedObject());
        //    Mem memHiddenLayers = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, hiddenLayers.Length * sizeof(int), hiddenLayersCHandle.AddrOfPinnedObject());
        //    Mem memInputTS = context.CreateBuffer(MemFlags.READ_WRITE | MemFlags.COPY_HOST_PTR, inputTS.Length * sizeof(float), inputTimeSeriesCHandle.AddrOfPinnedObject());
        //    Mem memOutputTS = context.CreateBuffer(MemFlags.READ_WRITE, inputTS.Length * sizeof(float), outputTimeSeriesCHandle.AddrOfPinnedObject());
        //    Mem memErrors = context.CreateBuffer(MemFlags.READ_WRITE, populationSize * sizeof(float));
            
        //    int argIndex = 0;
        //    kernelForTimeSeries.SetArg(argIndex++, memWeights);
        //    kernelForTimeSeries.SetArg(argIndex++, weights.Length);
        //    kernelForTimeSeries.SetArg(argIndex++, desc.TotalWeights);
        //    kernelForTimeSeries.SetArg(argIndex++, desc.InputLayerSize);
        //    kernelForTimeSeries.SetArg(argIndex++, desc.OutputLayerSize);
        //    kernelForTimeSeries.SetArg(argIndex++, hiddenLayers.Length);
        //    kernelForTimeSeries.SetArg(argIndex++, memHiddenLayers);
        //    kernelForTimeSeries.SetArg(argIndex++, inputTS.Length);
        //    kernelForTimeSeries.SetArg(argIndex++, memInputTS);
        //    kernelForTimeSeries.SetArg(argIndex++, memOutputTS);
        //    kernelForTimeSeries.SetArg(argIndex++, memErrors);
        //    kernelForTimeSeries.SetArg(argIndex++, (IntPtr)(inputTS.Length * sizeof(float)), (IntPtr)null);
        //    kernelForTimeSeries.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);
        //    kernelForTimeSeries.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);


        //    IntPtr[] globalWorkSize = new IntPtr[1];
        //    globalWorkSize[0] = (IntPtr)1;
        //    IntPtr[] localWorkSize = new IntPtr[1];
        //    localWorkSize[0] = (IntPtr)1;
        //    queue.EnqueueNDRangeKernel(kernelForTimeSeries, 1, null, globalWorkSize, localWorkSize);
        //    queue.Finish();
        //    float[] errors = new float[populationSize];
        //    GCHandle errorsCHandle = GCHandle.Alloc(errors, GCHandleType.Pinned);
        //    queue.EnqueueReadBuffer(memOutputTS, true, 0, outputTS.Length * sizeof(float), outputTimeSeriesCHandle.AddrOfPinnedObject());
        //    queue.EnqueueReadBuffer(memErrors, true, 0, errors.Length * sizeof(float), errorsCHandle.AddrOfPinnedObject());
        //}

        public void ProcessTimeSeries(float[] weights, IndividualDesc desc, float[] inputTS, out float[] outputTS, out float[] errors)
        {
            int populationSize                  = weights.Length / desc.TotalWeights;
            GCHandle weightsCHandle             = GCHandle.Alloc(weights, GCHandleType.Pinned);
            int[] hiddenLayers                  = desc.HiddenLayersSizes.ToArray();
            GCHandle hiddenLayersCHandle        = GCHandle.Alloc(hiddenLayers, GCHandleType.Pinned);
            GCHandle actFuncsCHandle            = GCHandle.Alloc(desc.ActivationFunctions, GCHandleType.Pinned);
            GCHandle inputTimeSeriesCHandle     = GCHandle.Alloc(inputTS, GCHandleType.Pinned);
            outputTS                            = new float[inputTS.Length];
            GCHandle outputTimeSeriesCHandle    = GCHandle.Alloc(outputTS, GCHandleType.Pinned);
            errors                              = new float[populationSize];
            GCHandle errorsCHandle              = GCHandle.Alloc(errors, GCHandleType.Pinned);
            Mem memWeights      = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, weights.Length * sizeof(float), weightsCHandle.AddrOfPinnedObject());
            Mem memHiddenLayers = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, hiddenLayers.Length * sizeof(int), hiddenLayersCHandle.AddrOfPinnedObject());
            Mem memActFuncs     = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, desc.ActivationFunctions.Length * sizeof(int), actFuncsCHandle.AddrOfPinnedObject());
            Mem memInputTS      = context.CreateBuffer(MemFlags.READ_ONLY | MemFlags.COPY_HOST_PTR, inputTS.Length * sizeof(float), inputTimeSeriesCHandle.AddrOfPinnedObject());
            Mem memOutputTS     = context.CreateBuffer(MemFlags.READ_WRITE, outputTS.Length * sizeof(float));
            Mem memErrors       = context.CreateBuffer(MemFlags.READ_WRITE, errors.Length * sizeof(float));

            int argIndex = 0;
            kernelForTimeSeries.SetArg(argIndex++, memWeights);
            kernelForTimeSeries.SetArg(argIndex++, weights.Length);
            kernelForTimeSeries.SetArg(argIndex++, desc.TotalWeights);
            kernelForTimeSeries.SetArg(argIndex++, desc.InputLayerSize);
            kernelForTimeSeries.SetArg(argIndex++, desc.OutputLayerSize);
            kernelForTimeSeries.SetArg(argIndex++, hiddenLayers.Length);
            kernelForTimeSeries.SetArg(argIndex++, memHiddenLayers);
            kernelForTimeSeries.SetArg(argIndex++, memActFuncs);
            kernelForTimeSeries.SetArg(argIndex++, inputTS.Length);
            kernelForTimeSeries.SetArg(argIndex++, memInputTS);
            kernelForTimeSeries.SetArg(argIndex++, memOutputTS);
            kernelForTimeSeries.SetArg(argIndex++, memErrors);
            kernelForTimeSeries.SetArg(argIndex++, (IntPtr)(inputTS.Length * sizeof(float)), (IntPtr)null);
            kernelForTimeSeries.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);
            kernelForTimeSeries.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);
            

            IntPtr[] globalWorkSize     = new IntPtr[1];
            globalWorkSize[0] = (IntPtr)1;//populationSize;
            IntPtr[] localWorkSize      = new IntPtr[1];
            localWorkSize[0]            = (IntPtr)1;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            queue.EnqueueNDRangeKernel(kernelForTimeSeries, 1, null, globalWorkSize, localWorkSize);
            queue.Finish();
            lastTimeResult = sw.ElapsedMilliseconds / 1000.0f;
            sw.Stop();
            queue.EnqueueReadBuffer(memOutputTS, true, 0, outputTS.Length * sizeof(float), outputTimeSeriesCHandle.AddrOfPinnedObject());
            queue.EnqueueReadBuffer(memErrors, true, 0, errors.Length * sizeof(float), errorsCHandle.AddrOfPinnedObject());
        }
    }
}
