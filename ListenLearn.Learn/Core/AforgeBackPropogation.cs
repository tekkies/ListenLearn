using System;
using System.Dynamic;
using System.IO;
using System.Text;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace ListenLearn.Learn.Core
{
    public class AforgeBackPropogation : Learner
    {
        protected ActivationNetwork activationNetwork;
        protected int maxAttempts;
        protected int epochSize;
        protected int epochsPerSprint;
        protected int maxEpochsPerAttempt;
        protected int totalEpochsThisAttempt;
        protected double momentum;
	    protected int attemptIndex;

        public bool Learn(Func<object, Sample> trainingExample, double targetError)
        {
            bool success = false;
            for (attemptIndex = 0; attemptIndex < maxAttempts; attemptIndex++)
            {
                if (TryLearning(trainingExample, targetError))
                {
                    System.Diagnostics.Debug.WriteLine(String.Format("Learned on attempt {0} after {1} epochs.", attemptIndex + 1, totalEpochsThisAttempt));
                    success = true;
                    break;
                }
            }
            return success;
        }

        private bool TryLearning(Func<object, Sample> trainingExample, double targetError)
        {
            activationNetwork.Randomize();
            var teacher = new BackPropagationLearning(activationNetwork);
            teacher.Momentum = momentum;
            totalEpochsThisAttempt = 0;
            while (totalEpochsThisAttempt < maxEpochsPerAttempt)
            {
                var sprintError = Sprint(trainingExample, epochsPerSprint, teacher, ref totalEpochsThisAttempt);
                double epochError = sprintError/epochsPerSprint;
                if (epochError < targetError)
                    return true;
            }
            return false;
        }

        private double Sprint(Func<object, Sample> trainingExample, int epochsPerSprint, BackPropagationLearning teacher,
            ref int totalEpochsThisSprint)
        {
            double sprintError = 0;
            for (int sampleIndex = 0; sampleIndex < (epochSize*epochsPerSprint); sampleIndex++)
            {
                var example = trainingExample.Invoke(null);
                sprintError += teacher.Run(example.input, example.output);
            }
            totalEpochsThisSprint += epochsPerSprint;
            return sprintError;
        }

        public double[] Compute(double[] input)
        {
            return activationNetwork.Compute(input);
        }

	    public virtual string ToCsv()
	    {
			
		    return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, \"{6}\"",
			    attemptIndex,
			    totalEpochsThisAttempt,
			    momentum,
			    maxAttempts,
			    maxEpochsPerAttempt,
			    epochSize,
				GetNetworkConfiguration());
	    }

	    private String GetNetworkConfiguration()
	    {
			StringBuilder configuration=new StringBuilder();
		    configuration.Append(activationNetwork.InputsCount);
		    foreach (var layer in activationNetwork.Layers)
		    {
				configuration.Append("-");
			    configuration.Append(layer.Neurons.Length);
		    }
		    return configuration.ToString();
	    }
    }
}