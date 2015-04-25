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
        [TestCase(@"Resources\2015-04-24--07-32-07-740.A.44100.sample")]
        public void AudioSampleLoaderTest_ExtractWaveform(string file)
        {
            var fullPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, file);
            var pcmParser = new PCMParser();
            byte[] bytes = File.ReadAllBytes(fullPath);
            pcmParser.Parse(bytes, 512);

            var analyser = new AforgeFftAnalyser();
            var output = analyser.Analyse(pcmParser.data);
            ChartPrinter.PrintChart(output, 20);
        }
    }
}