using System;
using System.IO;
using ListenLearn.Learn.Core;
using ListenLearn.Listen.Core;
using ListenLearnTest.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace ListenLearn.LearnTest.Core
{
    [TestFixture]
    public class SpectrumLearnerTest
    {
        [TestCase]
        public void SpectrumLearnerTest_Learn()
        {
            int sampleLength = 16;
            List<Sample> samples = GenerateSamples(sampleLength);
            Learner learner = new AforgeBackPropogationSpectrum(sampleLength/2, 2, 3);
            Random random = new Random();
            const double errorTarget = 0.01;
            Assert.IsTrue(learner.Learn(o => samples[random.Next(0, samples.Count)], errorTarget), "Learned the ropes");
        }

        private static List<Sample> GenerateSamples(int sampleLegth)
        {
            var samples = new List<Sample>();
            double[] waveform;

            waveform = new double[sampleLegth];
            var analyser = new AforgeFftAnalyser();
            var spectrum = analyser.Analyse(waveform);
            ChartPrinter.PrintChartWithAutoscale(waveform, 10);
            ChartPrinter.PrintChartWithAutoscale(spectrum, 10);
            samples.Add(new Sample(spectrum, new double[] { 0, 0 }));

            waveform = new double[sampleLegth];
            WaveUtils.MixSinWave(waveform, 2, 0.5, 0);
            analyser = new AforgeFftAnalyser();
            spectrum = analyser.Analyse(waveform);
            ChartPrinter.PrintChartWithAutoscale(waveform, 10);
            ChartPrinter.PrintChartWithAutoscale(spectrum, 10);
            samples.Add(new Sample(spectrum, new double[] { 1, 0 }));

            waveform = new double[sampleLegth];
            WaveUtils.MixSinWave(waveform, 5, 0.5, 0);
            analyser = new AforgeFftAnalyser();
            spectrum = analyser.Analyse(waveform);
            ChartPrinter.PrintChartWithAutoscale(waveform, 10);
            ChartPrinter.PrintChartWithAutoscale(spectrum, 10);
            samples.Add(new Sample(spectrum, new double[] { 0, 1 }));
            return samples;
        }
    }
}
