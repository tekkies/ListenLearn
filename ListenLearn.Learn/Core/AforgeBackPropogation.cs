using System;
using System.Dynamic;
using AForge.Neuro;
using AForge.Neuro.Learning;

namespace ListenLearn.Learn.Core
{
    public class AforgeBackPropogation : Learner
    {
        private readonly ActivationNetwork activationNetwork;

        public AforgeBackPropogation()
        {
            activationNetwork = new ActivationNetwork(
                new SigmoidFunction(2),
                2,
                2,
                1);
        }

        public bool Learn(Func<object, Sample> trainingExample, double targetError)
        {

            const int maxAttempts = 8;
            const int epochSize = 4;
            const int epochsPerCourse=10;
            const int maxEpochsPerCourse = 2000;

            for (int attemptIndex = 0; attemptIndex < maxAttempts; attemptIndex++)
            {

                activationNetwork.Randomize();
                var teacher = new BackPropagationLearning(activationNetwork);


                int totalEpochsThisCourse = 0;
                while (totalEpochsThisCourse < maxEpochsPerCourse)
                {
                    double courseError = 0;
                    for (int sampleIndex = 0; sampleIndex < (epochSize*epochsPerCourse); sampleIndex++)
                    {
                        var example = trainingExample.Invoke(null);
                        courseError += teacher.Run(example.input, example.output);
                    }
                    totalEpochsThisCourse += epochsPerCourse;


                    double epochError = courseError/epochsPerCourse;
                    //System.Diagnostics.Debug.WriteLine(String.Format("Epoch {0} Error={1}", totalEpochsThisCourse, epochError));

                    if (epochError < targetError)
                        return true;
                }
            }
            return false;
        }

        public double[] Compute(double[] input)
        {
            return activationNetwork.Compute(input);
        }
    }
}