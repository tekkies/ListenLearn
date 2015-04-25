using System;
using System.IO;
using ListenLearn.Learn.Core;
using ListenLearn.Listen.Core;
using ListenLearnTest.Core;
using NUnit.Framework;

namespace ListenLearn.LearnTest.Core
{
    [TestFixture]
    public class LaundryLearnerTest
    {
        [TestCase(2048,3,3)]
        //[TestCase(2048, 5, 3)]
        //[TestCase(2048, 7, 3)]
        //[TestCase(2048, 10, 3)]
        [Ignore("Next: Autoscale spectrum, do some tests on simpler spectrums")]
        public void LaundryLearnerTest_Punt(int sampleWindowSize, int l1Nodes, int l2Nodes)
        {
            var path = Path.Combine(TestContext.CurrentContext.WorkDirectory, @"Resources\Laundry");
            var files = Directory.GetFiles(path, "*.sample");
            Sample[] samples = new Sample[files.Length];
            LoadSamples(sampleWindowSize, files, samples);

            Learner learner = new AforgeBackPropogationLaundry(sampleWindowSize / 2, l1Nodes, l2Nodes);
            Random random = new Random();
            const double errorTarget = 0.1;
            Assert.IsTrue(learner.Learn(o => samples[random.Next(0, samples.Length)], errorTarget), "Learned the ropes");
        }

        private static void LoadSamples(int sampleWindowSize, string[] files, Sample[] samples)
        {
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                //Debug.WriteLine(file);
                var pcmParser = new PcmParser();
                byte[] bytes = File.ReadAllBytes(file);
                pcmParser.Parse(bytes, sampleWindowSize);
                var analyser = new AforgeFftAnalyser();
                var spectrum = analyser.Analyse(pcmParser.data);
                ChartPrinter.PrintChart(spectrum, 20, 200);

                var output = GetOutputFromFileName(file);
                samples[i] = new Sample(spectrum, output);
            }
        }

        private static double[] GetOutputFromFileName(string file)
        {
            var sampleOf = file.Substring(file.Length - 14, 1);
            double[] output = null;
            switch (sampleOf)
            {
                case "A":
                    output = new double[] {1, 0, 0};
                    break;
                case "B":
                    output = new double[] {0, 1, 0};
                    break;
                case "C":
                    output = new double[] {0, 0, 1};
                    break;
                default:
                    Assert.Fail();
                    break;
            }
            return output;
        }
      
    }
}
