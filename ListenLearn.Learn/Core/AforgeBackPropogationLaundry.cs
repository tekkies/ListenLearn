using System;
using System.Dynamic;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace ListenLearn.Learn.Core
{
    public class AforgeBackPropogationLaundry : AforgeBackPropogation
    {
        public AforgeBackPropogationLaundry(int l0, int l1, int l2)
        {
            activationNetwork = new ActivationNetwork(
                new SigmoidFunction(2),
                l0,
                l1,
                l2);
            momentum = 0;
            maxAttempts = 8;
            epochSize = 20;
            epochsPerSprint = 10;
            maxEpochsPerAttempt = 10000;
        }
    }
}