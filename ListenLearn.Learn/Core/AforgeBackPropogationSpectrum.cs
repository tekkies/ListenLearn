﻿using System;
using System.Dynamic;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace ListenLearn.Learn.Core
{
    public class AforgeBackPropogationSpectrum : AforgeBackPropogation
    {
        public AforgeBackPropogationSpectrum(int inputCount, int outputCount, int epochSize)
        {
            activationNetwork = new ActivationNetwork(
                new SigmoidFunction(2),
                inputCount,
				10,
                outputCount);
            momentum = 0;
            maxAttempts = 8;
            this.epochSize = epochSize;
            epochsPerSprint = 10;
            maxEpochsPerAttempt = 20000;
        }
    }
}