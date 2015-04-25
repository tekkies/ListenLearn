using System.IO;
using ListenLearn.Listen.Core;
using ListenLearnTest.Core;
using NUnit.Framework;

namespace ListenLearn.ListenTest.Core
{
    [TestFixture]
    public class PcmParserTest
    {
        [TestCase(@"Resources\Piano\C4.44100.sample", 375, -110)]
        public void AudioSampleLoaderTest_ReadData(string file, int sampleNo, int expectedValue)
        {
            var fullPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, file);
            var pcmParser = new PcmParser();
            byte[] bytes = File.ReadAllBytes(fullPath);
            pcmParser.Parse(bytes, 1024);
            Assert.AreEqual(expectedValue, (double)pcmParser.data[sampleNo]);
        }

    }
}