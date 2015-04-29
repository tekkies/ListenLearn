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
        const int sampleLength = 16;
        Random random = new Random();

        [TestCase]
        public void SpectrumLearnerTest_Learn()
        {
            List<Sample> samples = GenerateSamples();
            Learner learner = new AforgeBackPropogationSpectrum(sampleLength/2, 2, 3);
            Random random = new Random();
            const double errorTarget = 0.01;
            Assert.IsTrue(learner.Learn(o => samples[random.Next(0, samples.Count)], errorTarget), "Learned the ropes");
        }

        [TestCase]
        public void SpectrumLearnerTest_Meta_TestRandom()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(Random(1.8,2.2));
            }
        }

        private List<Sample> GenerateSamples()
        {
            var samples = new List<Sample>();
           
            for (int i = 0; i < 5; i++)
            {
                AddSample(samples, 1, 0, 0, 0);
                AddSample(samples, Random(1, 3), 0.5, 1, 0);
                AddSample(samples, 5, 0.5, 0, 1);
            }
            return samples;
        }

        double Random(double min, double max)
        {
            return min + (max - min)*random.NextDouble();
        }

        private static void AddSample(List<Sample> samples, double frequency, double amplitude, double a, double b)
        {
            double[] waveform;
            AforgeFftAnalyser analyser;
            double[] spectrum;
            waveform = new double[sampleLength];
            WaveUtils.MixSinWave(waveform, frequency, amplitude, 0);
            analyser = new AforgeFftAnalyser();
            spectrum = analyser.Analyse(waveform);
            ChartPrinter.PrintChartWithAutoscale(waveform, 10);
            ChartPrinter.PrintChartWithAutoscale(spectrum, 10);
            samples.Add(new Sample(spectrum, new double[] {a, b}));
        }
    }
}
