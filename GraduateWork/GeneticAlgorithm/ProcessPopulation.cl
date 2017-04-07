
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