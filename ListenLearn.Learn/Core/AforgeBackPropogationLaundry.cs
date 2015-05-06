using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace ListenLearn.Learn.Core
{
    public class AforgeBackPropogationLaundry : AforgeBackPropogation
    {
	    private List<int> networkConfiguration;

        public AforgeBackPropogationLaundry(int l0, int l1, int l2, int epochSize)
        {
	        networkConfiguration = new List<int>(){l1,l2};
	        activationNetwork = new ActivationNetwork(
                new SigmoidFunction(2),
                l0, 
				networkConfiguration.ToArray());
            momentum = 0.1;
            maxAttempts = 8;
			this.epochSize = epochSize;
            epochsPerSprint = 1;
            maxEpochsPerAttempt = 5000;        
		}
    }
}