using System;

namespace ListenLearn.Learn.Core
{
    public interface Learner
    {
        void Learn(Func<object, Sample> trainingExample, double targetError);
        double[] Compute(double[] input);
    }
}