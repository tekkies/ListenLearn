using System;
using System.Linq;
using System.Text;
using ListenLearn.Listen.Core;
using NUnit.Framework;

namespace ListenLearnTest
{
    [TestFixture]
    public class AnalyserTest
    {
        [Test]
        public void AnalyserTest_AnalyseSin()
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
            for (var i = 0; i < input.Length; i++)
            {
                input[i] += amplitude*Math.Sin(frequency*i + offset);
            }
        }

        private void PrintChart(Double[] output, int rows)
        {
            var max = output.Concat(new double[] {0}).Max();
            for (var row = 0; row < rows; row++)
            {
                PrintChartRow(output, rows, max, row);
            }
        }

        private static void PrintChartRow(double[] output, int rows, double max, int row)
        {
            var rowText = new StringBuilder();
            foreach (var item in output)
            {
                var appearsOnRow = item*(rows/max) >= (rows - row);
                rowText.Append(appearsOnRow ? '*' : ' ');
            }
            Console.WriteLine(row.ToString() + rowText);
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