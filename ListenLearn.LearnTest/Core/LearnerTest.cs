using System;
using System.Collections;
using ListenLearn.Learn.Core;
using NUnit.Framework;

namespace ListenLearn.LearnTest.Core
{
    [TestFixture]
    public class LearnerTest
    {
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 0)]
        public void LearnerTest_Xor(double a, double b, double expected)
        {
            Learner learner = new AforgeBackPropogation();

            Sample[] samples =
            {
                new Sample(new double[] { 0, 0 }, new double[] { 0 }),
                new Sample(new double[] { 0, 1 }, new double[] { 1 }),
                new Sample(new double[] { 1, 0 }, new double[] { 1 }),
                new Sample(new double[] { 1, 1 }, new double[] { 0 })
            };
            
            Random random = new Random();
            const double errorTarget = 0.01;
            Assert.IsTrue(learner.Learn(o => samples[random.Next(0, samples.Length)], errorTarget), "Learned the ropes");

            var input = new double[] {a, b};
            var output = learner.Compute(input)[0];
            Assert.IsTrue(Math.Abs(expected - output) < 0.2, "Near enough" );
        }
    }
}
