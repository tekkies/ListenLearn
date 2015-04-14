using ListenLearn.Listen.Core;
using NUnit.Framework;

namespace ListenLearnTest
{
    [TestFixture]
    public class AnalyserTest
    {
        [Test]
        public void AnalyserTest_CreateAnalyser()
        {
            var anlyser = new AforgeAnalyser();
        }
    }
}