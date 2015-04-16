using System;
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

        public void Learn(Func<object, Sample> trainingExample, double targetError)
        {
            var teacher = new BackPropagationLearning(activationNetwork);
            var error = targetError*2;
            while (error > targetError)
            {
                var example = trainingExample.Invoke(null);
                error = teacher.Run(example.input, example.output);
            }
        }

        public double[] Compute(double[] input)
        {
            return activationNetwork.Compute(input);
        }
    }
}