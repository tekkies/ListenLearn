using System;
using System.Dynamic;
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

        public bool Learn(Func<object, Sample> trainingExample, double targetError)
        {
            for (int attemptIndex = 0; attemptIndex < maxAttempts; attemptIndex++)
            {
                if (TryLearning(trainingExample, targetError))
                {
                    System.Diagnostics.Debug.WriteLine(String.Format("Learned on attempt {0} after {1} epochs.", attemptIndex + 1, totalEpochsThisAttempt));
                    return true;
                }
            }
            return false;
        }

        private bool TryLearning(Func<object, Sample> trainingExample, double targetError)
        {
            activationNetwork.Randomize();
            var teacher = new BackPropagationLearning(activationNetwork);
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
    }
}