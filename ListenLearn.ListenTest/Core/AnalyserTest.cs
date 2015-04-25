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
    public class AnalyserTest
    {
        [TestCase(5.25, 5)]
        [TestCase(5, 5)]
        [TestCase(10.25, 10)]
        public void AnalyserTest_AnalyseSin(double inputFrequency, int expectedPeak)
        {
            var analyser = new AforgeFftAnalyser();
            var input = new double[64];

            RenderSin(input, inputFrequency, 0.5, 0);
            var output = analyser.Analyse(input);
            ChartPrinter.PrintChart(input, 10);
            ChartPrinter.PrintChart(input, 10);
            PrintArray(output);
            Assert.AreEqual(expectedPeak, AnalyseUtils.GetPeakElement(output));
        }

        [TestCase(@"Resources\Piano\C4.44100.sample", 1024, 243, 263)]
        [TestCase(@"Resources\Piano\D4.44100.sample", 1024, 274, 294)]
        [TestCase(@"Resources\Piano\E4.44100.sample", 1024, 308, 328)]
        [TestCase(@"Resources\Piano\E4.44100.sample", 512, 298, 338)]
        public void AudioSampleLoaderTest_FindPeakFrequency(string file, int samples, int expectedLower, int expectedUpper)
        {
            var fullPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, file);
            var pcmParser = new PcmParser();
            byte[] bytes = File.ReadAllBytes(fullPath);
            pcmParser.Parse(bytes, samples);

            var analyser = new AforgeFftAnalyser();
            var output = analyser.Analyse(pcmParser.data);
            ChartPrinter.PrintChart(output, 20);
            //PrintArray(output);
            var peakElement = AnalyseUtils.GetPeakElement(output);
            int peakFrequency = analyser.GetFrequency(peakElement, 44100, samples);
            AssertBetween(expectedLower, expectedUpper, peakFrequency);
        }

        private void AssertBetween(int lower, int upper, int value)
        {
            if (value < lower)
            {
                Assert.AreEqual(lower, value);
            }
            else if (value > upper)
            {
                Assert.AreEqual(upper, value);
            }
        }

        private void RenderSin(double[] input, double frequency, double amplitude, double offset)
        {
            for (var i = 0; i < input.Length; i++)
            {
                input[i] += amplitude*Math.Sin((frequency*Math.PI*2*i)/input.Length + offset);
            }
        }


        private void PrintArray(double[] output)
        {
            foreach (var item in output)
            {
                Console.WriteLine(item);
            }
        }
    }
}