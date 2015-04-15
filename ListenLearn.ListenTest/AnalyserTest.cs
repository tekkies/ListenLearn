using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
            var input = new double[256];

            RenderSin(input, 0.5, 0.5, 0);
            //RenderSin(input, 1.5, 1, 0);
            //RenderSin(input, 12, 0.5);
            var output = analyser.Analyse(input);
            PrintChart(input, 40);
            PrintChart(output, 40);
            PrintArray(output);
        }

        private void RenderSin(double[] input, double frequency, double amplitude, double offset)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] += amplitude*Math.Sin(frequency*i+offset);
            }
        }

        private void PrintChart(Double[] output, int rows)
        {
            double max = output.Concat(new double[] {0}).Max();

            for (int row = 0; row < rows; row++)
            {
                StringBuilder rowText=new StringBuilder();
                foreach (var item in output)
                {
                    if (item*(rows/max) >= (rows - row))
                    {
                        rowText.Append('*');
                    }
                    else
                    {
                        rowText.Append(' ');
                    }
                }
                Console.WriteLine(row.ToString()+rowText);
            }
        }

        private void PrintArray(Double[] output)
        {
            foreach (var item in output)
            {
                Console.WriteLine(item);                    
            }
        }
    }
}