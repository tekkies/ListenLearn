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
        [Ignore]
        public void LearnerTest_Xor(double a, double b, double expected)
        {
            Learner learner;
        }
    }
}
