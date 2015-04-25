using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenLearnTest.Core
{
    public class ChartPrinter
    {
        public static void PrintLogChart(Double[] data, int rows)
        {
            double[] transform = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                transform[i] = Math.Log10(data[i]);
            }
            PrintChartWithAutoscale(transform, rows);
        }

        public static void PrintChartWithAutoscale(Double[] data, int rows)
        {
            PrintChart(data, rows, 0);
        }
        public static void PrintChart(Double[] data, int rows, double max)
        {
            if (Math.Abs(max) < 0.001)
            {
                max = data.Concat(new double[] { 0 }).Max();    
            }
            for (var row = 0; row < rows; row++)
            {
                PrintChartRow(data, rows, max, row);
            }
            PrintHorizontalAxis(data.Length);
        }

        private static void PrintHorizontalAxis(int length)
        {
            StringBuilder axis = new StringBuilder("u  ");
            for (int i = 0; i < length; i++)
            {
                axis.Append(i%10);
            }
            Console.WriteLine(axis);

            axis = new StringBuilder("t  ");
            for (int i = 0; i < length; i++)
            {
                axis.Append((i / 10)%10);
            }
            Console.WriteLine(axis);

            axis = new StringBuilder("h  ");
            for (int i = 0; i < length; i++)
            {
                axis.Append((i / 100) % 10);
            }
            Console.WriteLine(axis);

        }

        private static void PrintChartRow(double[] data, int rows, double max, int row)
        {
            var rowText = new StringBuilder();
            foreach (var item in data)
            {
                var appearsOnRow = item * (rows / max) >= (rows - row);
                rowText.Append(appearsOnRow ? '*' : ' ');
            }
            Console.WriteLine(row.ToString().PadRight(3) + rowText);
        }
    }
}
