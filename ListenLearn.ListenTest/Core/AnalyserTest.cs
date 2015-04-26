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
        public void AnalyserTest_AnalyseSin(double frequency, int expectedPeak)
        {
            var analyser = new AforgeFftAnalyser();
            var waveform = new double[64];

            WaveUtils.MixSinWave(waveform, frequency, 0.5, 0);
            var spectrum = analyser.Analyse(waveform);
            ChartPrinter.PrintChartWithAutoscale(waveform, 10);
            ChartPrinter.PrintChartWithAutoscale(waveform, 10);
            PrintArray(spectrum);
            Assert.AreEqual(expectedPeak, AnalyseUtils.GetPeakElement(spectrum));
        }

        [TestCase(@"C4.44100.sample", 2048, 243, 263)]
        [TestCase(@"D4.44100.sample", 1024, 274, 294)]
        [TestCase(@"E4.44100.sample", 1024, 308, 328)]
        [TestCase(@"E4.44100.sample", 512, 298, 338)]
        public void AudioSampleLoaderTest_FindPeakFrequency(string file, int samples, int expectedLower, int expectedUpper)
        {
            var fullPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, @"Resources\Piano", file);
            var pcmParser = new PcmParser();
            byte[] bytes = File.ReadAllBytes(fullPath);
            pcmParser.Parse(bytes, samples);
            var analyser = new AforgeFftAnalyser();

            var spectrum = analyser.Analyse(pcmParser.data);

            ChartPrinter.PrintChartWithAutoscale(spectrum, 20);
            var peakElement = AnalyseUtils.GetPeakElement(spectrum);
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


        private void PrintArray(double[] output)
        {
            foreach (var item in output)
            {
                Console.WriteLine(item);
            }
        }
    }
}