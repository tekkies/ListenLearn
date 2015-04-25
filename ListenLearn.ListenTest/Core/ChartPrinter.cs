using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenLearnTest.Core
{
    public class ChartPrinter
    {
        public static void PrintChart(Double[] output, int rows)
        {
            var max = output.Concat(new double[] { 0 }).Max();
            for (var row = 0; row < rows; row++)
            {
                PrintChartRow(output, rows, max, row);
            }
            Console.Write(' ');
            Console.WriteLine(new String('-', output.Length));
        }

        private static void PrintChartRow(double[] output, int rows, double max, int row)
        {
            var rowText = new StringBuilder();
            foreach (var item in output)
            {
                var appearsOnRow = item * (rows / max) >= (rows - row);
                rowText.Append(appearsOnRow ? '*' : ' ');
            }
            Console.WriteLine(row.ToString() + rowText);
        }
    }
}
