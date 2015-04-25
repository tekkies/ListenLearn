using System;
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
            Assert.AreEqual(expectedPeak, GetPeakElement(output));
        }

        private int GetPeakElement(double[] output)
        {
            int peakElement = -1;
            double peakValue = 0;
            for (int element = 0; element < output.Length; element++)
            {
                if (output[element] > peakValue)
                {
                    peakElement = element;
                    peakValue = output[element];
                }
            }
            return peakElement;
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