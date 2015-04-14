using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using AForge.Math;
using ListenLearn.Listen.Core;
using NUnit.Framework;

namespace ListenLearnTest
{
    [TestFixture]
    public class AnalyserTest
    {
        [Test]
        public void AnalyserTest_CreateAnalyser()
        {
            var analyser = new AforgeFftAnalyser();
            double[] sin = RenderSin(0.5, 0.5, 512);
            var output = analyser.Analyse(sin);
            Print(output);
        }

        private double[] RenderSin(double frequency, double amplitude, int samples)
        {
            var output = new double[samples];
            for (int i = 0; i < samples; i++)
            {
                output[i] = amplitude*Math.Sin(frequency*i);
            }
            return output;
        }

        private void Print(Complex[] output)
        {
            foreach (var item in output)
            {
                Console.WriteLine(Math.Abs(item.Re)); 
            }
        }
    }
}