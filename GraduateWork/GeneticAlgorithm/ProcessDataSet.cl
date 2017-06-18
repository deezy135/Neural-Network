
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
			acc = tanh(acc);
			outputDS[iNode * totalSets + iSet] = acc;
			float curError = (inputDS[inputDSPassed + inputLayerSize + iNode] - acc);
			tmpError += curError * curError;
		}
		weightsPassed += prevLayerSize * outputLayerSize;

		inputDSPassed += setSize;
	}
	errors[iJob] = tmpError / outputLayerSize;// / totalSets;

}