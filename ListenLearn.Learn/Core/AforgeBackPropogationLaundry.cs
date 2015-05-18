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

        public AforgeBackPropogationLaundry(int inputs, int outputs, int epochSize, int l1Nodes)
        {
	        networkConfiguration = new List<int>(){ l1Nodes, outputs};
	        activationNetwork = new ActivationNetwork(
                new SigmoidFunction(2),
                inputs, 
				networkConfiguration.ToArray());
            momentum = 0.1;
            maxAttempts = 1;
			this.epochSize = epochSize;
            epochsPerSprint = 1;
            maxEpochsPerAttempt = 20000;        
		}
    }
}