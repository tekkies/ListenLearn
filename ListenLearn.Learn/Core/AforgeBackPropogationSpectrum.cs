using System;
using System.Dynamic;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace ListenLearn.Learn.Core
{
    public class AforgeBackPropogationSpectrum : AforgeBackPropogation
    {
        public AforgeBackPropogationSpectrum(int inputCount, int outputCount)
        {
            activationNetwork = new ActivationNetwork(
                new SigmoidFunction(2),
                inputCount,
                outputCount);
            momentum = 0;
            maxAttempts = 8;
            epochSize = 20;
            epochsPerSprint = 10;
            maxEpochsPerAttempt = 10000;
        }
    }
}