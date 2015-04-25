using System;
using System.IO;
using System.Linq;
using System.Text;
using ListenLearn.Listen.Core;
using ListenLearnTest.Core;
using NUnit.Framework;

namespace ListenLearn.ListenTest.Core
{
    [TestFixture]
    public class AudioSampleLoaderTest
    {
        [TestCase(@"Resources\Piano\2015-04-24--07-32-07-740.A.44100.sample", 1024)]
        [TestCase(@"Resources\Piano\2015-04-24--07-32-21-962.B.44100.sample", 1024)]
        [TestCase(@"Resources\Piano\2015-04-24--07-32-24-651.C.44100.sample", 1024)]
        [TestCase(@"Resources\Piano\2015-04-24--07-32-24-651.C.44100.sample", 512)]
        public void AudioSampleLoaderTest_ExtractWaveform(string file, int samples)
        {
            var fullPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, file);
            var pcmParser = new PCMParser();
            byte[] bytes = File.ReadAllBytes(fullPath);
            pcmParser.Parse(bytes, samples);

            var analyser = new AforgeFftAnalyser();
            var output = analyser.Analyse(pcmParser.data);
            ChartPrinter.PrintChart(output, 20);
        }
    }
}