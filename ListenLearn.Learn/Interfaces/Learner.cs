using System;

namespace ListenLearn.Learn.Core
{
    public interface Learner
    {
        bool Learn(Func<object, Sample> trainingExample, double targetError);
        double[] Compute(double[] input);
	    string ToCsv();
    }
}