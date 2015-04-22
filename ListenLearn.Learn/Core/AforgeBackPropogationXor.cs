using System;
using System.Dynamic;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace ListenLearn.Learn.Core
{
    public class AforgeBackPropogationXor : AforgeBackPropogation
    {
        public AforgeBackPropogationXor()
        {
            activationNetwork = new ActivationNetwork(
                new SigmoidFunction(2),
                2,
                2,
                1);
            momentum = 0;
            maxAttempts = 8;
            epochSize = 4;
            epochsPerSprint = 10;
            maxEpochsPerAttempt = 3000;
        }
    }
}