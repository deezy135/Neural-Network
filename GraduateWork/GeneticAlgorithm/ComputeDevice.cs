using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCLNet;
using System.IO;
using System.Runtime.InteropServices;

namespace GeneticAlgorithm
{
    class ComputeDevice
    {
        private Context         context;
        private CommandQueue    queue;
        private Kernel          kernel;

        public ComputeDevice()
        {
            if (OpenCL.NumberOfPlatforms == 0)
                throw new Exception("OpenCL not available");

            // Query Devices, create a Context+Command Queue and compile a program
            Platform p = OpenCL.GetPlatform(0);
            Device[] oclDevices = p.QueryDevices(DeviceType.ALL);
            context = p.CreateDefaultContext();
            queue = context.CreateCommandQueue(oclDevices[0], CommandQueueProperties.PROFILING_ENABLE);
            Console.WriteLine(oclDevices[0].Vendor + " " + oclDevices[0].Name);
            // Load and build source+create a kernel
            OpenCLNet.Program program = context.CreateProgramWithSource(File.ReadAllText(@"C:\Users\deezy\Documents\visual studio 2015\Projects\NeuralNetwork\GeneticNN_cs\ProcessPopulation.cl"));
            try
            {
                program.Build();
            }
            catch
            {
                Console.WriteLine(program.GetBuildLog(oclDevices[0]));
            }

            //Console.WriteLine("MaxWorkGroupSize: " + oclDevices[0].Max);
            //Console.WriteLine("MaxWorkItemDimensions: " + oclDevices[0].MaxWorkItemDimensions);
            //Console.WriteLine("MaxWorkItemSizes: " + oclDevices[0].MaxWorkItemSizes[0].ToString());
            //Console.WriteLine("MaxWorkItemSizes: " + oclDevices[0].MaxWorkItemSizes[1].ToString());
            //Console.WriteLine("MaxWorkItemSizes: " + oclDevices[0].MaxWorkItemSizes[2].ToString());
            kernel = program.CreateKernel("KernelFunction");
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
            kernel.SetArg(argIndex++, memWeights);
            kernel.SetArg(argIndex++, weights.Length);
            kernel.SetArg(argIndex++, desc.TotalWeights);
            kernel.SetArg(argIndex++, desc.InputLayerSize);
            kernel.SetArg(argIndex++, desc.OutputLayerSize);
            kernel.SetArg(argIndex++, hiddenLayers.Length);
            kernel.SetArg(argIndex++, memHiddenLayers);
            kernel.SetArg(argIndex++, inputTS.Length);
            kernel.SetArg(argIndex++, memInputTS);
            kernel.SetArg(argIndex++, memOutputTS);
            kernel.SetArg(argIndex++, memErrors);
            kernel.SetArg(argIndex++, (IntPtr)(inputTS.Length * sizeof(float)), (IntPtr)null);
            kernel.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);
            kernel.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);


            IntPtr[] globalWorkSize = new IntPtr[1];
            globalWorkSize[0] = (IntPtr)1;
            IntPtr[] localWorkSize = new IntPtr[1];
            localWorkSize[0] = (IntPtr)1;
            queue.EnqueueNDRangeKernel(kernel, 1, null, globalWorkSize, localWorkSize);
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
            kernel.SetArg(argIndex++, memWeights);
            kernel.SetArg(argIndex++, weights.Length);
            kernel.SetArg(argIndex++, desc.TotalWeights);
            kernel.SetArg(argIndex++, desc.InputLayerSize);
            kernel.SetArg(argIndex++, desc.OutputLayerSize);
            kernel.SetArg(argIndex++, hiddenLayers.Length);
            kernel.SetArg(argIndex++, memHiddenLayers);
            kernel.SetArg(argIndex++, inputTS.Length);
            kernel.SetArg(argIndex++, memInputTS);
            kernel.SetArg(argIndex++, memOutputTS);
            kernel.SetArg(argIndex++, memErrors);
            kernel.SetArg(argIndex++, (IntPtr)(inputTS.Length * sizeof(float)), (IntPtr)null);
            kernel.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);
            kernel.SetArg(argIndex++, (IntPtr)(desc.MaxLayerSize * sizeof(float)), (IntPtr)null);
            

            IntPtr[] globalWorkSize = new IntPtr[1];
            globalWorkSize[0] = (IntPtr)populationSize;
            IntPtr[] localWorkSize = new IntPtr[1];
            localWorkSize[0] = (IntPtr)1;
            queue.EnqueueNDRangeKernel(kernel, 1, null, globalWorkSize, localWorkSize);
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
