using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ListenLearn.Learn.Core;
using ListenLearn.Listen.Core;
using ListenLearnTest.Core;
using NUnit.Framework;

namespace ListenLearn.LearnTest.Core
{
    [TestFixture]
    public class LaundryLearnerTest
    {
        [TestCase(2048)]
        [Ignore("Next: Try some networks")]
        public void LaundryLearnerTest_Experiment(int sampleWindowSize)
        {
            var path = Path.Combine(TestContext.CurrentContext.WorkDirectory, @"Resources\Laundry");
            var files = Directory.GetFiles(path, "*.sample");
            Sample[] samples = new Sample[files.Length];
            LoadSamples(sampleWindowSize, files, samples);
	        NormalizeSamples(samples);

            Random random = new Random();

	        for (int i = 0; i < 10000; i++)
	        {
				Learner learner = new AforgeBackPropogationLaundry(sampleWindowSize / 2, samples[0].output.Length, samples.Count(), random.Next(8,100));
				double errorTarget = random.Next(12, 18);
				var stopwatch = new Stopwatch();
				stopwatch.Start();
		        learner.Learn(o => samples[random.Next(0, samples.Length)], errorTarget);
		        using (var writer = new StreamWriter(this.GetType().Name+".csv", true))
		        {
			        writer.WriteLine(string.Format("{0}, {1}, {2}", errorTarget, learner.ToCsv(), stopwatch.ElapsedMilliseconds));
		        }
	        }
	        //Assert.IsTrue(learner.Learn(o => samples[random.Next(0, samples.Length)], errorTarget), "Learned the ropes");
        }

	    private void NormalizeSamples(Sample[] samples)
	    {
		    foreach (var sample in samples)
		    {
			    NormalizeSample(sample);
		    }
	    }

	    private void NormalizeSample(Sample sample)
	    {
		    double max = sample.input.Concat(new double[] {0}).Max();
		    for (int i = 0; i < sample.input.Count(); i++)
		    {
			    sample.input[i] = sample.input[i]/max;
		    }
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
                //ChartPrinter.PrintChart(spectrum, 20, 200);

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
