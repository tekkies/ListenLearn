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

            RenderSin(input, 0.5, 0.5);
            RenderSin(input, 1.5, 0.5);
            var output = analyser.Analyse(input);
            Print(output, 100);
        }

        private void RenderSin(double[] input, double frequency, double amplitude)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] += amplitude*Math.Sin(frequency*i);
            }
        }

        private void Print(Complex[] output, int rows)
        {
            double max = output.Select(item => Math.Abs(item.Re)).Concat(new double[] {0}).Max();
            
            for (int row = 0; row < rows; row++)
            {
                StringBuilder rowText=new StringBuilder();
                foreach (var item in output)
                {
                    if (item.Re*(rows/max) >= (rows - row))
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
    }
}